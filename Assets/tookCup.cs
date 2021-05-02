using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tookCup  : MonoBehaviour
{
   public Renderer rend;



    private void Start() {
        Renderer rend = gameObject.AddComponent<Renderer>() as Renderer;
        rend =  GetComponent<Renderer>();
        rend.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}