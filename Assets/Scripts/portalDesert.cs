using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portalDesert : MonoBehaviour
{
    private float endRoundTimer;
    private bool endRace;
    private bool loser;

    // Start is called before the first frame update
    void Start()
    {
        endRoundTimer = 5f;
        endRace = false;
        loser = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(endRace)
        {
            endRoundTimer -= Time.deltaTime;
            if(endRoundTimer < 0)
            {
                if(loser) {
                    //TODO: Go back to mainMenu
                    SceneManager.LoadScene("desert");
                }
                else {
                    //TODO: Go to beach World
                    SceneManager.LoadScene("desert");
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            other.gameObject.SetActive(false);
            endRace = true;
        }
        if(other.CompareTag("bots")){
            endRace = true;
        }
    }
}
