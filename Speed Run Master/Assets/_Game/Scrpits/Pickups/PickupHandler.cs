using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PickupHandler : MonoBehaviour
{
    [SerializeField] private List<Pickup> m_Weapons = new List<Pickup>();
    [SerializeField] private List<Sprite> m_PickupSprites;
    [SerializeField] private Image m_PickupImage;
    private int m_CurrentPickupIndex;
    private bool m_ActionInProgress;//if changes weapon during us of weapon it stops the weapon thats being used 
    private AudioSource m_NoPickupAudio;

    // Start is called before the first frame update
    void Start()
    {
        m_NoPickupAudio = GetComponent<AudioSource>();
        m_PickupImage.sprite = m_PickupSprites[0];
        m_CurrentPickupIndex = -1;
        m_ActionInProgress = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            int newPickupIndex = UnityEngine.Random.Range(0, m_Weapons.Count);
            m_Weapons[newPickupIndex].IncreaseAmmo();
            if (m_CurrentPickupIndex == -1)
            {
                SwitchWeapon();
            }
            Destroy(other.gameObject);
        }
    }

    public void SwitchWeaponOnInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            SwitchWeapon();
        }
    }

    public void SwitchWeapon()
    {
        if (m_ActionInProgress == true)
        {
            m_Weapons[m_CurrentPickupIndex].DeactivatePickup();
            m_ActionInProgress = false;
        }

        int newIndex = -1;

        for (int i = 0; i < m_Weapons.Count; i++)
        {
            //Debug.Log(m_Weapons[i].IsEmpty());
            if (m_Weapons[i].IsEmpty() == false)
            {
                if (newIndex != -1 && m_CurrentPickupIndex == i)
                {

                }
                else
                {
                    newIndex = i;
                }

                //if the current pickup is the last i have stay on the pickup im currently on
                if (m_CurrentPickupIndex == m_Weapons.Count - 1)
                {
                    break;
                }

                if (i > m_CurrentPickupIndex)
                {
                    break;
                }
            }
        }

        loadWeapon(newIndex);

    }

    private void loadWeapon(int newIndex)
    {
        //when you change pickup it stops all the last [ickup attributs
        if (m_CurrentPickupIndex != -1)
        {
            m_Weapons[m_CurrentPickupIndex].DeselectPickup();
        }

        if (newIndex != -1)
        {
            m_Weapons[newIndex].SelectPickup();
        }
        else
        {
            m_NoPickupAudio.Play();
        }

        m_PickupImage.sprite = m_PickupSprites[newIndex + 1];
        m_CurrentPickupIndex = newIndex;
    }


    //callbackcontext + context - are used to be able to play with the gamepad
    //we only use it for the up and down click
    public void Fire(InputAction.CallbackContext context)
    {
        if (m_CurrentPickupIndex != -1 && context.phase == InputActionPhase.Started)
        {
            m_Weapons[m_CurrentPickupIndex].ActivatePickup();
            m_ActionInProgress = true;
        }
        else if(m_CurrentPickupIndex == -1 && context.phase == InputActionPhase.Started)
        {
            m_NoPickupAudio.Play();
        }
        else if (m_CurrentPickupIndex != -1 && context.phase == InputActionPhase.Canceled)
        {
            m_ActionInProgress = false;
            m_Weapons[m_CurrentPickupIndex].DeactivatePickup();
            if (m_Weapons[m_CurrentPickupIndex].IsEmpty() == true)
            {
                SwitchWeapon();
            }
        }
    }
}
