using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_MissileLauncher : Pickup
{
    [SerializeField] private Transform m_SpawnRocketTransform;
    [SerializeField] private GameObject m_RocketPrefab;
    private List<Destructible> m_Targets = new List<Destructible>();
    private Destructible m_CurrentTarget;
    private bool m_IsSelected;

    private void Update()
    {
        if (m_IsSelected)
        {
            findTarget();
        }
        //Debug.Log(m_Ammo);
    }

    public override void ActivatePickup()
    {
        base.ActivatePickup();
        if (m_CurrentTarget != null)
        {
            Instantiate(m_RocketPrefab, m_SpawnRocketTransform.position, m_SpawnRocketTransform.rotation).GetComponent<MissileLogic>().Initialize(m_CurrentTarget.transform);
        }
    }

    public override void DeselectPickup()
    {
        m_IsSelected = false;
        if (m_CurrentTarget != null)
        {
            m_CurrentTarget.StopTarget();
            m_CurrentTarget = null;
        }
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
        m_Targets.RemoveAll(a => a == null);

        Destructible newTarget = null;
        float shortestDistance = 1000000, currentDistance, objectMagnitude;

        foreach (var target in m_Targets)
        {
            //distance between camera and destructible in question
            objectMagnitude = Vector3.Distance(target.transform.position, Camera.main.transform.position);
            //distance between where caera is looking plus the distance between camera and object and the object
            currentDistance = Vector3.Distance((Camera.main.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0)) + Camera.main.transform.forward * objectMagnitude), target.transform.position);
            if (currentDistance < shortestDistance)
            {
                shortestDistance = currentDistance;
                newTarget = target;
            }
        }

        //Debug.Log(m_Targets[4].name);

        if (m_CurrentTarget != newTarget)
        {
            if (m_CurrentTarget != null)
            {
                m_CurrentTarget.StopTarget();
            }

            if (newTarget != null)
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
