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


    //abstract = child class has to use this method 
    public abstract void SelectPickup();
    public abstract void DeselectPickup();

    //used for when the pickup has finished is use
    public abstract void DeactivatePickup();

    //virtual = child doesnt have to use this method
    public virtual void ActivatePickup()
    {
        m_Ammo--;
    }
}
