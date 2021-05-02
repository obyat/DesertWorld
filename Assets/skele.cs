using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skele : MonoBehaviour
{

    Animator animator;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        time = Random.Range(2f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0){
            animator.SetBool("isWalk", true);
            time = Random.Range(2F, 4f);
        } else if(time < 2) {
            animator.SetBool("isWalk", false);
        } else {
            transform.Translate(transform.forward * Time.deltaTime);
        }
    }
}
