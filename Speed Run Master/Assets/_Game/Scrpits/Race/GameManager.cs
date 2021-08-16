using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool SpawnPlayer;
    private static HistoryData[] m_Replays;
    private int m_FrameIndex;
    private HistoryData m_NewData;
    private GameInput m_Input;
    private float m_OldHorizontalInput;
    private float m_OldVerticalInput;


    // Start is called before the first frame update
    void Start()
    {
        m_Input = new GameInput();
        m_Input.Enable();
        m_OldHorizontalInput = 0;
        m_OldVerticalInput = 0;

        if (m_Replays == null)
        {
            m_Replays = new HistoryData[4];
        }

        foreach (var history in m_Replays)
        {
            if (history != null)
            {
                history.Initialize();
            }
        }

        //testing temporary
        SpawnPlayer = true;

        m_FrameIndex = 0;
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            m_NewData = new HistoryData();
            m_NewData.MapIndex = SceneManager.GetActiveScene().buildIndex;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        foreach (var history in m_Replays)
        {
            if (history != null)
            {
                while (history.ApplyAction(m_FrameIndex) == true)
                {

                }
            }
        }

        //Debug.Log(m_Replays.)

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

        if (m_NewData != null)
        {
            if (m_OldHorizontalInput != horizontalInput)
            {
                m_NewData.LoadAction(0, m_FrameIndex, Mathf.RoundToInt(horizontalInput), Vector3.zero);
                m_OldHorizontalInput = horizontalInput;
            }

            if (m_OldVerticalInput != verticalInput)
            {
                m_NewData.LoadAction(1, m_FrameIndex, Mathf.RoundToInt(verticalInput), Vector3.zero);
                m_OldVerticalInput = verticalInput;
            }
        }

        m_FrameIndex++;

        //testing
        if (m_FrameIndex == 100)
        {
            AddNewDataToHistory();
        }
    }

    public bool CheckExistance(int carIndex)
    {
        bool output = false;

        if (carIndex == -1)
        {
            output = SpawnPlayer;
        }
        else if (carIndex >= 0 && carIndex < m_Replays.Length)
        {
            output = m_Replays[carIndex] != null && SceneManager.GetActiveScene().buildIndex == m_Replays[carIndex].MapIndex;
        }
        else
        {
            throw new System.Exception("Bad Car histtory index");
        }

        //Debug.Log(carIndex);
        //Debug.Log(output);

        return output;
    }

    public bool IsSetCameraActive(int carIndex)
    {
        bool output = false;

        if (carIndex == -1)
        {
            output = !SpawnPlayer;
        }
        else if (carIndex == 0 && SpawnPlayer == false)
        {
            output = false;
        }
        else if (carIndex >= 0 && carIndex < m_Replays.Length)
        {
            output = m_Replays[carIndex] != null && SceneManager.GetActiveScene().buildIndex == m_Replays[carIndex].MapIndex;
        }
        else
        {
            throw new System.Exception("Bad Car histtory index");
        }

        //Debug.Log(carIndex);
        //Debug.Log(output);

        return !output;
    }

    public float GetHorizontalInput(int carIndex)
    {
        return m_Replays[carIndex].HorizontalInput;
    }

    public float GetVerticalInput(int carIndex)
    {
        return m_Replays[carIndex].VerticalInput;
    }

    public bool GetBraking(int carIndex)
    {
        return m_Replays[carIndex].IsBraking;
    }

    private void AddNewDataToHistory()
    {
        m_Replays[3] = m_Replays[2];
        m_Replays[2] = m_Replays[1];
        m_Replays[1] = m_Replays[0];
        m_Replays[0] = m_NewData;
    }
}
