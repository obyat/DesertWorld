using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
public class tookCup  : MonoBehaviour
{
   public Renderer rend;



    private void Start() {
        if(gameObject.GetComponent<Renderer>()!=null){
        rend =  GetComponent<Renderer>();
        rend.enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}