using UnityEngine;
using Photon.Pun;
using RacingDriftGame.Scripts.Collections;
using TMPro;

namespace RacingDriftGame.Scripts.Photon
{
    public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_InputField createInput;
        [SerializeField] private TMP_InputField joinInput;

        public void CreateRoom() //OnClickEvent
        {
            PhotonNetwork.CreateRoom(createInput.text);
        }

        public void JoinRoom() //OnClickEvent
        {
            PhotonNetwork.JoinRoom(joinInput.text);
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel(SceneNames.MultiplierWideForestRoad);
        }
    }
}