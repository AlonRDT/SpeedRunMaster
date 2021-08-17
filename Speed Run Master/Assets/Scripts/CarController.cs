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
    private bool isJumping;
    private bool isGrappling;
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

    [SerializeField] private bool m_IsPlayer;
    [SerializeField] private GameManager m_Manager;
    [SerializeField] private GameObject m_Camera;
    [SerializeField] private GameObject m_PickupHandler;
    [SerializeField] private Pickup_Hook m_GrapplingHookPickup;
    [SerializeField] private Pickup_Jump m_JumpPickup;

    private void Start()
    {
        isJumping = false;
        isGrappling = false;
        if (m_Manager.CheckExistance(m_IsPlayer) == false)
        {
            Destroy(gameObject);
        }
        else if (m_Manager.IsSetCameraActive(m_IsPlayer) == true)
        {
            m_Camera.SetActive(true);
        }

        if(m_IsPlayer == false)
        {
            Destroy(m_PickupHandler.GetComponent<SphereCollider>());
            Destroy(m_PickupHandler.GetComponent<PlayerInput>());
        }

        Rb.centerOfMass = new Vector3(0, centerOfMass, 0);
        m_Input = new GameInput();
        m_Input.Enable();
    }

    private void FixedUpdate()
    {
        if (m_Manager.CanCarMove() == false) return;
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    //gets the input of the keyboard/keypad
    private void GetInput()
    {
        if (m_IsPlayer == true)
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
            horizontalInput = m_Manager.GetHorizontalInput();
            verticalInput = m_Manager.GetVerticalInput();
            //Debug.Log(verticalInput);
            isBreaking = m_Manager.GetBrakingInput();
            bool jump = m_Manager.GetJumpInput();
            if(jump == true && jump != isJumping)
            {
                m_JumpPickup.ManualJump();
            }
            isJumping = jump;
            bool grapple = m_Manager.GetJGrappleInput();
            if(grapple != isGrappling)
            {
                if(grapple == true)
                {
                    m_GrapplingHookPickup.ManualStartGrapple(m_Manager.GetJGrapplePoint());
                }
                else
                {
                    m_GrapplingHookPickup.ManualStopGrapple();
                }
            }
            isGrappling = grapple;
        }
    }

    public void Break(InputAction.CallbackContext context)
    {
        if (m_IsPlayer == true)
        {
            if (context.phase == InputActionPhase.Started)
            {
                isBreaking = true;
                m_Manager.RegisterBreaking(true);
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                isBreaking = false;
                m_Manager.RegisterBreaking(false);
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