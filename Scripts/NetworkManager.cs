using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
namespace Com.TpicGaming.A
{
   

    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        
        #region Private Serializable Fields
        
        [SerializeField]
        private byte maxPlayersPerRoom = 4;
        #endregion
        #region Private Fields
        bool isConnecting;
        string gameVersion = "1";
        #endregion

        #region Public Fields

      
        [SerializeField]
        private GameObject controlPanel;
       
        [SerializeField]
        private GameObject progressLabel;
        #endregion


        #region MonoBehaviour CallBacks

        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }
        #endregion
        #region Public Methods

        public void Connect()
        {
            isConnecting = true;
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        #endregion
        #region MonoBehaviourPunCallbacks Callbacks


        public override void OnConnectedToMaster()
        {
           
            if (isConnecting)
            {
               
                PhotonNetwork.JoinRandomRoom();
            }
        }


        public override void OnDisconnected(DisconnectCause cause)
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }


        public override void OnJoinRandomFailed(short returnCode, string message)
        {

            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
         
                Debug.Log("We load the 'Room for 2' ");
                PhotonNetwork.LoadLevel(1);
            
        }
        #endregion

    }
}