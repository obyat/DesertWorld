using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Jumper : MonoBehaviour
{

    public GameObject bots;
    private Animator animator;
    private Animation anim;



    void Start()
    {
       
        bots = GameObject.FindGameObjectWithTag("police");
        animator = bots.transform.GetComponent<Animator>();

}


    // Update is called once per frame
    void Update()
    {


    }
    private void OnTriggerEnter(Collider other)
    {
                float policeX = bots.transform.position.x;

        if(other.CompareTag("police"))
        { 
                Debug.Log("POLICE HIT");

        animator.SetBool("isJumping", true);

            // animator.SetBool("isJumping", false);
        }

    }
}

    
