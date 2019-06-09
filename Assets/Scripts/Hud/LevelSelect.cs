using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public void LoadLevel(int LevelNo){
        string sceneName = "level"+LevelNo;
        SceneManager.LoadScene(sceneName);
    }
}
