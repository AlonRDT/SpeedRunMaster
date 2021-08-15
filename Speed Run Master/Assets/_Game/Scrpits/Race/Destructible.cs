using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject m_MarkerBase;
    private bool m_IsTargeted;

    // Start is called before the first frame update
    void Start()
    {
        StopTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsTargeted == true)
        {
            m_MarkerBase.transform.LookAt(Camera.main.transform);
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
