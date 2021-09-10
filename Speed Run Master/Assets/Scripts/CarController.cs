using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool m_IsBreaking;
    private bool m_IsJumping;
    private bool m_IsGrappling;
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

    [System.Serializable]
    public class StatPowerup
    {
        public Stats modifiers;
        public string PowerUpID;
        public float ElapsedTime;
        public float MaxTime;
    }

    [System.Serializable]
    public struct Stats
    {
        [Header("Movement Settings")]
        [Min(0.001f), Tooltip("Top speed attainable when moving forward.")]
        public float TopSpeed;

        [Tooltip("How quickly the kart reaches top speed.")]
        public float Acceleration;

        [Min(0.001f), Tooltip("Top speed attainable when moving backward.")]
        public float ReverseSpeed;

        [Tooltip("How quickly the kart reaches top speed, when moving backward.")]
        public float ReverseAcceleration;

        [Tooltip("How quickly the kart starts accelerating from 0. A higher number means it accelerates faster sooner.")]
        [Range(0.2f, 1)]
        public float AccelerationCurve;

        [Tooltip("How quickly the kart slows down when the brake is applied.")]
        public float Braking;

        [Tooltip("How quickly the kart will reach a full stop when no inputs are made.")]
        public float CoastingDrag;

        [Range(0.0f, 1.0f)]
        [Tooltip("The amount of side-to-side friction.")]
        public float Grip;

        [Tooltip("How tightly the kart can turn left or right.")]
        public float Steer;

        [Tooltip("Additional gravity for when the kart is in the air.")]
        public float AddedGravity;

        // allow for stat adding for powerups.
        public static Stats operator +(Stats a, Stats b)
        {
            return new Stats
            {
                Acceleration = a.Acceleration + b.Acceleration,
                AccelerationCurve = a.AccelerationCurve + b.AccelerationCurve,
                Braking = a.Braking + b.Braking,
                CoastingDrag = a.CoastingDrag + b.CoastingDrag,
                AddedGravity = a.AddedGravity + b.AddedGravity,
                Grip = a.Grip + b.Grip,
                ReverseAcceleration = a.ReverseAcceleration + b.ReverseAcceleration,
                ReverseSpeed = a.ReverseSpeed + b.ReverseSpeed,
                TopSpeed = a.TopSpeed + b.TopSpeed,
                Steer = a.Steer + b.Steer,
            };
        }
    }

    public Rigidbody Rigidbody { get; private set; }
    public float AirPercent { get; private set; }
    public float GroundPercent { get; private set; }

    public Stats baseStats = new Stats
    {
        TopSpeed = 10f,
        Acceleration = 5f,
        AccelerationCurve = 4f,
        Braking = 10f,
        ReverseAcceleration = 5f,
        ReverseSpeed = 5f,
        Steer = 5f,
        CoastingDrag = 4f,
        Grip = .95f,
        AddedGravity = 1f,
    };

    [Header("Vehicle Physics")]
    [Tooltip("The transform that determines the position of the kart's mass.")]
    public Transform CenterOfMass;

    [Range(0.0f, 20.0f), Tooltip("Coefficient used to reorient the kart in the air. The higher the number, the faster the kart will readjust itself along the horizontal plane.")]
    public float AirborneReorientationCoefficient = 3.0f;

    [Header("Drifting")]
    [Range(0.01f, 1.0f), Tooltip("The grip value when drifting.")]
    public float DriftGrip = 0.4f;
    [Range(0.0f, 10.0f), Tooltip("Additional steer when the kart is drifting.")]
    public float DriftAdditionalSteer = 5.0f;
    [Range(1.0f, 30.0f), Tooltip("The higher the angle, the easier it is to regain full grip.")]
    public float MinAngleToFinishDrift = 10.0f;
    [Range(0.01f, 0.99f), Tooltip("Mininum speed percentage to switch back to full grip.")]
    public float MinSpeedPercentToFinishDrift = 0.5f;
    [Range(1.0f, 20.0f), Tooltip("The higher the value, the easier it is to control the drift steering.")]
    public float DriftControl = 10.0f;
    [Range(0.0f, 20.0f), Tooltip("The lower the value, the longer the drift will last without trying to control it by steering.")]
    public float DriftDampening = 10.0f;

    [Header("Suspensions")]
    [Tooltip("The maximum extension possible between the kart's body and the wheels.")]
    [Range(0.0f, 1.0f)]
    public float SuspensionHeight = 0.2f;
    [Range(10.0f, 100000.0f), Tooltip("The higher the value, the stiffer the suspension will be.")]
    public float SuspensionSpring = 20000.0f;
    [Range(0.0f, 5000.0f), Tooltip("The higher the value, the faster the kart will stabilize itself.")]
    public float SuspensionDamp = 500.0f;
    [Tooltip("Vertical offset to adjust the position of the wheels relative to the kart's body.")]
    [Range(-1.0f, 1.0f)]
    public float WheelsPositionVerticalOffset = 0.0f;

    const float k_NullInput = 0.01f;
    const float k_NullSpeed = 0.01f;
    Vector3 m_VerticalReference = Vector3.up;

    // Drift params
    public bool WantsToDrift { get; private set; } = false;
    public bool IsDrifting { get; private set; } = false;
    float m_CurrentGrip = 1.0f;
    float m_DriftTurningPower = 0.0f;
    float m_PreviousGroundPercent = 1.0f;

    // can the kart move?
    bool m_CanMove = true;
    List<StatPowerup> m_ActivePowerupList = new List<StatPowerup>();
    Stats m_FinalStats;

    Quaternion m_LastValidRotation;
    Vector3 m_LastValidPosition;
    Vector3 m_LastCollisionNormal;
    bool m_HasCollision;
    bool m_InAir = false;

    public void AddPowerup(StatPowerup statPowerup) => m_ActivePowerupList.Add(statPowerup);
    public void SetCanMove(bool move) => m_CanMove = move;
    public float GetMaxSpeed() => Mathf.Max(m_FinalStats.TopSpeed, m_FinalStats.ReverseSpeed);

    void UpdateSuspensionParams(WheelCollider wheel)
    {
        wheel.suspensionDistance = SuspensionHeight;
        wheel.center = new Vector3(0.0f, WheelsPositionVerticalOffset, 0.0f);
        JointSpring spring = wheel.suspensionSpring;
        spring.spring = SuspensionSpring;
        spring.damper = SuspensionDamp;
        wheel.suspensionSpring = spring;
    }


    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        m_IsJumping = false;
        m_IsGrappling = false;
        SetCanMove(false);

        UpdateSuspensionParams(frontLeftWheelCollider);
        UpdateSuspensionParams(frontRightWheelCollider);
        UpdateSuspensionParams(rearLeftWheelCollider);
        UpdateSuspensionParams(rearRightWheelCollider);

        m_CurrentGrip = baseStats.Grip;

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

        if (m_IsPlayer == false)
        {
            Destroy(m_PickupHandler.GetComponent<SphereCollider>());
            Destroy(m_PickupHandler.GetComponent<PlayerInput>());
        }

        m_Input = new GameInput();
        m_Input.Enable();

    }

    private void FixedUpdate()
    {
        UpdateSuspensionParams(frontLeftWheelCollider);
        UpdateSuspensionParams(frontRightWheelCollider);
        UpdateSuspensionParams(rearLeftWheelCollider);
        UpdateSuspensionParams(rearRightWheelCollider);


        GetInput();
        TickPowerups();

        // apply our physics properties
        Rigidbody.centerOfMass = transform.InverseTransformPoint(CenterOfMass.position);

        int groundedCount = 0;
        if (frontLeftWheelCollider.isGrounded && frontLeftWheelCollider.GetGroundHit(out WheelHit hit))
            groundedCount++;
        if (frontRightWheelCollider.isGrounded && frontRightWheelCollider.GetGroundHit(out hit))
            groundedCount++;
        if (rearLeftWheelCollider.isGrounded && rearLeftWheelCollider.GetGroundHit(out hit))
            groundedCount++;
        if (rearRightWheelCollider.isGrounded && rearRightWheelCollider.GetGroundHit(out hit))
            groundedCount++;

        // calculate how grounded and airborne we are
        GroundPercent = (float)groundedCount / 4.0f;
        AirPercent = 1 - GroundPercent;

        SetCanMove(m_Manager.CanCarMove());

        MoveVehicle(verticalInput, m_IsBreaking, horizontalInput);

        GroundAirbourne();

        m_PreviousGroundPercent = GroundPercent;
    }

    void GroundAirbourne()
    {
        // while in the air, fall faster
        if (AirPercent >= 1)
        {
            Rigidbody.velocity += Physics.gravity * Time.fixedDeltaTime * m_FinalStats.AddedGravity;
        }
    }

    //gets the input of the keyboard/keypad
    private void GetInput()
    {
        WantsToDrift = false;

        if (m_IsPlayer == true)
        {
            //that's how you write the code for the gamepad. started is when click - canceled is when you let go
            horizontalInput = m_Input.Gameplay.TurnCar.ReadValue<float>();
            if (horizontalInput > 0)
            {
                horizontalInput = 1;
            }
            else if (horizontalInput < 0)
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
            m_IsBreaking = m_Manager.GetBrakingInput();
            bool jump = m_Manager.GetJumpInput();
            if (jump == true && jump != m_IsJumping)
            {
                m_JumpPickup.ManualJump();
            }
            m_IsJumping = jump;
            //follows the changes in the button activation click the player did
            bool grapple = m_Manager.GetJGrappleInput();
            if (grapple != m_IsGrappling)
            {
                if (grapple == true)
                {
                    m_GrapplingHookPickup.ManualStartGrapple(m_Manager.GetJGrapplePoint());
                }
                else
                {
                    m_GrapplingHookPickup.ManualStopGrapple();
                }
            }
            m_IsGrappling = grapple;
        }

        WantsToDrift = m_IsBreaking && Vector3.Dot(Rigidbody.velocity, transform.forward) > 0.0f;
    }

    void TickPowerups()
    {
        // remove all elapsed powerups
        m_ActivePowerupList.RemoveAll((p) => { return p.ElapsedTime > p.MaxTime; });

        // zero out powerups before we add them all up
        var powerups = new Stats();

        // add up all our powerups
        for (int i = 0; i < m_ActivePowerupList.Count; i++)
        {
            var p = m_ActivePowerupList[i];

            // add elapsed time
            p.ElapsedTime += Time.fixedDeltaTime;

            // add up the powerups
            powerups += p.modifiers;
        }

        // add powerups to our final stats
        m_FinalStats = baseStats + powerups;

        // clamp values in finalstats
        m_FinalStats.Grip = Mathf.Clamp(m_FinalStats.Grip, 0, 1);
    }

    public void Break(InputAction.CallbackContext context)
    {
        if (m_IsPlayer == true)
        {
            //that's how you write the code for the gamepad. started is when click - canceled is when you let go
            if (context.phase == InputActionPhase.Started)
            {
                m_IsBreaking = true;
                m_Manager.RegisterBreaking(true);
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                m_IsBreaking = false;
                m_Manager.RegisterBreaking(false);
            }
        }
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    public bool IsPlayer()
    {
        return m_IsPlayer;
    }

    void MoveVehicle(float accelerate, bool brake, float turnInput)
    {
        float accelInput = accelerate;

        // manual acceleration curve coefficient scalar
        float accelerationCurveCoeff = 5;
        Vector3 localVel = transform.InverseTransformVector(Rigidbody.velocity);

        bool accelDirectionIsFwd = accelInput >= 0;
        bool localVelDirectionIsFwd = localVel.z >= 0;

        // use the max speed for the direction we are going--forward or reverse.
        float maxSpeed = localVelDirectionIsFwd ? m_FinalStats.TopSpeed : m_FinalStats.ReverseSpeed;
        float accelPower = accelDirectionIsFwd ? m_FinalStats.Acceleration : m_FinalStats.ReverseAcceleration;

        float currentSpeed = Rigidbody.velocity.magnitude;
        float accelRampT = currentSpeed / maxSpeed;
        float multipliedAccelerationCurve = m_FinalStats.AccelerationCurve * accelerationCurveCoeff;
        float accelRamp = Mathf.Lerp(multipliedAccelerationCurve, 1, accelRampT * accelRampT);

        bool isBraking = brake;

        // if we are braking (moving reverse to where we are going)
        // use the braking accleration instead
        float finalAccelPower = isBraking ? m_FinalStats.Braking : accelPower;

        float finalAcceleration = finalAccelPower * accelRamp;

        // apply inputs to forward/backward
        float turningPower = IsDrifting ? m_DriftTurningPower : turnInput * m_FinalStats.Steer;

        Quaternion turnAngle = Quaternion.AngleAxis(turningPower, transform.up);
        Vector3 fwd = turnAngle * transform.forward;
        Vector3 movement = fwd * accelInput * finalAcceleration * ((m_HasCollision || GroundPercent > 0.0f) ? 1.0f : 0.0f);

        // forward movement
        bool wasOverMaxSpeed = currentSpeed >= maxSpeed;

        // if over max speed, cannot accelerate faster.
        if (wasOverMaxSpeed && !isBraking)
            movement *= 0.0f;

        Vector3 newVelocity = Rigidbody.velocity + movement * Time.fixedDeltaTime;


        //  clamp max speed if we are on ground
        if (GroundPercent > 0.0f && !wasOverMaxSpeed)
        {
            newVelocity = Vector3.ClampMagnitude(newVelocity, maxSpeed);
        }

        // coasting is when we aren't touching accelerate
        if (Mathf.Abs(accelInput) < k_NullInput && GroundPercent > 0.0f)
        {
            newVelocity = Vector3.MoveTowards(newVelocity, new Vector3(0, Rigidbody.velocity.y, 0), Time.fixedDeltaTime * m_FinalStats.CoastingDrag);
        }

        Rigidbody.velocity = newVelocity;

        // Drift
        if (GroundPercent > 0.0f)
        {
            if (m_InAir)
            {
                m_InAir = false;
            }

            if (brake == false)
            {
                newVelocity.y = Rigidbody.velocity.y;
            }
            else
            {
                ApplyBreaking();
            }

            // manual angular velocity coefficient
            float angularVelocitySteering = 0.4f;
            float angularVelocitySmoothSpeed = 20f;

            // turning is reversed if we're going in reverse and pressing reverse
            if (!localVelDirectionIsFwd && !accelDirectionIsFwd)
                angularVelocitySteering *= -1.0f;

            var angularVel = Rigidbody.angularVelocity;

            // move the Y angular velocity towards our target
            angularVel.y = Mathf.MoveTowards(angularVel.y, turningPower * angularVelocitySteering, Time.fixedDeltaTime * angularVelocitySmoothSpeed);

            // apply the angular velocity
            Rigidbody.angularVelocity = angularVel;

            // rotate rigidbody's velocity as well to generate immediate velocity redirection
            // manual velocity steering coefficient
            float velocitySteering = 25f;

            // If the karts lands with a forward not in the velocity direction, we start the drift
            if (GroundPercent >= 0.0f && m_PreviousGroundPercent < 0.1f)
            {
                Vector3 flattenVelocity = Vector3.ProjectOnPlane(Rigidbody.velocity, m_VerticalReference).normalized;
                if (Vector3.Dot(flattenVelocity, transform.forward * Mathf.Sign(accelInput)) < Mathf.Cos(MinAngleToFinishDrift * Mathf.Deg2Rad))
                {
                    IsDrifting = true;
                    m_CurrentGrip = DriftGrip;
                    m_DriftTurningPower = 0.0f;
                }
            }

            // Drift Management
            if (!IsDrifting)
            {
                if ((WantsToDrift || isBraking) && currentSpeed > maxSpeed * MinSpeedPercentToFinishDrift)
                {
                    IsDrifting = true;
                    m_DriftTurningPower = turningPower + (Mathf.Sign(turningPower) * DriftAdditionalSteer);
                    m_CurrentGrip = DriftGrip;
                }
            }

            if (IsDrifting)
            {
                float turnInputAbs = Mathf.Abs(turnInput);
                if (turnInputAbs < k_NullInput)
                    m_DriftTurningPower = Mathf.MoveTowards(m_DriftTurningPower, 0.0f, Mathf.Clamp01(DriftDampening * Time.fixedDeltaTime));

                // Update the turning power based on input
                float driftMaxSteerValue = m_FinalStats.Steer + DriftAdditionalSteer;
                m_DriftTurningPower = Mathf.Clamp(m_DriftTurningPower + (turnInput * Mathf.Clamp01(DriftControl * Time.fixedDeltaTime)), -driftMaxSteerValue, driftMaxSteerValue);

                bool facingVelocity = Vector3.Dot(Rigidbody.velocity.normalized, transform.forward * Mathf.Sign(accelInput)) > Mathf.Cos(MinAngleToFinishDrift * Mathf.Deg2Rad);

                bool canEndDrift = true;
                if (isBraking)
                    canEndDrift = false;
                else if (!facingVelocity)
                    canEndDrift = false;
                else if (turnInputAbs >= k_NullInput && currentSpeed > maxSpeed * MinSpeedPercentToFinishDrift)
                    canEndDrift = false;

                if (canEndDrift || currentSpeed < k_NullSpeed)
                {
                    // No Input, and car aligned with speed direction => Stop the drift
                    IsDrifting = false;
                    m_CurrentGrip = m_FinalStats.Grip;
                }

            }

            // rotate our velocity based on current steer value
            Rigidbody.velocity = Quaternion.AngleAxis(turningPower * Mathf.Sign(localVel.z) * velocitySteering * m_CurrentGrip * Time.fixedDeltaTime, transform.up) * Rigidbody.velocity;
        }
        else
        {
            m_InAir = true;
        }

        bool validPosition = false;
        if (Physics.Raycast(transform.position + (transform.up * 0.1f), -transform.up, out RaycastHit hit, 3.0f, 1 << 9 | 1 << 10 | 1 << 11)) // Layer: ground (9) / Environment(10) / Track (11)
        {
            Vector3 lerpVector = (m_HasCollision && m_LastCollisionNormal.y > hit.normal.y) ? m_LastCollisionNormal : hit.normal;
            m_VerticalReference = Vector3.Slerp(m_VerticalReference, lerpVector, Mathf.Clamp01(AirborneReorientationCoefficient * Time.fixedDeltaTime * (GroundPercent > 0.0f ? 10.0f : 1.0f)));    // Blend faster if on ground
        }
        else
        {
            Vector3 lerpVector = (m_HasCollision && m_LastCollisionNormal.y > 0.0f) ? m_LastCollisionNormal : Vector3.up;
            m_VerticalReference = Vector3.Slerp(m_VerticalReference, lerpVector, Mathf.Clamp01(AirborneReorientationCoefficient * Time.fixedDeltaTime));
        }

        validPosition = GroundPercent > 0.7f && !m_HasCollision && Vector3.Dot(m_VerticalReference, Vector3.up) > 0.9f;

        // Airborne / Half on ground management
        if (GroundPercent < 0.7f)
        {
            Rigidbody.angularVelocity = new Vector3(0.0f, Rigidbody.angularVelocity.y * 0.98f, 0.0f);
            Vector3 finalOrientationDirection = Vector3.ProjectOnPlane(transform.forward, m_VerticalReference);
            finalOrientationDirection.Normalize();
            if (finalOrientationDirection.sqrMagnitude > 0.0f)
            {
                Rigidbody.MoveRotation(Quaternion.Lerp(Rigidbody.rotation, Quaternion.LookRotation(finalOrientationDirection, m_VerticalReference), Mathf.Clamp01(AirborneReorientationCoefficient * Time.fixedDeltaTime)));
            }
        }
        else if (validPosition)
        {
            m_LastValidPosition = transform.position;
            m_LastValidRotation.eulerAngles = new Vector3(0.0f, transform.rotation.y, 0.0f);
        }
    }
}
