using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoinTextScript : MonoBehaviour
{

    public int playerNo;

    private Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        coinText = GetComponent<Text>();
        if(!coinText){
            Debug.LogError("No text component");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(coinText){
            coinText.text = "Player "+ playerNo + " coins: "+gameController.instance.coinsCollected[playerNo-1].Count;
        }
    }
}
