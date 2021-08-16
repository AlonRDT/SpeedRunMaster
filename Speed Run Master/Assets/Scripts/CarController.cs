using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public Rigidbody Rb;

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;
    public float centerOfMass = 0.5f;

    public float motorForce;
    public float breakForce;
    public float maxSteerAngle;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    public Transform frontLeftWheelTransform;
    public Transform frontRightWheeTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    private GameInput m_Input;

    [SerializeField] private int m_HistoryIndex;
    [SerializeField] private GameManager m_Manager;
    [SerializeField] private GameObject m_Camera;

    private void Start()
    {
        if (m_Manager.CheckExistance(m_HistoryIndex) == false)
        {
            Destroy(gameObject);
        }
        else if (m_Manager.IsSetCameraActive(m_HistoryIndex) == true)
        {
            m_Camera.SetActive(true);
        }

        Rb.centerOfMass = new Vector3(0, centerOfMass, 0);
        m_Input = new GameInput();
        m_Input.Enable();
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();


    }
    //gets the input of the keyboard/keypad
    private void GetInput()
    {
        if (m_HistoryIndex == -1)
        {
            horizontalInput = m_Input.Gameplay.TurnCar.ReadValue<float>();
            if (horizontalInput > 0)
            {
                horizontalInput = 1;
            }
            else if(horizontalInput < 0)
            {
                horizontalInput = -1;
            }
            verticalInput = m_Input.Gameplay.MoveCar.ReadValue<float>();
            if (verticalInput > 0)
            {
                verticalInput = 1;
            }
            else if (verticalInput < 0)
            {
                verticalInput = -1;
            }
        }
        else
        {
            horizontalInput = m_Manager.GetHorizontalInput(m_HistoryIndex);
            verticalInput = m_Manager.GetVerticalInput(m_HistoryIndex);
            //Debug.Log(verticalInput);
            isBreaking = m_Manager.GetBraking(m_HistoryIndex);
        }
    }

    public void Break(InputAction.CallbackContext context)
    {
        if (m_HistoryIndex == -1)
        {
            if (context.phase == InputActionPhase.Started)
            {
                isBreaking = true;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                isBreaking = false;
            }
        }
    }

    private void HandleMotor()
    {
        //Torque is the result of applying a force at a lever/distance. similar to velocity
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}