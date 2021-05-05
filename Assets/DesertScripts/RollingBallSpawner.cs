using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBallSpawner : MonoBehaviour
{
    public GameObject rollingBall;
    public float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if(spawnTime < 0)
        {
            Instantiate(rollingBall, new Vector3(Random.Range(3f, 18f), 20f, 142f), Quaternion.identity);
            spawnTime = 1f;
        }
    }
}
