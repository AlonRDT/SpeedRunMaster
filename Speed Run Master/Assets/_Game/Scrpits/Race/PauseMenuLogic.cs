using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuLogic : MonoBehaviour
{
    [SerializeField] GameObject m_PuaseMenuPanel;
    [SerializeField] GameObject m_FirstSelectedButton;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        m_PuaseMenuPanel.SetActive(false);
    }

    public void OpenMenu()
    {
        if (m_PuaseMenuPanel.activeInHierarchy == false)
        {
            TogglePanel();
        }
    }

    public void MenuBack()
    {
        if (m_PuaseMenuPanel.activeInHierarchy == true)
        {
            TogglePanel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PuaseMenuPanel.activeInHierarchy == true)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(m_FirstSelectedButton);
            }
        }
    }

    public void Resume()
    {
        TogglePanel();
    }

    public void TogglePanel()
    {
        if (m_PuaseMenuPanel.activeInHierarchy == true)
        {
            Time.timeScale = 1;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            m_PuaseMenuPanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            m_PuaseMenuPanel.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(m_FirstSelectedButton);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
