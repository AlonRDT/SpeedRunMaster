using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_MissileLauncher : Pickup
{
    private List<Destructible> m_Targets = new List<Destructible>();
    private Destructible m_CurrentTarget;
    private bool m_IsSelected;

    private void Update()
    {
        if (m_IsSelected)
        {
            findTarget();
        }
    }

    public override void ActivatePickup()
    {
        base.ActivatePickup();
        throw new System.NotImplementedException();
    }

    public override void DeselectPickup()
    {
        m_IsSelected = false;
        m_CurrentTarget.StopTarget();
        m_CurrentTarget = null;
    }

    public override void SelectPickup()
    {
        m_IsSelected = true;
        findTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Destructible")
        {
            Destructible newDestructible = other.GetComponent<Destructible>();
            if (newDestructible == null)
            {
                throw new System.Exception("Gameobject with tag Destructible and no component Destructible is not allowed");
            }
            else
            {
                m_Targets.Add(newDestructible);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Destructible")
        {
            Destructible newDestructible = other.GetComponent<Destructible>();
            if (newDestructible == null)
            {
                throw new System.Exception("Gameobject with tag Destructible and no component Destructible is not allowed");
            }
            else
            {
                m_Targets.Remove(newDestructible);
            }
        }
    }

    private void findTarget()
    {
        Destructible newTarget = null;
        float shortestDistance = 1000000, currentDistance, objectMagnitude;

        foreach (var target in m_Targets)
        {
            objectMagnitude = Vector3.Distance(target.transform.position, Camera.main.transform.position);
            currentDistance = Vector3.Distance((Camera.main.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0)) * objectMagnitude), target.transform.position);
            if(currentDistance < shortestDistance)
            {
                shortestDistance = currentDistance;
                newTarget = target;
            }
        }

        //Debug.Log(newTarget.name);

        if(m_CurrentTarget != newTarget)
        {
            if(m_CurrentTarget != null)
            {
                m_CurrentTarget.StopTarget();
            }

            if(newTarget != null)
            {
                newTarget.StartTarget();
            }

            m_CurrentTarget = newTarget;
        }
    }

    public override void DeactivatePickup()
    {
       
    }
}
