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

        //GameObject player = (GameObject)Instantiate(Resources.Load("Player"));
        //make the player invisible
        player.GetComponent<SpriteRenderer>().enabled = false;

        //player destroyed particle effect

        //reset position
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.FromNode = startNode;
        playerController.ToNode = startNode.forwardNode;
        playerController.align();

        //reset coins

        
        //make player visible
        player.GetComponent<SpriteRenderer>().enabled = true;

    }

    
}
