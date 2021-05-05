using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroySnake : MonoBehaviour
{

    public int value;
    int numOfSnakes;

    public GameObject pickupEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        destroySnake[] snake = FindObjectsOfType<destroySnake>();
        numOfSnakes = snake.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("farmObjects"))
        {
            FindObjectOfType<GameManager>().setNumOfSnakes(numOfSnakes);
            Destroy(gameObject);
        }
        else if(other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().addGold(value);

            Instantiate(pickupEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
        else if(other.CompareTag("bots"))
        {

            FindObjectOfType<GameManager>().addBotGold(other.gameObject, value);
            
            Instantiate(pickupEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
