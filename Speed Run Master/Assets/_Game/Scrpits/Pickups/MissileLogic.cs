using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLogic : MonoBehaviour
{
    private Transform m_TargetTransform;
    private Rigidbody m_RigidBody;

    [SerializeField] private GameObject m_ExplosionPrefab;
    [SerializeField] private float m_TurnSpeed;
    [SerializeField] private float m_FlySpeed;

    private bool m_IsInitialized;

    // Start is called before the first frame update
    void Awake()
    {
        m_IsInitialized = false;
        m_RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_IsInitialized)
        {
            //explodes the missile is has no target while fired
            if(m_TargetTransform == null)
            {
                explode();
            }
            else
            {
                m_RigidBody.velocity = transform.forward * m_FlySpeed;
                //sets the roation of the missile to the target
                Quaternion rocketTargetRot = Quaternion.LookRotation(m_TargetTransform.position - transform.position);
                m_RigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocketTargetRot, m_TurnSpeed));
            }
        }
        
    }

    private void explode()
    {
        Destroy(gameObject);
        Destroy(Instantiate(m_ExplosionPrefab, transform.position, Quaternion.identity), 3);
    }

    //start the initialization process
    public void Initialize(Transform target)
    {
        m_TargetTransform = target;
        m_IsInitialized = true;
        //Debug.Log(target.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Destructible")
        {
            explode();
        }
    }
}
