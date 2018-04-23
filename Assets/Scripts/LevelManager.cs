using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public int numPlayers;
    public GameObject[] players;

    public Text[] scores;
    public Text timerDisplay;

    public Button menu;
    public Button restart;

    public float timer;

    void Start()
    {
        int[] startScores = { 0, 0 };
        timer = 10f;
        if(numPlayers == 2)
        {
            scores[0].color = Color.red;
            scores[1].color = Color.blue;
        }     
        DisplayInfo(timer, startScores);
        menu.gameObject.SetActive(false);
        restart.gameObject.SetActive(false);
    }

    void DisplayInfo(float timeleft, int[] _scores)
    {
        timerDisplay.text = "Time Left: " + timeleft.ToString("F1");
        for(int i = 0; i < numPlayers; i++)
        {
            scores[i].text = "Player " + players[i].GetComponent<PlayerScript>().player.ToString() + ": " + _scores[i].ToString(); 
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
	
    void Update()
    {
        if(timer > 0)
        {
            int[] _scores = new int[numPlayers];
            for (int i = 0; i < numPlayers; i++)
            {
                _scores[i] = players[i].GetComponent<PlayerScript>().score;
            }
            timer -= Time.deltaTime;
            DisplayInfo(timer, _scores);
        } else
        {
            for(int i = 0; i < numPlayers; i++)
            {
                players[i].GetComponent<PlayerScript>().plr.interactable = false;
            }
            menu.gameObject.SetActive(true);
            restart.gameObject.SetActive(true);
        }
    }
}
