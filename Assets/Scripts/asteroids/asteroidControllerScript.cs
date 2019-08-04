using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidControllerScript : MonoBehaviour
{

//variables to be shown in inspector
//note the coordinate system is wrt controller

//max distance from initial position in +ve x
    public float positiveXDistance = 10f;
    
//max distance from initial position in -ve x
    public float negativeXDistance = -10f;

//speed
    public float speed = 10f;
    
    
    private GameObject asteroid;

    private Rigidbody2D rb2d;


    /// <summary>
    /// Reset is called when the user hits the Reset button in the Inspector's
    /// context menu or when adding the component the first time.
    /// </summary>
    void Reset()
    {
        SetUp();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
//        Debug.Log("onEnable called");
        SetUp();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        SetUp();
    }
    void SetUp()
    {
        //intialize components
        asteroid = GetComponentInChildren<SpriteRenderer>().gameObject;
        if(asteroid){
            rb2d =  asteroid.GetComponent<Rigidbody2D>();
            if(!rb2d){
                Debug.LogError("No rigidbody component in asteriod sprite");
            }
        }
        else
        {
            Debug.LogError("assign a asteriod sprite");
        }
        rb2d.velocity = asteroid.transform.right*speed;
//        Debug.Log(asteroid.transform.right*speed);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!rb2d)
            return;
        float XDisplacement = asteroid.transform.localPosition.x;
        if(XDisplacement>=positiveXDistance){
            setvelocity("reverse");
        }
        else if(XDisplacement<=negativeXDistance){
            // Debug.Log("displacement x:"+XDisplacement.ToString());
            setvelocity("forward");
        }
    }

    /// <summary>
	/// sets the velocity of the asteriod according to direction
	/// </summary>
    void setvelocity(string direction){
        switch(direction){
            case "reverse":
            rb2d.velocity = asteroid.transform.right*speed*-1;
            break;
            case "forward":
            rb2d.velocity = asteroid.transform.right*speed;
            break;
        }

    }
}
