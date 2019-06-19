﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Nodes FromNode = null;
    public Nodes ToNode = null;

    public int playerNo = 1;

    public float positionTolerance = 1f;

    private KeyCode Forwardkey;
    private KeyCode Leftkey;
    private KeyCode Rightkey;



    public float speed = 4f;

    private enum directions
    {
        forward,left,right
    }

    private directions directionFacing = directions.forward;

    public bool canMoveForward = true;

   

    void Start()
    {
       
        FromNode = gameController.instance.startNode;
        ToNode = FromNode.forwardNode;
        align();
        if(playerNo == 1){
            Forwardkey = KeyCode.W;
            Leftkey = KeyCode.A;
            Rightkey = KeyCode.D;
        }
        else
        {
            Forwardkey = KeyCode.UpArrow;
            Leftkey = KeyCode.LeftArrow;
            Rightkey = KeyCode.RightArrow;
        }
        
    }
    void Update()
    {

        if(FromNode!=null&&ToNode!=null){

            float totDistance = (ToNode.transform.position-FromNode.transform.position).magnitude;

            float currDistance = (transform.position-FromNode.transform.position).magnitude;
            currDistance += Time.deltaTime*speed;

            float t = currDistance/totDistance;


            //turning control
            if(Input.GetKeyDown(Leftkey)&& (transform.position-FromNode.transform.position).magnitude<=positionTolerance){
                //Debug.Log(directionFacing);
                if(directionFacing == directions.forward&&FromNode.leftNode!=null){
                    directionFacing = directions.left;
                    ToNode = FromNode.leftNode;
                }
                else if(directionFacing == directions.right)
                {
                    directionFacing = directions.forward;
                    ToNode = FromNode.forwardNode;

                }
                align();
                
            }
            if(Input.GetKeyDown(Rightkey)&& (transform.position-FromNode.transform.position).magnitude<=positionTolerance){
                Debug.Log(directionFacing);
                if(directionFacing == directions.forward&&FromNode.rightNode!=null){
                    directionFacing = directions.right;
                    ToNode = FromNode.rightNode;
                }
                else if(directionFacing == directions.left)
                {
                    directionFacing = directions.forward;
                    ToNode = FromNode.forwardNode;

                }
                align();
            }
            

            //forward movement

           if(canMoveForward){
                if(Input.GetKey(Forwardkey)){
                transform.position = Vector2.Lerp(FromNode.transform.position,ToNode.transform.position,t);

                }
           }

           if(Input.GetKeyDown(Forwardkey)){
                Debug.Log("key down");
                canMoveForward = true;
            }


            //when player reaches to node
            if(transform.position == ToNode.transform.position){
                if(ToNode.end){
                    if(gameController.instance.coinsCollected[playerNo-1].Count>=gameController.instance.coinsToWin){
                        //call gameover
                        gameController.instance.GameOver(playerNo);
                    }
                    else
                    {
                        //send back to start
                        gameController.instance.ReSpawnPlayer(gameObject,false);
                    }
                }
                else{
                    FromNode = ToNode;
                    ToNode = ToNode.forwardNode;
                    directionFacing = directions.forward;
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
        Debug.Log("collision");
        if(other.tag == "asteroid"){
            gameController.instance.ReSpawnPlayer(gameObject,true);
        }
        if(other.tag == "coin"){
            gameController.instance.coinCollected(gameObject,other.transform.parent.gameObject);
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
