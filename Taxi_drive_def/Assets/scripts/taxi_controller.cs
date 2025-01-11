using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody playerRB;
    public WheelColliders colliders;
    public WheelMeshes wheelMeshes;
    public float gasInput;
    public float brakeInput;
    public float steeringInput;
    public float motorPower;
    public float brakePower;
    public float slipAngle;
    private float speed;
    public AnimationCurve steeringCurve;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        speed = playerRB.velocity.magnitude;
        CheckInput();
        ApplyMotor();
        ApplySteering();
        ApplyBrake();
        ApplyWheelPositions();
    }

    void CheckInput()
    {
        gasInput = Input.GetAxis("Vertical");

        steeringInput = Input.GetAxis("Horizontal");

        slipAngle = Vector3.Angle(transform.forward, playerRB.velocity - transform.forward);

        //fixed code to brake even after going on reverse by Andrew Alex 
        float movingDirection = Vector3.Dot(transform.forward, playerRB.velocity);
        if (movingDirection < -0.5f && gasInput > 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else if (movingDirection > 0.5f && gasInput < 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else
        {
            brakeInput = 0;
        }


    }
    void ApplyBrake()
    {
        if (gasInput == 0 && speed > 0.1f) // Frenado automático con fuerza total
        {
            float autoBrakeForce = brakePower; // Usa toda la potencia de frenado
            colliders.wheelFR.brakeTorque = autoBrakeForce * 0.7f;
            colliders.wheelFL.brakeTorque = autoBrakeForce * 0.7f;
            colliders.wheelRR.brakeTorque = autoBrakeForce * 0.3f;
            colliders.wheelRL.brakeTorque = autoBrakeForce * 0.3f;
        }
        else // Frenado manual
        {
            colliders.wheelFR.brakeTorque = brakeInput * brakePower * 0.7f;
            colliders.wheelFL.brakeTorque = brakeInput * brakePower * 0.7f;
            colliders.wheelRR.brakeTorque = brakeInput * brakePower * 0.3f;
            colliders.wheelRL.brakeTorque = brakeInput * brakePower * 0.3f;
        }
    }
    void ApplyMotor()
    {
        if (gasInput != 0)
        {
            colliders.wheelRR.motorTorque = motorPower * gasInput;
            colliders.wheelRL.motorTorque = motorPower * gasInput;
        }
        else
        {
            colliders.wheelRR.motorTorque = 0;
            colliders.wheelRL.motorTorque = 0;
        }
    }
    void ApplySteering()
    {

        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        if (slipAngle < 120f)
        {
            steeringAngle += Vector3.SignedAngle(transform.forward, playerRB.velocity + transform.forward, Vector3.up);
        }
        steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);
        colliders.wheelFR.steerAngle = steeringAngle;
        colliders.wheelFL.steerAngle = steeringAngle;
    }

    void ApplyWheelPositions()
    {
        UpdateWheel(colliders.wheelFR, wheelMeshes.wheelFR);
        UpdateWheel(colliders.wheelFL, wheelMeshes.wheelFL);
        UpdateWheel(colliders.wheelRR, wheelMeshes.wheelRR);
        UpdateWheel(colliders.wheelRL, wheelMeshes.wheelRL);
    }

    void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh)
    {
        Quaternion quat;
        Vector3 position;
        coll.GetWorldPose(out position, out quat);
        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = quat;
    }
}
[System.Serializable]
public class WheelColliders
{
    public WheelCollider wheelFR;
    public WheelCollider wheelFL;
    public WheelCollider wheelRR;
    public WheelCollider wheelRL;
}
[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer wheelFR;
    public MeshRenderer wheelFL;
    public MeshRenderer wheelRR;
    public MeshRenderer wheelRL;
}
