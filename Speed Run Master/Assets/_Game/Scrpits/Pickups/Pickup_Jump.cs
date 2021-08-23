using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Jump : Pickup
{
    [SerializeField] private GameManager m_Manager;
    [SerializeField] private float m_JumpPower;

    //override = allows to change a virtual function
    public override void ActivatePickup()
    {
        base.ActivatePickup();
        Vector3 up = transform.parent.parent.up;
        Rigidbody rb = transform.parent.parent.GetComponent<Rigidbody>();
        rb.AddForce(m_JumpPower * up, ForceMode.Impulse);
        m_Manager.RegisterJumping(true);
    }

    public override void DeactivatePickup()
    {
        m_Manager.RegisterJumping(false);
        
    }
    //abstract - must be used even empty
    public override void DeselectPickup()
    {
        
    }

    public override void SelectPickup()
    {
        
    }

    //makes the ghost jump in the history time
    public void ManualJump()
    {
        Vector3 up = transform.parent.parent.up;
        Rigidbody rb = transform.parent.parent.GetComponent<Rigidbody>();
        rb.AddForce(m_JumpPower * up, ForceMode.Impulse);
    }
}
