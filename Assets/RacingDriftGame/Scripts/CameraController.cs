using System;
using RacingDriftGame.Scripts.Car;
using UnityEngine;

namespace RacingDriftGame.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private TypeOfGame typeOfGame;
        [SerializeField] private Transform player;
        private Rigidbody playerRB;
        private Vector3 cameraOffset = new(0, 2, -5f);
        private Vector3 velocity = Vector3.zero;
        private float smoothTime = 0.3f;

        private void Start()
        {
            if (typeOfGame == TypeOfGame.Single)
            {
                playerRB = player.GetComponent<Rigidbody>();
            }
        }

        private void LateUpdate()
        {
            if (playerRB != null)
            {
                Vector3 targetPosition = player.position + player.transform.TransformVector(cameraOffset);
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
                transform.LookAt(player);
            }
        }

        public void SetPlayer(Transform player)
        {
            this.player = player;
            playerRB = player.GetComponent<Rigidbody>();
        }
    }
}