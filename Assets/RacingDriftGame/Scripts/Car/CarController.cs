using System;
using UnityEngine;

namespace RacingDriftGame.Scripts.Car
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private WheelColliders wheelColliders;
        [SerializeField] private WheelMeshes wheelMeshes;
        [SerializeField] private WheelParticles wheelParticles;
        [SerializeField] private AnimationCurve steeringCurve;
        [SerializeField] private GameObject smokePrefab;
        private Rigidbody playerBody;
        private float speed;
        private const float motorPower = 1000f;
        private float brakePower = 500000f;
        private float brakeInput;
        private float slipAngle;
        private float gasInput;
        private float steeringInput;


        private void Start()
        {
            playerBody = GetComponent<Rigidbody>();
            InstantiateSmokeParticles();
        }

        private void Update()
        {
            speed = playerBody.velocity.magnitude;
            CheckInput();
            ApplyMotor();
            ApplySteering();
            ApplyBrake();
            CheckSmokeParticles();
            UpdateWheelsPosAndRot();
        }

        private void CheckInput()
        {
            gasInput = Input.GetAxis("Vertical");
            steeringInput = Input.GetAxis("Horizontal");
            slipAngle = Vector3.Angle(transform.forward, playerBody.velocity);
            if (slipAngle < 120f)
            {
                if (gasInput < 0)
                {
                    brakeInput = Mathf.Abs(gasInput);
                    gasInput = 0;
                }
            }
            else
            {
                brakeInput = 0;
            }
        }

        private void InstantiateSmokeParticles()
        {
            // var particles = new ParticleSystem[4];
            wheelParticles.FRWheelFx = Instantiate(smokePrefab, wheelColliders.FRWheel.transform.position - Vector3.up * wheelColliders.FRWheel.radius, Quaternion.identity, wheelColliders.FRWheel.transform)
                .GetComponent<ParticleSystem>();
            wheelParticles.FLWheelFx = Instantiate(smokePrefab, wheelColliders.FLWheel.transform.position - Vector3.up * wheelColliders.FLWheel.radius, Quaternion.identity, wheelColliders.FLWheel.transform)
                .GetComponent<ParticleSystem>();
            wheelParticles.RRWheelFx = Instantiate(smokePrefab, wheelColliders.RRWheel.transform.position - Vector3.up * wheelColliders.RRWheel.radius, Quaternion.identity, wheelColliders.RRWheel.transform)
                .GetComponent<ParticleSystem>();
            wheelParticles.RLWheelFx = Instantiate(smokePrefab, wheelColliders.RLWheel.transform.position - Vector3.up * wheelColliders.RLWheel.radius, Quaternion.identity, wheelColliders.RLWheel.transform)
                .GetComponent<ParticleSystem>();
        }

        private void CheckSmokeParticles()
        {
            WheelHit[] wheelHits = new WheelHit[4];
            wheelColliders.FRWheel.GetGroundHit(out wheelHits[0]);
            wheelColliders.FLWheel.GetGroundHit(out wheelHits[1]);
            wheelColliders.RRWheel.GetGroundHit(out wheelHits[2]);
            wheelColliders.RLWheel.GetGroundHit(out wheelHits[3]);

            float slipAllowance = 0.5f;
            
            if (Mathf.Abs(wheelHits[0].sidewaysSlip) + Mathf.Abs(wheelHits[0].forwardSlip) > slipAllowance)
            {
                 wheelParticles.FRWheelFx.Play();
            }
            else
            {
                wheelParticles.FRWheelFx.Stop();
            }
            
            if (Mathf.Abs(wheelHits[1].sidewaysSlip) + Mathf.Abs(wheelHits[1].forwardSlip) > slipAllowance)
            {
                wheelParticles.FLWheelFx.Play();
            }
            else
            {
                wheelParticles.FLWheelFx.Stop();
            }
            
            if (Mathf.Abs(wheelHits[2].sidewaysSlip) + Mathf.Abs(wheelHits[2].forwardSlip) > slipAllowance)
            {
                wheelParticles.RRWheelFx.Play();
            }
            else
            {
                wheelParticles.RRWheelFx.Stop();
            }
            
            if (Mathf.Abs(wheelHits[3].sidewaysSlip) + Mathf.Abs(wheelHits[3].forwardSlip) > slipAllowance)
            {
                wheelParticles.RLWheelFx.Play();
            }
            else
            {
                wheelParticles.RLWheelFx.Stop();
            }
        }

        private void ApplyBrake()
        {
            wheelColliders.FRWheel.brakeTorque = brakeInput * brakePower * 0.7f;
            wheelColliders.FLWheel.brakeTorque = brakeInput * brakePower * 0.7f;
            wheelColliders.RRWheel.brakeTorque = brakeInput * brakePower * 0.3f;
            wheelColliders.RLWheel.brakeTorque = brakeInput * brakePower * 0.3f;
        }

        private void ApplyMotor()
        {
            wheelColliders.RRWheel.motorTorque = motorPower * gasInput;
            wheelColliders.RLWheel.motorTorque = motorPower * gasInput;
        }

        private void ApplySteering()
        {
            var steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
            steeringAngle +=
                Vector3.SignedAngle(transform.forward, playerBody.velocity + transform.forward, Vector3.up);
            steeringAngle = Mathf.Clamp(steeringAngle, -40f, 40f);
            wheelColliders.FRWheel.steerAngle = steeringAngle;
            wheelColliders.FLWheel.steerAngle = steeringAngle;
        }

        private void UpdateWheelsPosAndRot()
        {
            UpdateWheel(wheelColliders.FLWheel, wheelMeshes.FLWheel);
            UpdateWheel(wheelColliders.FRWheel, wheelMeshes.FRWheel);
            UpdateWheel(wheelColliders.RLWheel, wheelMeshes.RLWheel);
            UpdateWheel(wheelColliders.RRWheel, wheelMeshes.RRWheel);
        }

        private void UpdateWheel(WheelCollider wheelCollider, MeshRenderer wheelMesh)
        {
            var wheelTransform = wheelMesh.transform;
            wheelCollider.GetWorldPose(out var position, out var quaternion);
            wheelTransform.position = position;
            wheelTransform.rotation = quaternion;
        }
    }
}