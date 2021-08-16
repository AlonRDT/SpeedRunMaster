using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryData
{
    public int MapIndex;
    public float HorizontalInput;
    public float VerticalInput;
    public bool IsBraking;
    public bool IsGrapple;

    private List<int> m_ActionFrame = new List<int>();
    private List<int> m_ActionIndex = new List<int>();
    private List<int> m_ActionValue = new List<int>();
    private List<Vector3> m_HookGapplePoint = new List<Vector3>();

    private int m_CurrentArrayIndex;

    public void Initialize()
    {
        m_CurrentArrayIndex = 0;
    }

    public void LoadAction(int actionIndex, int frameIndex, int actionValue, Vector3 grapplePoint)
    {
        m_ActionIndex.Add(actionIndex);
        m_ActionFrame.Add(frameIndex);
        m_ActionValue.Add(actionValue);

        //Debug.Log(actionIndex);
        //Debug.Log(actionValue);

        if (actionIndex == 3 && actionValue == 1)
        {
            m_HookGapplePoint.Add(grapplePoint);
        }
    }

    public bool ApplyAction(int frameIndex)
    {
        bool output = false;

        if (m_ActionFrame.Count > m_CurrentArrayIndex)
        {
            if (m_ActionFrame[m_CurrentArrayIndex] == frameIndex)
            {
                //Debug.Log(m_CurrentArrayIndex);

                switch (m_ActionIndex[m_CurrentArrayIndex])
                {
                    case 0:
                        HorizontalInput = m_ActionValue[m_CurrentArrayIndex];
                        output = true;
                        m_CurrentArrayIndex++;
                        break;
                    case 1:
                        VerticalInput = m_ActionValue[m_CurrentArrayIndex];
                        //Debug.Log(VerticalInput);
                        output = true;
                        m_CurrentArrayIndex++;
                        break;
                    case 2:
                        IsBraking = m_ActionValue[m_CurrentArrayIndex] == 1;
                        output = true;
                        m_CurrentArrayIndex++;
                        break;
                    case 3:
                        IsGrapple = m_ActionValue[m_CurrentArrayIndex] == 1;
                        output = true;
                        m_CurrentArrayIndex++;
                        break;
                    default:
                        break;
                }
            }
        }

        return output;
    }
}
