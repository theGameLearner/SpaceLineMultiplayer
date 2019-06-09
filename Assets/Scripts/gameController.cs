using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public Nodes startNode;
    public Nodes endNode;

    public GameObject gameOverPanel;

    public int coinsToWin = 1;
    public static gameController instance;

    public int NoOfPlayers = 2;

    public List<List<GameObject>> coinsCollected;

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
        coinsCollected = new List<List<GameObject>>();
        for (int i = 0; i < NoOfPlayers; i++)
        {
            coinsCollected.Add(new List<GameObject>());
        }
    }

    public void ReSpawnPlayer(GameObject player,bool resetCoins){

        

        PlayerController playerController = player.GetComponent<PlayerController>();
        if(!playerController){
            Debug.LogError("playercontroller missing");
            return;
        }
        int playerIndex = playerController.playerNo-1;
        //make the player invisible
        player.GetComponent<SpriteRenderer>().enabled = false;

        //player destroyed particle effect

        //reset position
        Debug.Log("controller:"+startNode);
        playerController.FromNode = startNode;
        playerController.ToNode = startNode.forwardNode;
        Debug.Log("controller:"+playerController.FromNode);
        Debug.Log("controller:"+playerController.ToNode);
        playerController.align();

        if(resetCoins){
            //reset coins
            foreach (var coin in coinsCollected[playerIndex])
            {
                coin.gameObject.SetActive(true);
                //playerController.coinsCollected.Remove(coin);
            }
            coinsCollected[playerIndex] = new List<GameObject>();
        }
       

        
        //make player visible
        player.GetComponent<SpriteRenderer>().enabled = true;

    }

    public void coinCollected(GameObject player,GameObject coin){

        PlayerController playerController = player.GetComponent<PlayerController>();
        if(!playerController){
            Debug.LogError("playercontroller missing");
            return;
        }
        int playerIndex = playerController.playerNo-1;

        //add coin to player's collection
        coinsCollected[playerIndex].Add(coin);
        coin.gameObject.SetActive(false);

    }

    public void GameOver(int WinningPlayer){
        
        gameOverPanel.SetActive(true);
        var msg = gameOverPanel.GetComponentInChildren<Text>();
        msg.text="GameOver: Player-"+WinningPlayer.ToString()+" wins!";

    }
    public void ResetScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
