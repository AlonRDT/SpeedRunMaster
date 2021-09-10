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
        //CheckExistance is to check if theres a player and/or ghost in the game
        if (m_Manager.CheckExistance(m_IsPlayer) == false)
        {
            Destroy(gameObject);
        }
        //checks if the camera is on the ghost or the player
        else if (m_Manager.IsSetCameraActive(m_IsPlayer) == true)
        {
            m_Camera.SetActive(true);
        }

        if(m_IsPlayer == false)
        {
            Destroy(m_PickupHandler.GetComponent<SphereCollider>());
            Destroy(m_PickupHandler.GetComponent<PlayerInput>());
        }

        //make the center of mass be lower in the car so the car wont flip easily
        Rb.centerOfMass = new Vector3(0, centerOfMass, 0);


        m_Input = new GameInput();
        m_Input.Enable();

    }

    private void FixedUpdate()
    {
        //cancarmove - lets the car move only when GO appears
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
            //that's how you write the code for the gamepad. started is when click - canceled is when you let go
            horizontalInput = m_Input.Gameplay.TurnCar.ReadValue<float>();
            if (horizontalInput > 0)
            {
                horizontalInput = 1;
            }
            else if(horizontalInput < 0)
            {
                horizontalInput = -1;
            }
            //readvalue = getaxis for gamepad controll
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
            //gets the history input for the ghost
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
            //follows the changes in the button activation click the player did
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
            //that's how you write the code for the gamepad. started is when click - canceled is when you let go
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
        //Torque is the result of applying a force rotation. similar to velocity
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
        //steerAngle = Steering angle in degrees, always around the local y-axis.

        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        //sends UpdateSingleWheel the position of each wheel 
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        //reset the position of the wheels
        Vector3 pos;
        Quaternion rot;
        //GetWorldPose - Gets the world space pose of the wheel accounting for ground contact, suspension limits, steer angle, and rotation angle (angles in degrees)
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    public bool IsPlayer()
    {
        return m_IsPlayer;
    }
}