using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //static = the data isnt deleted when scenes are changed. and doesnt require to define object to use in other scripts(public)
    public static bool SpawnPlayer;
    private static HistoryData[] m_Replays;
    //isdebug = allows to save replay by pressing a button
    [SerializeField] private bool m_IsDebug;
    [SerializeField] private Text m_TimeText;
    //ready set go + end time
    [SerializeField] private Text m_MainText;

    //saves the clicks time for the ghost to use
    private float m_TimeElapsed;
    //historydata - saves the players input during the game
    private HistoryData m_NewData;
    private GameInput m_Input;
    private float m_OldHorizontalInput;
    private float m_OldVerticalInput;
    private int m_SceneIndex;
    private bool m_IsGameStarted;
    private Vector3 m_CurrentGrapplePoint;


    // Start is called before the first frame update
    void Start()
    {
        m_Input = new GameInput();
        m_Input.Enable();
        m_OldHorizontalInput = 0;
        m_OldVerticalInput = 0;
        m_TimeElapsed = 0;
        m_SceneIndex = SceneManager.GetActiveScene().buildIndex;
        m_IsGameStarted = false;

        if (m_Replays == null)
        {
            m_Replays = new HistoryData[3];
        }

        foreach (var history in m_Replays)
        {
            if (history != null)
            {
                history.Initialize();
            }
        }

        if(m_IsDebug == true)
        {
            SpawnPlayer = true;
        }

        if (m_SceneIndex != 0)
        {
            //Debug.Log("new history data");
            m_NewData = new HistoryData();
            m_NewData.MapIndex = m_SceneIndex;
            StartCoroutine(StartGame());
        }
        else
        {
            m_MainText.gameObject.SetActive(false);
            m_TimeText.gameObject.SetActive(false);
        }
    }

    IEnumerator StartGame()
    {
        Animator anim = m_MainText.GetComponent<Animator>();
        m_MainText.text = "Ready";
        anim.SetTrigger("Enlarge");
        yield return new WaitForSeconds(2);
        m_MainText.text = "Set";
        anim.SetTrigger("Enlarge");
        yield return new WaitForSeconds(2);
        m_MainText.text = "Go";
        anim.SetTrigger("Enlarge");
        yield return new WaitForSeconds(1);
        m_IsGameStarted = true;
        yield return new WaitForSeconds(1);
        m_MainText.gameObject.SetActive(false);
    }

    public bool CanCarMove()
    {
        return m_IsGameStarted;
    }

    // fixedUpdate is called once per frame over the exsact same time
    private void FixedUpdate()
    {
        if (m_SceneIndex == 0) return;

        m_TimeText.text = m_TimeElapsed.ToString("0.00");

        if (m_IsGameStarted == false) return;

        m_TimeElapsed += Time.fixedDeltaTime;

        //checks what map were on to check if the replay for this map exists 
        if (m_Replays[m_SceneIndex - 1] != null)
        {
            int actionIndex = 0;
            while (actionIndex >= 0)
            {
                actionIndex = m_Replays[m_SceneIndex - 1].ApplyAction(m_TimeElapsed);
                if (actionIndex == 4 && m_Replays[m_SceneIndex - 1].IsGrapple == true)
                {
                    m_CurrentGrapplePoint = m_Replays[m_SceneIndex - 1].GetGrapplePoint();
                }
            }
        }

        //gets the gamepad input and registes it to the history
        float horizontalInput = m_Input.Gameplay.TurnCar.ReadValue<float>();
        if (horizontalInput > 0)
        {
            horizontalInput = 1;
        }
        else if (horizontalInput < 0)
        {
            horizontalInput = -1;
        }
        float verticalInput = m_Input.Gameplay.MoveCar.ReadValue<float>();
        if (verticalInput > 0)
        {
            verticalInput = 1;
        }
        else if (verticalInput < 0)
        {
            verticalInput = -1;
        }

        //adds the game data for th ghost to newdata. (in future if drive is shorter it will make it the new ghost)
        if (m_NewData != null)
        {
            //Debug.Log(verticalInput);
            if (m_OldHorizontalInput != horizontalInput)
            {
                m_NewData.RegisterHorizontalAction(m_TimeElapsed, Mathf.RoundToInt(horizontalInput));
                m_OldHorizontalInput = horizontalInput;
            }

            if (m_OldVerticalInput != verticalInput)
            {
                m_NewData.RegisterVerticalAction(m_TimeElapsed, Mathf.RoundToInt(verticalInput));
                m_OldVerticalInput = verticalInput;
            }
        }
    }

    public bool CheckExistance(bool player)
    {
        bool output = false;

        if (player == true)
        {
            output = SpawnPlayer;
        }
        else
        {
            output = m_Replays[m_SceneIndex - 1] != null;
        }

        //Debug.Log(carIndex);
        //Debug.Log(output);

        return output;
    }

    public bool IsSetCameraActive(bool player)
    {
        bool output = false;

        if (player == true)
        {
            output = SpawnPlayer;
        }
        else
        {
            output = !SpawnPlayer;
        }

        //Debug.Log(carIndex);
        //Debug.Log(output);

        return output;
    }

    //replays = gets the input for that time from history for ghost. sceneindex = shows the index for the current map(-1 because the first map is the main menu
    public float GetHorizontalInput()
    {
        return m_Replays[m_SceneIndex - 1].HorizontalInput;
    }

    public float GetVerticalInput()
    {
        return m_Replays[m_SceneIndex - 1].VerticalInput;
    }

    public bool GetBrakingInput()
    {
        return m_Replays[m_SceneIndex - 1].IsBraking;
    }

    public bool GetJumpInput()
    {
        return m_Replays[m_SceneIndex - 1].IsJump;
    }

    public bool GetJGrappleInput()
    {
        return m_Replays[m_SceneIndex - 1].IsGrapple;
    }

    public Vector3 GetJGrapplePoint()
    {
        return m_CurrentGrapplePoint;
    }

    public void RaceComplete()
    {
        m_TimeText.gameObject.SetActive(false);
        m_MainText.gameObject.SetActive(true);
        m_MainText.text = m_TimeElapsed.ToString();

        //checks if the newdata time is shorter and saves it as the replay if its shorter
        m_NewData.RaceTime = m_TimeElapsed;
        if (m_Replays[m_SceneIndex - 1] != null)
        {
            if (m_TimeElapsed < m_Replays[m_SceneIndex - 1].RaceTime)
            {
                m_Replays[m_SceneIndex - 1] = m_NewData;
            }
        }
        else
        {
            m_Replays[m_SceneIndex - 1] = m_NewData;
        }

        string currentValue = "None";
        float finalValue;
        switch (m_SceneIndex)
        {
            //playerprefs saves the best time on that map even when you exit the game. checks the memory slot for whats there
            case 1:
                currentValue = PlayerPrefs.GetString("BestCanyonTime", "None");
                break;
            case 2:
                currentValue = PlayerPrefs.GetString("BestMoonTime", "None");
                break;
            case 3:
                currentValue = PlayerPrefs.GetString("BestLabTime", "None");
                break;
            default:
                break;
        }

        if (currentValue != "None")
        {
            //if parse is valid than final value will be initialized with parse value(float)
            if (float.TryParse(currentValue, out finalValue))
            {
                //changes the time it took to finish the replay to the time it took to finish the race(only if race time was lower)
                if (finalValue > m_TimeElapsed)
                {
                    finalValue = m_TimeElapsed;
                }
            }
            else
            {
                finalValue = m_TimeElapsed;
            }
        }
        else
        {
            finalValue = m_TimeElapsed;
        }

        switch (m_SceneIndex)
        {
            //playerprefs saves the best time on that map even when you exit the game. saves the memory slot for whats there
            case 1:
                PlayerPrefs.SetString("BestCanyonTime", finalValue.ToString("0.00"));
                break;
            case 2:
                PlayerPrefs.SetString("BestMoonTime", finalValue.ToString("0.00"));
                break;
            case 3:
                PlayerPrefs.SetString("BestLabTime", finalValue.ToString("0.00"));
                break;
            default:
                break;
        }


        //stop fixed update. stops saving the ghost data
        m_IsGameStarted = false;
        Invoke("loadMainMenu", 4);
    }

    private void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RegisterBreaking(bool start)
    {
        m_NewData.RegisterBrakeAction(m_TimeElapsed, start);
    }

    public void RegisterJumping(bool start)
    {
        m_NewData.RegisterJumpAction(m_TimeElapsed, start);
    }

    public void RegisterGrappling(bool start, Vector3 point)
    {
        m_NewData.ResgterGrappleAction(m_TimeElapsed, start, point);
    }

    public void DebugSaveReplay(InputAction.CallbackContext context)
    {
        if (m_IsDebug)
        {
            if (context.phase == InputActionPhase.Started)
            {
                m_Replays[m_SceneIndex - 1] = m_NewData;
            }
        }
    }

    //cant click replay button if there is no replay
    public static bool DoesMapHaveHistory(int mapIndex)
    {
        if (m_Replays == null)
        {
            m_Replays = new HistoryData[3];
        }
        return m_Replays[mapIndex - 1] != null;
    }
}
