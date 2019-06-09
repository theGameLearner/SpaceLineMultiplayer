using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinToWin : MonoBehaviour
{
     private Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        coinText = GetComponent<Text>();
        if(!coinText){
            Debug.LogError("No text component");
            return;
        }
        coinText.text = "Coins to win: "+gameController.instance.coinsToWin;
        
    }

    
}
