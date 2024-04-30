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
        private float maxWheelRotAngle = 40f;
        private float driftAngle = 120f;
        private float slipAllowance = 0.5f;


        private void Start()
        {
            playerBody = GetComponent<Rigidbody>();
            InstAllSmokeTrailParticles();
        }

        private void Update()
        {
            speed = playerBody.velocity.magnitude;
            CheckInput();
            ApplyMotor();
            ApplySteering();
            ApplyBrake();
            CheckAllSmokeWheelParticles();
            UpdateWheelsPosAndRot();
        }

        private void CheckInput()
        {
            gasInput = Input.GetAxis("Vertical");
            steeringInput = Input.GetAxis("Horizontal");
            slipAngle = Vector3.Angle(transform.forward, playerBody.velocity);
            if (slipAngle < driftAngle)
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

        private void InstAllSmokeTrailParticles()
        {
            wheelParticles.FRWheelFx = InstantiateSmokeFXOnWheel(wheelColliders.FRWheel);
            wheelParticles.FLWheelFx = InstantiateSmokeFXOnWheel(wheelColliders.FLWheel);
            wheelParticles.RRWheelFx = InstantiateSmokeFXOnWheel(wheelColliders.RRWheel);
            wheelParticles.RLWheelFx = InstantiateSmokeFXOnWheel(wheelColliders.RLWheel);
        }

        private ParticleSystem InstantiateSmokeFXOnWheel(WheelCollider wheelCollider)
        {
            var wheelFX = Instantiate(smokePrefab, 
                                            wheelCollider.transform.position - Vector3.up * wheelCollider.radius, 
                                            Quaternion.identity, 
                                            wheelCollider.transform)
                                            .GetComponent<ParticleSystem>();
            return wheelFX;
        }
        
        

        private void CheckAllSmokeWheelParticles()
        {
            WheelHit[] wheelHits = new WheelHit[4];
            wheelColliders.FRWheel.GetGroundHit(out wheelHits[0]);
            wheelColliders.FLWheel.GetGroundHit(out wheelHits[1]);
            wheelColliders.RRWheel.GetGroundHit(out wheelHits[2]);
            wheelColliders.RLWheel.GetGroundHit(out wheelHits[3]);

            CheckSmokeFX(wheelHits[0], wheelParticles.FRWheelFx);
            CheckSmokeFX(wheelHits[1], wheelParticles.FLWheelFx);
            CheckSmokeFX(wheelHits[2], wheelParticles.RRWheelFx);
            CheckSmokeFX(wheelHits[3], wheelParticles.RLWheelFx);
        }

        private void CheckSmokeFX(WheelHit wheelHit, ParticleSystem smokeFx)
        {
            if (Mathf.Abs(wheelHit.sidewaysSlip) + Mathf.Abs(wheelHit.forwardSlip) > slipAllowance)
            {
                smokeFx.Play();
            }
            else
            {
                smokeFx.Stop();
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
            steeringAngle = Mathf.Clamp(steeringAngle, -maxWheelRotAngle, maxWheelRotAngle);
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