using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{

    public int countDownTime;
    public Text countdownDisplay;
    public bool startText = true;

    IEnumerator countdownToStart()
    {
        // Time.timeScale = 0;
        while(countDownTime > 0)
        {
            
            countdownDisplay.text = countDownTime.ToString();

            yield return new WaitForSeconds(1f);

            countDownTime--;

        }
        Time.timeScale = 1;
        countdownDisplay.text = "GO!";

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(countdownToStart());
        startText = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(startText){
            StartCoroutine(countdownToStart());
            startText = false;
        }
    }
}
