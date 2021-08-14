using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Shield : Pickup
{
    [SerializeField] private GameObject m_ShieldMesh;
    [SerializeField] private float m_ShieldActiveTime;
    private float m_AccumlatedTime;
    private bool m_IsActive;

    protected new void Start()
    {
        base.Start();
        m_ShieldMesh.SetActive(false);
        m_AccumlatedTime = 0;
        m_IsActive = false;
    }

    private void Update()
    {
        if (m_IsActive)
        {
            m_AccumlatedTime += Time.deltaTime;

            if (m_AccumlatedTime >= m_ShieldActiveTime)
            {
                m_ShieldMesh.SetActive(false);
                m_IsActive = false;
            }
        }
    }

    public override void ActivatePickup()
    {
        if (m_IsActive == false)
        {
            base.ActivatePickup();
            m_AccumlatedTime = 0;
            m_IsActive = true;
            m_ShieldMesh.SetActive(true);
        }
    }

    public override void DeselectPickup()
    {
       
    }

    public override void SelectPickup()
    {
       
    }

    public override void DeactivatePickup()
    {
        
    }
}
