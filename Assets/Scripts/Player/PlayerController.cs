using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Nodes FromNode;
    public Nodes ToNode;

    public float speed = 4f;

   

    void Start()
    {
        align();
        
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

        if(transform.position == ToNode.transform.position){
            FromNode = ToNode;
            ToNode = ToNode.forwardNode;
            align();
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
            gameController.instance.ReSpawnPlayer(gameObject);
        }
    }

    public void align(){

        transform.position = FromNode.transform.position;

        if(FromNode&&ToNode){
            Vector3 direction = (ToNode.transform.position-FromNode.transform.position).normalized;
            Debug.Log("direction:"+direction.ToString());
            Debug.Log("transform.up"+transform.up);
            transform.rotation = Quaternion.FromToRotation(Vector3.up,direction);

        }
        else
        {
            Debug.LogError("FromNode Or ToNode not assigned");
            return;
        }

        
    }
}
