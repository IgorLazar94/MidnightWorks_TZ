using System;
using UnityEngine;
using Photon.Pun;
using RacingDriftGame.Scripts.Collections;
using UnityEngine.SceneManagement;

namespace RacingDriftGame.Scripts.Photon
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            SceneManager.LoadScene(SceneNames.LobbyScene);
        }
    }
}
