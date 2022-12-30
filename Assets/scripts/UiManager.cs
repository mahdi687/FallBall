using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class UiManager : MonoBehaviour
{

    public GameObject replay;
    public GameObject quit;
    public GameObject home;

    public Text scoreText, bestScoreText, goldText;
    void Start()
    {
        replay.SetActive(false);
        quit.SetActive(false);
        home.SetActive(false);
        StartCoroutine(CountScore());
    }

    IEnumerator CountScore()
    {
        while(true)
        {
            if (FindObjectOfType<groundManager>().groundFinishMove && !FindObjectOfType<PlayerMouvement>().gameOver)
                ScoreManager.instance.AddScore(1);
            yield return new WaitForSeconds(1);
        }
    }

    void Update()
    {
        scoreText.text = ScoreManager.instance.Score.ToString();
        bestScoreText.text = PlayerPrefs.GetInt("highScore").ToString();
        if (FindObjectOfType<PlayerMouvement>().gameOver)
        {
            Invoke("enableButton", 1.5f);
            
        }
    }
    void enableButton()
    {
        replay.SetActive(true);
        quit.SetActive(true);
        home.SetActive(true);
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void homeScreen()
    {
        SceneManager.LoadScene(0);
    }
}
