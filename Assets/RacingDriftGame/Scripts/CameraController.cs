using System;
using System.Collections;
using RacingDriftGame.Scripts.Car;
using UnityEngine;

namespace RacingDriftGame.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        private Rigidbody playerRB;
        private Vector3 cameraOffset = new (0,2,0);
        private float cameraSpeed = 4f;
        

        private void Start()
        {
            Invoke(nameof(GetPlayerBody), 0.3f);
        }

        private void LateUpdate()
        {
            if (playerRB != null)
            {
                Vector3 playerForward = (playerRB.velocity + player.transform.forward).normalized;
                transform.position = Vector3.Lerp(transform.position,
                    player.position + player.transform.TransformVector(cameraOffset) + playerForward * (-5f),
                    cameraSpeed * Time.deltaTime);
                transform.LookAt(player);
            }
        }

        private void GetPlayerBody()
        {
            playerRB = player.GetComponent<Rigidbody>();
        }

        public void SetPlayer(Transform player)
        {
            this.player = player;
        }
    }
}
