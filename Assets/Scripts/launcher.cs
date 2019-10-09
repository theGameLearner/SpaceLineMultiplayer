using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class launcher : MonoBehaviourPunCallbacks
{
    #region privateFields
        string gameVersion = "1";
        string defaultRoomName = "Room 1";
        List<RoomInfo> createdRooms = new List<RoomInfo>();

        bool JoiningRoom = false;

    #endregion

    #region serializedFields
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private byte maxPlayersPerRoom = 2;

        



    #endregion

    #region public fields
    [Tooltip("Room Name input field")]
    public InputField RoomNameField;

    [Tooltip("canvas containing the lobby")]
    public GameObject lobbyCanvas;

    public GameObject roomCanvas;

    public room_list rList;
        
    #endregion

    #region MonobehaviourCallbacks
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        // Start is called before the first frame update
        void Start()
        {
            Connect();
            
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if(!PhotonNetwork.IsConnected||PhotonNetwork.NetworkClientState!=ClientState.JoinedLobby){
//                lobbyCanvas.SetActive(false);
            }
        }
        
    #endregion

   
   #region PublicClassFunctions
         public void Connect()
        {
            // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
            if (PhotonNetwork.IsConnected)
            {
                // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
                //connect to lobby
                PhotonNetwork.JoinLobby(TypedLobby.Default);
                lobbyCanvas.SetActive(true);
                
            }
            else
            {
                // #Critical, we must first and foremost connect to Photon Online Server.
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public void createRoom(){
            string roomName = RoomNameField.text;
            connectToRoom(roomName);
        }
        

        public void connectToRoom(string roomName){
            if(roomName==""){
                Debug.LogError("empty room name");
                return;
            }
            JoiningRoom = true;
            int j = -1;
            for(int i=0;i<createdRooms.Count;i++){
                if(createdRooms[i].Name == roomName){
                    j=i;
                    break;
                }
            }
            if(j!=-1){
                PhotonNetwork.JoinRoom(createdRooms[j].Name);
            }
            else
            {
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.IsOpen = true;
                roomOptions.IsVisible = true;
                roomOptions.MaxPlayers = maxPlayersPerRoom;

                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);

                
            }
            
        }

        public void startGame(){
            PhotonNetwork.LoadLevel("multiplayerLevel1");
        }

        

        public void leaveRoom(){
            roomCanvas.SetActive(false);
            PhotonNetwork.LeaveRoom();
            lobbyCanvas.SetActive(true);
            refresh();
        }

        public void refresh(){
            if (PhotonNetwork.IsConnected)
            {
                //Re-join Lobby to get the latest Room list
                PhotonNetwork.JoinLobby(TypedLobby.Default);
            }
            else
            {
                //We are not connected, estabilish a new connection
                Connect();
            }
        }

        public void JoinRandomRoom(){
            PhotonNetwork.JoinRandomRoom();
        }

       
   #endregion

  
        #region PUNcallbacks

            public override void OnPlayerEnteredRoom(Player newPlayer){
                roomCanvas.GetComponent<roomCanvas>().setData();
            }
            public override void OnPlayerLeftRoom(Player newPlayer){
                roomCanvas.GetComponent<roomCanvas>().setData();
            }

            public override void OnConnectedToMaster()
            {
                Debug.Log("Launcher: OnConnectedToMaster() was called by PUN");
                //connect to lobby
                PhotonNetwork.JoinLobby(TypedLobby.Default);
                lobbyCanvas.SetActive(true);

            }

            public override void OnRoomListUpdate(List<RoomInfo> roomList){
                Debug.Log("room list updated");
                createdRooms = roomList;
                rList.populate(createdRooms);
            }


            public override void OnDisconnected(DisconnectCause cause)
            {
                Debug.LogWarningFormat("Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
            }


            public override void OnJoinRandomFailed(short returnCode, string message)
            {
                Debug.Log("Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

                // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
                PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = maxPlayersPerRoom});
            }

            public override void OnJoinedRoom()
            {
                Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
                lobbyCanvas.SetActive(false);
                roomCanvas.SetActive(true);
                roomCanvas.GetComponent<roomCanvas>().setData();
               
            }

            public override void OnJoinRoomFailed(short returnCode, string message){
                //create a message box with the reason
            }

        #endregion
}
