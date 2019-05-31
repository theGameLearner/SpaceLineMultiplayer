using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public Nodes startNode;
    public Nodes endNode;
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

    public void ReSpawnPlayer(GameObject player){

        PlayerController playerController = player.GetComponent<PlayerController>();
        if(!playerController){
            Debug.LogError("playercontroller missing");
            return;
        }

        //GameObject player = (GameObject)Instantiate(Resources.Load("Player"));
        //make the player invisible
        player.GetComponent<SpriteRenderer>().enabled = false;

        //player destroyed particle effect

        //reset position
        playerController.FromNode = startNode;
        playerController.ToNode = startNode.forwardNode;
        playerController.align();

        //reset coins
        foreach (var coin in playerController.coinsCollected)
        {
            coin.gameObject.SetActive(true);
        }
        playerController.coinsCollected = new List<GameObject>();

        
        //make player visible
        player.GetComponent<SpriteRenderer>().enabled = true;

    }

    public void coinCollected(GameObject player,GameObject coin){

        PlayerController playerController = player.GetComponent<PlayerController>();
        if(!playerController){
            Debug.LogError("playercontroller missing");
            return;
        }
        playerController.coinsCollected.Add(coin);
        coin.gameObject.SetActive(false);

    }

    
}
