using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    private int m_Ammo;

    protected void Start()
    {
        m_Ammo = 0;
    }

    public void IncreaseAmmo()
    {
        m_Ammo++;
        //Debug.Log(m_Ammo);
        //Debug.Log(transform.name);
    }

    public bool IsEmpty()
    {
        return m_Ammo == 0;
    }

    public abstract void SelectPickup();
    public abstract void DeselectPickup();
    public abstract void DeactivatePickup();
    public virtual void ActivatePickup()
    {
        m_Ammo--;
    }
}
