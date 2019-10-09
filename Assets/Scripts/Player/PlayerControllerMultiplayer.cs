using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerControllerMultiplayer : MonoBehaviourPun
{
    public Nodes FromNode = null;
    public Nodes ToNode = null;

    public int playerNo = 1;

    public float positionTolerance = 1f;

    private KeyCode Forwardkey;
    private KeyCode Leftkey;
    private KeyCode Rightkey;

    public float speed = 4f;

    public int destinationIndex = 0;

    public bool canMoveForward = true;

    void Start()
    {
       
        FromNode = gameControllerMultiplayer.instance.startNode;
        ToNode = FromNode.myDestinations[0];
        align();
        
            Forwardkey = KeyCode.UpArrow;
            Leftkey = KeyCode.LeftArrow;
            Rightkey = KeyCode.RightArrow;

        
    }
    void Update()
    {
        

        if(FromNode!=null&&ToNode!=null)
		{

            

            if(photonView.IsMine)

            { //turning control
                if(Input.GetKeyDown(Leftkey)&& (transform.position-FromNode.transform.position).magnitude<=positionTolerance){
                    
                    if(FromNode.myDestinations.Count!=0){
                        destinationIndex=(destinationIndex+1)%FromNode.myDestinations.Count; 
                        if(FromNode.myDestinations[destinationIndex] == null){
                            Debug.LogError("node mydestination"+ destinationIndex+"of "+FromNode.name+" is null");
                            destinationIndex = (destinationIndex-1+FromNode.myDestinations.Count)%FromNode.myDestinations.Count;
                        }
                        else
                        {
                            ToNode = FromNode.myDestinations[destinationIndex];
                            speed = FromNode.myDestSpeed[destinationIndex];

                            align();
                        }
                    }
                    else{
                        Debug.LogError("node "+FromNode.name+" has no destinations");
                    }
                    
                    
                }
                if(Input.GetKeyDown(Rightkey)&& (transform.position-FromNode.transform.position).magnitude<=positionTolerance){
                if(FromNode.myDestinations.Count != 0){ 
                        destinationIndex = (destinationIndex-1+FromNode.myDestinations.Count)%FromNode.myDestinations.Count;
                        if(FromNode.myDestinations[destinationIndex] == null){
                            destinationIndex=(destinationIndex+1)%FromNode.myDestinations.Count; 
                            Debug.LogError("node mydestination"+ destinationIndex+"of "+FromNode.name+" is null");
                        }
                        else
                        {
                            ToNode = FromNode.myDestinations[destinationIndex];
                            align();
                        }
                    }
                    else{
                        Debug.LogError("node "+FromNode.name+" has no destinations");
                    }
                    
                }
            
            

            //forward movement

           if(canMoveForward){
                if(Input.GetKey(Forwardkey)){
        
                float totDistance = (ToNode.transform.position-FromNode.transform.position).magnitude;

                float currDistance = (transform.position-FromNode.transform.position).magnitude;
                Debug.Log("tot:"+(ToNode.transform.position-FromNode.transform.position).magnitude);
                Debug.Log("curr:"+currDistance);
                currDistance += Time.deltaTime*speed;
                Debug.Log("curr2:"+currDistance);
                float t = currDistance/totDistance;
                Debug.Log("t"+t);
                transform.position = Vector2.Lerp(FromNode.transform.position,ToNode.transform.position,t);
                Debug.Log(transform.position);
                }
           }

           if(Input.GetKeyDown(Forwardkey)){
               // Debug.Log("key down");
                canMoveForward = true;
            }
        }


            //when player reaches to node
            if(transform.position == ToNode.transform.position){
                if(ToNode.end){
                    if(gameControllerMultiplayer.instance.coinsCollected[playerNo].Count>=gameControllerMultiplayer.instance.coinsToWin){
                        //call gameover
                        gameControllerMultiplayer.instance.GameOver(playerNo);
                    }
                    else
                    {
                        //send back to start
                        gameControllerMultiplayer.instance.ReSpawnPlayer(gameObject,false);
                    }
                }
                else{
                    FromNode = ToNode;
                    ToNode = ToNode.myDestinations[0];
                    destinationIndex = 0;
                    canMoveForward = false;
                    align();
                }

            
           

        }
        else
        {
            // Debug.LogError("FromNode Or ToNode not assigned:"+playerNo);
            return;
        }

        
        }
      
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
//        Debug.Log("collision");
        if(other.tag == "asteroid"){
            gameControllerMultiplayer.instance.ReSpawnPlayer(gameObject,true);
        }
        if(other.tag == "coin"){
            gameControllerMultiplayer.instance.coinCollected(gameObject,other.transform.parent.gameObject);
        }
    }

    public void align(){

        Debug.Log(FromNode);
        Debug.Log(ToNode);
        transform.position = FromNode.transform.position;

        if(FromNode&&ToNode){
            Vector3 direction = (ToNode.transform.position-FromNode.transform.position).normalized;
            transform.rotation = Quaternion.FromToRotation(Vector3.up,direction);

        }
        else
        {
            Debug.LogError("FromNode Or ToNode not assigned");
            return;
        }

        
    }
}
