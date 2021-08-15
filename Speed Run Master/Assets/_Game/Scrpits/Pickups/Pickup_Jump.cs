using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Jump : Pickup
{
    [SerializeField] private float m_JumpPower;

    public override void ActivatePickup()
    {
        base.ActivatePickup();
        Vector3 up = transform.parent.parent.up;
        Rigidbody rb = transform.parent.parent.GetComponent<Rigidbody>();
        rb.AddForce(m_JumpPower * up, ForceMode.Impulse);
    }

    public override void DeactivatePickup()
    {
        
    }

    public override void DeselectPickup()
    {
        
    }

    public override void SelectPickup()
    {
        
    }
}
