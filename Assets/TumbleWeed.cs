using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleWeed : MonoBehaviour
{
    float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
    speed = Random.Range(10f, 100f);
    
    //Debug.Log("speed is:" +speed );
    GetComponent<Rigidbody>().velocity = Random.onUnitSphere * speed; 
    GetComponent<Rigidbody>().AddRelativeForce(Random.onUnitSphere * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
