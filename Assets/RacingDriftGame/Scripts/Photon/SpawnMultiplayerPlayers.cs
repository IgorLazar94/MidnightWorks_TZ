using System;
using Photon.Pun;
using RacingDriftGame.Scripts.Car;
using RacingDriftGame.Scripts.Collections;
using RacingDriftGame.Scripts.UI.GameplayUI;
using UnityEngine;

namespace RacingDriftGame.Scripts.Photon
{
    public class SpawnMultiplayerPlayers : MonoBehaviour
    {
        public CarController playerPrefab;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private TextureCollection textureCollection;
        [SerializeField] private ScoresDriftManager scoresDriftManager;
        [SerializeField] private HUDButton gasButton, brakeButton, turnLeftButton, turnRightButton;
        private Camera mainCamera;

        private void Start()
        {
            var player = PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPoint.position, Quaternion.identity);
            FindCameraAndSetPlayer(player.transform);
            var carController = player.GetComponent<CarController>();
            scoresDriftManager.SetPlayer(carController);
            carController.SetHUDButtonsLinks(gasButton, brakeButton, turnLeftButton, turnRightButton);
            player.GetComponent<CarView>().SetTextureLink(textureCollection);
        }

        private void FindCameraAndSetPlayer(Transform player)
        {
            mainCamera = Camera.main;
            if (mainCamera != null) mainCamera.GetComponent<CameraController>().SetPlayer(player);
        }
    }
}
