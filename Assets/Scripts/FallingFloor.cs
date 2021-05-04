using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FallingFloor : MonoBehaviour
{
    public Vector3 initialPosition;
    public float dropTimer;
    public float returnTimer;
    public bool falling;
    public int speed;
    public GameObject bots;




    public GameObject respawnPrefab;
    public GameObject[] respawns;
    void Start()
    {
        initialPosition = transform.position;
        dropTimer = Random.Range(8f, 12f);
        speed = 2;
        bots = GameObject.FindGameObjectWithTag("bots");
 
    }


    // Update is called once per frame
    void Update()
    {

        float policeX = bots.transform.position.x;
     //   Debug.Log("POL POST" + policeX);
        dropTimer -= Time.deltaTime;
        if(dropTimer < 0)
        {
            // Debug.Log(bots.transform.position - transform.position);

            dropTimer = Random.Range(8f, 12f);
            if(Random.Range(0f, 1f) > 0.5f)
            {
                falling = true;
            }
        }
        else if (dropTimer < 2 && dropTimer > 0)
        {
            transform.position = initialPosition;
            falling = false;
        }
        if(falling)
        {
            transform.Translate(Vector3.down* Time.deltaTime * speed);

        }
    }
}
