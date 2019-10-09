using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

struct PlayerInfo{
    public int playerId;
    public string playerName;
}

public class gameControllerMultiplayer : MonoBehaviourPunCallbacks
{
    public Nodes startNode;
    public Nodes endNode;

    public GameObject playerPrefab;

    public GameObject gameOverPanel;

    public int coinsToWin = 1;
    public static gameControllerMultiplayer instance;

    public int NoOfPlayers = 2;
	List<PlayerInfo> players;

    public Dictionary<int,List<GameObject>> coinsCollected;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(instance == null){
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }   
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        players = new List<PlayerInfo>();
        NoOfPlayers = PhotonNetwork.CurrentRoom.PlayerCount;

		foreach (Player p in PhotonNetwork.PlayerList)
        {
            PlayerInfo player = new PlayerInfo();
            player.playerId = p.ActorNumber;
            player.playerName = p.NickName;

            players.Add(player);

        }
		coinsCollected = new Dictionary<int, List<GameObject>>();
        foreach(PlayerInfo pi in players){
            coinsCollected[pi.playerId] = new List<GameObject>();
        }

        GameObject playership = (GameObject)PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity, 0);
        playership.GetComponent<PlayerControllerMultiplayer>().playerNo = PhotonNetwork.LocalPlayer.ActorNumber;
    }

    public void ReSpawnPlayer(GameObject player,bool resetCoins){

        

        PlayerControllerMultiplayer playerControllerMultiplayer = player.GetComponent<PlayerControllerMultiplayer>();
        if(!playerControllerMultiplayer){
            Debug.LogError("playerControllerMultiplayer missing");
            return;
        }
        int playerIndex = playerControllerMultiplayer.playerNo;
        //make the player invisible
        player.GetComponent<SpriteRenderer>().enabled = false;

        //player destroyed particle effect

        //reset position
        Debug.Log("controller:"+startNode);
        playerControllerMultiplayer.FromNode = startNode;
        playerControllerMultiplayer.destinationIndex = 0;
        playerControllerMultiplayer.ToNode = startNode.myDestinations[0];
        Debug.Log("controller:"+playerControllerMultiplayer.FromNode);
        Debug.Log("controller:"+playerControllerMultiplayer.ToNode);
        playerControllerMultiplayer.align();

        if(resetCoins){
            //reset coins
            foreach (var coin in coinsCollected[playerIndex])
            {
                coin.gameObject.SetActive(true);
                //playerControllerMultiplayer.coinsCollected.Remove(coin);
            }
            coinsCollected[playerIndex] = new List<GameObject>();
        }
       

        
        //make player visible
        player.GetComponent<SpriteRenderer>().enabled = true;

    }

    public void coinCollected(GameObject player,GameObject coin){

        PlayerControllerMultiplayer playerControllerMultiplayer = player.GetComponent<PlayerControllerMultiplayer>();
        if(!playerControllerMultiplayer){
            Debug.LogError("playerControllerMultiplayer missing");
            return;
        }
        int playerIndex = playerControllerMultiplayer.playerNo;

        //add coin to player's collection
        coinsCollected[playerIndex].Add(coin);
        coin.gameObject.SetActive(false);

    }

    public void GameOver(int WinningPlayer){
        
        gameOverPanel.SetActive(true);
        var msg = gameOverPanel.GetComponentInChildren<Text>();
        string WinningPlayerName = "";
        foreach(PlayerInfo pi in players){
            if(pi.playerId == WinningPlayer){
                WinningPlayerName = pi.playerName;
            }
        }
        msg.text="GameOver: Player-"+WinningPlayerName+" wins!";

    }
    public void ResetScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
