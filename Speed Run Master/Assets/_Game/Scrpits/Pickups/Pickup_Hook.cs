using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Hook : Pickup
{
    [SerializeField] private GameObject m_Crosshair;
    [SerializeField] private GameObject m_GrappleStart;
    [SerializeField] private float m_LineLength;
    [SerializeField] private float m_JointFinalMaxDistancePrecentage;
    [SerializeField] private float m_JointDeltaMaxDistancePredentage;
    [SerializeField] private float m_JointSpring;
    [SerializeField] private float m_JointDamper;
    [SerializeField] private float m_JointMassScale;
    [SerializeField] private Vector3 m_JointAnchorLocation;

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
        m_Spring.anchor = m_JointAnchorLocation;

        //the distance grapple will try to keep from grapple point
        m_Spring.maxDistance = m_LineLength * (1 - m_JointDeltaMaxDistancePredentage);


        //pull push power
        m_Spring.spring = m_JointSpring;
        m_Spring.damper = m_JointDamper;
        m_Spring.massScale = m_JointMassScale;

        m_LineRenderer.positionCount = 2;
    }

    private void LateUpdate()
    {
        if (m_Spring != null)
        {
            float distance = Vector3.Distance(transform.position, m_GrapplePoint);
            m_Spring.maxDistance = Mathf.Max(distance - m_LineLength * m_JointDeltaMaxDistancePredentage, m_LineLength * m_JointFinalMaxDistancePrecentage);
            drawRope();
        }
    }

    private void drawRope()
    {

        m_LineRenderer.SetPosition(0, m_GrappleStart.transform.position);
        m_LineRenderer.SetPosition(1, m_GrapplePoint);
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
