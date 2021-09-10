using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    //calls the endlevel function from the game manager
    [SerializeField] private GameManager m_Manager;
    private void OnTriggerEnter(Collider other)
    {
        CarController car = other.GetComponent<CarController>();
        if (car != null && car.IsPlayer() == true)
        {
            m_Manager.RaceComplete();
            Destroy(gameObject);
        }
    }
}
