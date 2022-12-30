using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
   //making the script public So we can modify it from another script 
    public static ScoreManager instance;

    public int Score;
    void Awake ()
    {
        instance = this;   
    }
    public void AddScore(int amount )
    {
        Score += amount;
         
        if(Score > PlayerPrefs.GetInt("highScore",0))
        {
            PlayerPrefs.SetInt("highScore",Score);
        }
    }
}
