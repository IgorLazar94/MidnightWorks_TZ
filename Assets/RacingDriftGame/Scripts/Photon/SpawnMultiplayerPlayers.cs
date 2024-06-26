using System;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using RacingDriftGame.Scripts.Car;
using RacingDriftGame.Scripts.Collections;
using RacingDriftGame.Scripts.UI.GameplayUI;
using UnityEngine;

namespace RacingDriftGame.Scripts.Photon
{
    public class SpawnMultiplayerPlayers : MonoBehaviourPunCallbacks
    {
        public static Action OnStartTheGame;
        [SerializeField] private CarController playerPrefab;
        [SerializeField] private Transform[] playerSpawnPoints;
        [SerializeField] private TextureCollection textureCollection;
        [SerializeField] private ScoresDriftManager scoresDriftManager;
        [SerializeField] private HUDButton gasButton, brakeButton, turnLeftButton, turnRightButton;
        private float timeToStart = 3f;
        private bool isStartGame = false;
        private Camera mainCamera;

        private void Start()
        {
            int spawnIndex = GetSpawnIndex();
            var playerSpawnPoint = playerSpawnPoints[spawnIndex];
            var player = PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPoint.position, Quaternion.identity);
            FindCameraAndSetPlayer(player.transform);
            var carController = player.GetComponent<CarController>();
            scoresDriftManager.SetPlayer(carController);
            carController.SetHUDButtonsLinks(gasButton, brakeButton, turnLeftButton, turnRightButton);
            player.GetComponent<CarView>().SetTextureLink(textureCollection);
            if (PhotonNetwork.CurrentRoom.Players.Count >= 2)
            {
                PrepareStartGame();
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (!isStartGame && PhotonNetwork.CurrentRoom.Players.Count >= 2)
            {
                isStartGame = true;
                PrepareStartGame();
            }
        }

        private void FindCameraAndSetPlayer(Transform player)
        {
            mainCamera = Camera.main;
            if (mainCamera != null)
                mainCamera.GetComponent<CameraController>().SetPlayer(player);
        }

        private int GetSpawnIndex()
        {
            int playerCount = PhotonNetwork.CurrentRoom.Players.Count;
            return Mathf.Min(playerCount - 1, playerSpawnPoints.Length - 1);
        }

        private IEnumerator StartTheGame(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            OnStartTheGame?.Invoke();
        }

        private void PrepareStartGame()
        {
            StartCoroutine(StartTheGame(timeToStart));
            scoresDriftManager.StartCountingToTheStart(timeToStart);
        }
    }
}