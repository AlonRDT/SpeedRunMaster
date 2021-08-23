using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    [SerializeField] GameObject m_PanelChooseLevel;
    [SerializeField] GameObject m_PanelHighScores;
    [SerializeField] GameObject m_PanelRewatch;
    [SerializeField] GameObject m_MainMenuFirstButton;
    [SerializeField] GameObject m_ChooseLevelFirstButton;
    [SerializeField] GameObject m_RewatchFirstButton;
    [SerializeField] GameObject m_HighscoreFirstButton;
    [SerializeField] GameObject m_ChooseLevelCloseButton;
    [SerializeField] GameObject m_HighscoreCloseButton;
    [SerializeField] GameObject m_RewatchCloseButton;
    [SerializeField] Text m_BestCanyonTimeText;
    [SerializeField] Text m_BestMoonTimeText;
    [SerializeField] Text m_BestLabTimeText;
    [SerializeField] List<Button> m_ReplayButtons;

    private void Start()
    {
        m_PanelChooseLevel.SetActive(false);
        m_PanelHighScores.SetActive(false);
        m_PanelRewatch.SetActive(false);

        m_BestCanyonTimeText.text = PlayerPrefs.GetString("BestCanyonTime", "None");
        m_BestMoonTimeText.text = PlayerPrefs.GetString("BestMoonTime", "None");
        m_BestLabTimeText.text = PlayerPrefs.GetString("BestLabTime", "None");


        Time.timeScale = 1;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_MainMenuFirstButton);

        for (int i = 0; i < m_ReplayButtons.Count; i++)
        {
            if(GameManager.DoesMapHaveHistory(i + 1) == false)
            {
                m_ReplayButtons[i].interactable = false;
            }
        }
    }

    public void MenuBack()
    {
        if (m_PanelChooseLevel.activeInHierarchy == true)
        {
            CloseChooseLevel();
        }
        else if (m_PanelHighScores.activeInHierarchy == true)
        {
            CloseHighScores();
        }
        else if (m_PanelRewatch.activeInHierarchy == true)
        {
            CloseRewatch();
        }
    }

    public void OpenChooseLevel()
    {
        m_PanelChooseLevel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_ChooseLevelFirstButton);
    }

    public void CloseChooseLevel()
    {
        m_PanelChooseLevel.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_ChooseLevelCloseButton);
    }

    public void OpenHighScores()
    {
        m_PanelHighScores.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_HighscoreFirstButton);
    }

    public void CloseHighScores()
    {
        m_PanelHighScores.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_HighscoreCloseButton);
    }

    public void OpenRewatch()
    {
        m_PanelRewatch.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_RewatchFirstButton);
    }

    public void CloseRewatch()
    {
        m_PanelRewatch.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_RewatchCloseButton);
    }

    //if spawnplayer isnt checked in the ui button the game knows thzt its only a repaly
    public void LoadDesertLevel(bool spawnPlayer)
    {
        GameManager.SpawnPlayer = spawnPlayer;
        SceneManager.LoadScene(1);
    }

    public void LoadMoonLevel(bool spawnPlayer)
    {
        GameManager.SpawnPlayer = spawnPlayer;
        SceneManager.LoadScene(2);
    }

    public void LoadLabLevel(bool spawnPlayer)
    {
        GameManager.SpawnPlayer = spawnPlayer;
        SceneManager.LoadScene(3);
    }

    public void Exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            GameObject newSelected = m_MainMenuFirstButton;

            if (m_PanelChooseLevel.activeInHierarchy == true)
            {
                newSelected = m_ChooseLevelFirstButton;
            }
            else if (m_PanelHighScores.activeInHierarchy == true)
            {
                newSelected = m_HighscoreFirstButton;
            }
            else if (m_PanelRewatch.activeInHierarchy == true)
            {
                newSelected = m_RewatchFirstButton;
            }

            EventSystem.current.SetSelectedGameObject(newSelected);
        }
    }
}
