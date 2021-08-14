using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField] private float m_ReturnCameraToDefaultPositionSpeed;
    [SerializeField] private float m_MoveCameraSpeed;
    [SerializeField] private GameObject m_YRotationBase;
    private GameInput m_Input;

    // Start is called before the first frame update
    void Start()
    {
        m_Input = new GameInput();
        m_Input.Gameplay.Enable();
    }

    private void FixedUpdate()
    {
        Vector2 cameraMovementVector = m_Input.Gameplay.TurnCamera.ReadValue<Vector2>();

        if (cameraMovementVector.x == 0 && cameraMovementVector.y == 0)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, m_ReturnCameraToDefaultPositionSpeed * Time.fixedDeltaTime);
            m_YRotationBase.transform.localRotation = Quaternion.Lerp(m_YRotationBase.transform.localRotation, Quaternion.identity, m_ReturnCameraToDefaultPositionSpeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.Rotate(new Vector2(-cameraMovementVector.y, 0) * m_MoveCameraSpeed, Space.Self);
            m_YRotationBase.transform.Rotate(new Vector2(0, cameraMovementVector.x) * m_MoveCameraSpeed, Space.Self);
        }
    }
}
