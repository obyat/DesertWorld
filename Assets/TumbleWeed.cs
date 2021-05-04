using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleWeed : MonoBehaviour
{
    float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
     GetComponent<Rigidbody>().velocity = Random.onUnitSphere * speed; 
    GetComponent<Rigidbody>().AddRelativeForce(Random.onUnitSphere * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
