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
    private bool m_ActionInProgress;

    // Start is called before the first frame update
    void Start()
    {
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
        if (m_CurrentPickupIndex != -1)
        {
            m_Weapons[m_CurrentPickupIndex].DeselectPickup();
        }

        if (newIndex != -1)
        {
            m_Weapons[newIndex].SelectPickup();
        }

        m_PickupImage.sprite = m_PickupSprites[newIndex + 1];
        m_CurrentPickupIndex = newIndex;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (m_CurrentPickupIndex != -1 && context.phase == InputActionPhase.Started)
        {
            m_Weapons[m_CurrentPickupIndex].ActivatePickup();
            m_ActionInProgress = true;
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
