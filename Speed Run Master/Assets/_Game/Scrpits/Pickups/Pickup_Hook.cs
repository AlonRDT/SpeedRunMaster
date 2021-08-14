using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Hook : Pickup
{
    [SerializeField] private GameObject m_Crosshair;

    protected new void Start()
    {
        base.Start();
        m_Crosshair.SetActive(false);
    }

    public override void ActivatePickup()
    {
        base.ActivatePickup();
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }
}
