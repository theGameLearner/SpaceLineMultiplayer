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

    public void ReSpawnPlayer(GameObject player,bool resetCoins){

        PlayerController playerController = player.GetComponent<PlayerController>();
        if(!playerController){
            Debug.LogError("playercontroller missing");
            return;
        }

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
            foreach (var coin in playerController.coinsCollected)
            {
                coin.gameObject.SetActive(true);
                //playerController.coinsCollected.Remove(coin);
            }
            playerController.coinsCollected = new List<GameObject>();
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
        //add coin to player's collection
        playerController.coinsCollected.Add(coin);
        coin.gameObject.SetActive(false);

    }

    public void GameOver(int WinningPlayer){
        
        gameOverPanel.SetActive(true);
        var msg = gameOverPanel.GetComponentInChildren<Text>();
        msg.text+="Player-"+WinningPlayer.ToString()+" wins!";

    }
    public void ResetScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
