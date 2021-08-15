using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Hook : Pickup
{
    [SerializeField] private GameObject m_Crosshair;
    [SerializeField] private GameObject m_GrappleStart;
    [SerializeField] private float m_LineLength;

    private LineRenderer m_LineRenderer;
    private SpringJoint m_Spring;
    private GameObject m_Base;
    private Vector3 m_GrapplePoint;

    protected new void Start()
    {
        base.Start();
        m_Crosshair.SetActive(false);
        m_LineRenderer = GetComponent<LineRenderer>();
        m_Base = transform.parent.parent.gameObject;
    }

    public override void ActivatePickup()
    {
        base.ActivatePickup();
        m_Spring = m_Base.AddComponent<SpringJoint>();
        //dont calculate automatically the anchor
        m_Spring.autoConfigureConnectedAnchor = false;
        //get a point in front of camera at desired distance
        m_GrapplePoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0)) + Camera.main.transform.forward * m_LineLength;
        m_Spring.connectedAnchor = m_GrapplePoint;

        //the distance grapple will try to keep from grapple point
        //m_Spring.maxDistance = m_LineLength * 0f;
        //m_Spring.minDistance = m_LineLength * 0.1f;

        //pull push power
        m_Spring.spring = 3f;
        m_Spring.damper = 2f;
        //m_Spring.massScale = 1f;

        m_LineRenderer.positionCount = 2;
    }

    private void LateUpdate()
    {
        drawRope();
    }

    private void drawRope()
    {
        if(m_Spring != null)
        {
            m_LineRenderer.SetPosition(0, m_GrappleStart.transform.position);
            m_LineRenderer.SetPosition(1, m_GrapplePoint);
        }
    }

    public override void DeselectPickup()
    {
        m_Crosshair.SetActive(false);
    }

    public override void SelectPickup()
    {
        m_Crosshair.SetActive(true);
    }

    public override void DeactivatePickup()
    {
        m_LineRenderer.positionCount = 0;
        Destroy(m_Spring);
    }
}
