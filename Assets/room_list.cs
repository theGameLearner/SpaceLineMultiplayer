using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class room_list : MonoBehaviour
{
    public GameObject room_prefab;
    GameObject content; 
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        content = transform.Find("Viewport/Content").gameObject;
        if(content == null){
            Debug.LogError("Content missing in scroll view");
        }
    }
    
    public void populate(List<RoomInfo> rooms){
        if(content!=null){

            foreach(Transform c in content.transform){
                Destroy(c.gameObject);
            }

            GameObject temp;

            foreach(RoomInfo room in rooms){
                temp = (GameObject)Instantiate(room_prefab,content.transform);
                room_button rButton = temp.GetComponent<room_button>();
                rButton.room_name.text = room.Name;
                rButton.no_of_players.text = "players:"+room.PlayerCount.ToString();
                temp.transform.parent = content.transform;
            }
        }
        

    }
}
