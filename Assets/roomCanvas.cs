using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class roomCanvas : MonoBehaviour
{
    public Text roomName;
    public Text playersJoined;
    public Text hostText;
    public Text statusText;

    public Text noOfPlayers;

    public Button startButton;



    public void setData(){
        bool isMasterClient = PhotonNetwork.IsMasterClient;
        Room room = PhotonNetwork.CurrentRoom;

        if(isMasterClient){
            hostText.text = "host : yes";
            startButton.gameObject.SetActive(true);   
        }
        else
        {
            hostText.text = "host : no";
            startButton.gameObject.SetActive(false);            
        }
        roomName.text = room.Name;
        string playerList  = "players:\n";
        foreach(Player player in PhotonNetwork.PlayerList){
            playerList+=player.NickName+"\n";
        }
        playersJoined.text = playerList;

        noOfPlayers.text = "no of players:"+room.PlayerCount;
         if(room.PlayerCount == room.MaxPlayers){
                startButton.interactable = true;
                statusText.text = "waiting for host to start game.";                  
            }
        else
        {
            startButton.interactable = false;
            statusText.text = "waiting for players to join ...";              
        }

        
       
    }





}
