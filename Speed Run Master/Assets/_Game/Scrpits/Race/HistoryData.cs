using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryData
{
    public int MapIndex;
    public float RaceTime;
    public float HorizontalInput;
    public float VerticalInput;
    public bool IsBraking;
    public bool IsJump;
    public bool IsGrapple;

    private List<float> m_ActionTime = new List<float>();
    private List<int> m_ActionIndex = new List<int>();
    private List<int> m_ActionValue = new List<int>();
    private List<Vector3> m_HookGapplePoint = new List<Vector3>();

    private int m_CurrentArrayIndex;
    private int m_CurrentGrappleIndex;

    public void Initialize()
    {
        m_CurrentArrayIndex = 0;
        m_CurrentGrappleIndex = 0;
    }

    public void RegisterHorizontalAction(float actionTime, int actionValue)
    {
        m_ActionIndex.Add(0);
        m_ActionTime.Add(actionTime);
        m_ActionValue.Add(actionValue);
    }

    public void RegisterVerticalAction(float actionTime, int actionValue)
    {
        //Debug.Log(actionTime);
        m_ActionIndex.Add(1);
        m_ActionTime.Add(actionTime);
        m_ActionValue.Add(actionValue);
    }

    public void RegisterBrakeAction(float ActionTime, bool start)
    {
        //Debug.Log("break " + start);
        m_ActionIndex.Add(2);
        m_ActionTime.Add(ActionTime);
        if (start == true)
        {
            m_ActionValue.Add(1);
        }
        else
        {
            m_ActionValue.Add(0);
        }
    }

    public void RegisterJumpAction(float ActionTime, bool start)
    {
        //Debug.Log("jump " + start);
        m_ActionIndex.Add(3);
        m_ActionTime.Add(ActionTime);
        if (start == true)
        {
            m_ActionValue.Add(1);
        }
        else
        {
            m_ActionValue.Add(0);
        }
    }

    public void ResgterGrappleAction(float ActionTime, bool start, Vector3 grapplePoint)
    {
        //Debug.Log("grapple " + start);
        m_ActionIndex.Add(4);
        m_ActionTime.Add(ActionTime);
        if (start == true)
        {
            m_ActionValue.Add(1);
            m_HookGapplePoint.Add(grapplePoint);
        }
        else
        {
            m_ActionValue.Add(0);
        }
    }

    public int ApplyAction(float timeElapsesd)
    {
        int output = -1;
        //Debug.Log(m_CurrentArrayIndex);
        if (m_ActionIndex.Count > m_CurrentArrayIndex)
        {
            if (timeElapsesd >= m_ActionTime[m_CurrentArrayIndex])
            {
                //Debug.Log(m_ActionTime[m_CurrentArrayIndex] + " > " + timeElapsesd);

                switch (m_ActionIndex[m_CurrentArrayIndex])
                {
                    case 0:
                        HorizontalInput = m_ActionValue[m_CurrentArrayIndex];
                        output = 0;
                        m_CurrentArrayIndex++;
                        break;
                    case 1:
                        VerticalInput = m_ActionValue[m_CurrentArrayIndex];
                        //Debug.Log(VerticalInput);
                        output = 1;
                        m_CurrentArrayIndex++;
                        break;
                    case 2:
                        IsBraking = m_ActionValue[m_CurrentArrayIndex] == 1;
                        output = 2;
                        m_CurrentArrayIndex++;
                        break;
                    case 3:
                        IsJump = m_ActionValue[m_CurrentArrayIndex] == 1;
                        output = 3;
                        m_CurrentArrayIndex++;
                        break;
                    case 4:
                        IsGrapple = m_ActionValue[m_CurrentArrayIndex] == 1;
                        output = 4;
                        m_CurrentArrayIndex++;
                        break;
                    default:
                        break;
                }
            }
        }

        return output;
    }

    public Vector3 GetGrapplePoint()
    {
        m_CurrentGrappleIndex++;
        return m_HookGapplePoint[m_CurrentGrappleIndex - 1];
    }
}
