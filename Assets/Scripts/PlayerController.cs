using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject FromNode;
    public GameObject ToNode;

    public float speed = 4f;

   

    void Start()
    {
        align();
        transform.position = FromNode.transform.position;
    }

    void Update()
    {

        if(FromNode&&ToNode){

            float totDistance = (ToNode.transform.position-FromNode.transform.position).magnitude;

            float currDistance = (transform.position-FromNode.transform.position).magnitude;
            currDistance += Time.deltaTime*speed;

            float t = currDistance/totDistance;

           if(Input.GetKey(KeyCode.W)){
            transform.position = Vector2.Lerp(FromNode.transform.position,ToNode.transform.position,t);

            }

        }
        else
        {
            Debug.LogError("FromNode Or ToNode not assigned");
            return;
        }
      
    }

    void align(){

        if(FromNode&&ToNode){
            Vector2 direction = (ToNode.transform.position-FromNode.transform.position).normalized;
            Debug.Log("direction:"+direction.ToString());
            transform.rotation = Quaternion.FromToRotation(transform.up,direction);

        }
        else
        {
            Debug.LogError("FromNode Or ToNode not assigned");
            return;
        }

        
    }
}
