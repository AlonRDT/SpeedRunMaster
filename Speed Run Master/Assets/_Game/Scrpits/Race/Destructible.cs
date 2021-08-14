using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject m_MarkerBase;
    private bool m_IsTargeted;
    private Transform m_PlayerTransform;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerTransform = GameObject.Find("Player").transform;
        StopTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsTargeted == true)
        {
            m_MarkerBase.transform.LookAt(m_PlayerTransform);
            m_MarkerBase.transform.Rotate(new Vector3(0, -90, 0));
        }
    }

    public void StartTarget()
    {
        m_IsTargeted = true;
        m_MarkerBase.SetActive(true);
    }

    public void StopTarget()
    {
        m_IsTargeted = false;
        m_MarkerBase.SetActive(false);
    }

    public bool IsTargeted()
    {
        return m_IsTargeted;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Destructor")
        {
            Destroy(gameObject);
        }
    }
}
