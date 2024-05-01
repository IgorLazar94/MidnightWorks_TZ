using System;
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
            playerRB = player.GetComponent<Rigidbody>();
        }

        private void LateUpdate()
        {
            Vector3 playerForward = (playerRB.velocity + player.transform.forward).normalized;
            transform.position = Vector3.Lerp(transform.position,
                player.position + player.transform.TransformVector(cameraOffset) + playerForward * (-5f),
                cameraSpeed * Time.deltaTime);
            transform.LookAt(player);
        }
    }
}
