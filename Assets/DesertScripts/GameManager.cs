using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentGold;
    public Text goldText;

    public int placementNum;
    public Text placement;

    public int numOfBots;
    public int[] botspoints;
    public GameObject[] bots;

    public GameObject blueSnake;
    public GameObject greenSnake;
    public int numOfSnakes;
    public int maxOfSnakes;
    public bool isBlue;

    public Text timer;
    public float timerText;

    public float startTimer;
    public GameObject[] startText;

    public GameObject winText;
    public GameObject loseText;
    public GameObject player;

    public bool startGame;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        isBlue = true;
        startTimer = 6f;
        startGame = true;
        numOfBots = 9;
    }

    // Update is called once per frame
    void Update()
    {
        if(numOfSnakes < maxOfSnakes) 
        {
            spawnSnake();
        }
        timerText -= Time.deltaTime;
        timer.text = "Timer: " + (int)timerText;
        if(timerText <= 0)
        {
            // TODO:
            // Win or Lose game, Update opponent count.
            Vector3 pos = player.transform.position;
            pos.y += 3; 

        
            if (placementNum >= 2)
            {
                Instantiate(winText, pos, Quaternion.identity);
            }
            else
            {
                Instantiate(loseText, pos, Quaternion.identity);
            }
            Time.timeScale = 0;
        }
        if((int) startTimer > 4){
            // On "Start in" screen
        } else if((int) startTimer == 4){
            Destroy(startText[0]);
            startText[1].SetActive(true);
        } else if((int) startTimer == 3){
            Destroy(startText[1]);
            startText[2].SetActive(true);
        } else if((int) startTimer == 2){
            Destroy(startText[2]);
            startText[3].SetActive(true);
        } else if((int) startTimer == 1){
            Destroy(startText[3]);
            startText[4].SetActive(true);
        }
        else if (startGame)
        {
            Time.timeScale = 1;
            Destroy(startText[4]);
            startGame = false;
        }
        startTimer -= .0024f;

        for(int i = 0; i < numOfBots; i++)
        {
            if(currentGold <= botspoints[i]) {
                placementNum++;
            }
        }
        updatePlacement(++placementNum);
        placementNum = 0;
    }

    public void addGold(int goldToAdd)
    {
        currentGold += goldToAdd;
        goldText.text = "Points: " + currentGold;
        numOfSnakes--;
    }
    public void addBotGold(GameObject bot, int goldToAdd)
    {
        // botOneGold += goldToAdd;
        // botOneText.text = "Bot 1: " + botOneGold;
        for(int i = 0; i < numOfBots; i++)
        {
            if(bot.GetInstanceID() == bots[i].GetInstanceID())
            {
                botspoints[i]++;
            }
        }
        numOfSnakes--;
    }

    private void spawnSnake()
    {
        Vector3 spawn = new Vector3(Random.Range(-35.0f, 35.0f), 0f, Random.Range(-35.0f, 35.0f));
        if (isBlue)
        {
            Instantiate(blueSnake, spawn, Quaternion.identity);
            isBlue = false;
        } 
        else
        {
            Instantiate(greenSnake, spawn, Quaternion.identity);
            isBlue = true;
        }
        numOfSnakes++;
    }

    // public void killedSnake()
    // {
    //     numOfSnakes--;
    // }

    public void setNumOfSnakes(int num)
    {
        numOfSnakes = num;
    }

    public void updatePlacement(int num)
    {
        int temp = numOfBots + 1;
        placement.text = "Place: " + num + " of " + temp;
    }
}
