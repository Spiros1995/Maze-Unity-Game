using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    void Update()
    {
        GameObject.Find("Score").GetComponent<Text>().text = Game.finalScore + "";
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Application.isEditor) {
                UnityEditor.EditorApplication.isPlaying = false;
            }
            Application.Quit();
        }
        
    }
}
