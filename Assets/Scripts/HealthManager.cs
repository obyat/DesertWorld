using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public PlayerController thePlayer;

    public float invincibiltyLength;
    public float invincibilityCounter;

    public Renderer playerRender;
    public float flashCounter;
    public float flashLength = 0.1f;

    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLength;

    public GameObject deathEffect;
    public Image blackScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitForFade;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        respawnPoint = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                playerRender.enabled = !playerRender.enabled;
                flashCounter = flashLength;
            }
            if(invincibilityCounter <= 0)
            {
                playerRender.enabled = true;
            }
        }
        if(isFadeToBlack) 
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }

        if(isFadeFromBlack) 
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(blackScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }
    }

    public void hurtPlayer(int damage, Vector3 dir)
    {
        if(invincibilityCounter <= 0)
        {
            currentHealth -= damage;

            if(currentHealth <= 0)
            {
                respawn();
            }
            else
            {
                thePlayer.knockBack(dir);
                invincibilityCounter = invincibiltyLength;
                playerRender.enabled = false;
                flashCounter = flashLength;
            }
        }
    }

    public void respawn()
    {
        if(!isRespawning)
        {
            StartCoroutine("respawnCo");
        }
    }

    public IEnumerator respawnCo()
    {
        isRespawning = true;
        thePlayer.gameObject.SetActive(false);
        Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSeconds(respawnLength);

        isFadeToBlack = true;

        yield return new WaitForSeconds(waitForFade);

        isFadeFromBlack = true;
        isRespawning = false;

        thePlayer.transform.position = respawnPoint;
        thePlayer.gameObject.SetActive(true);
        currentHealth = maxHealth;

        invincibilityCounter = invincibiltyLength;
        playerRender.enabled = false;
        flashCounter = flashLength;
    }

    public void healPlayer(int heal)
    {
        currentHealth += heal;
        
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void setSpawnPoint(Vector3 newPostion)
    {
        respawnPoint = newPostion;
    }
}
