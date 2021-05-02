using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botController : MonoBehaviour
{
    public CharacterController controller;
    public float gravityScale;
    public float jumpForce;
    public float moveSpeed;
    private Vector3 moveDirection; 

    public Animator anim;

    public Transform pivot;
    public float rotateSpeed;

    public Transform playerModel;

    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    public GameObject closestSnake;

    public Vector3 target;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // controller = GetComponent<CharacterController>();
        // closestSnake = FindClosestEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        closestSnake = FindClosestEnemy();

        target = closestSnake.transform.position;
        target.y = transform.position.y + (Physics.gravity.y * Time.deltaTime * gravityScale);

        if(target.y <= 1f)
        {
            target.y = 1f;
        } 

        if(Vector3.Distance(closestSnake.transform.position, transform.position) < 2f) 
        {
            target.y = jumpForce;
            anim.SetBool("isGrounded", false);
         }
        else
        {
            anim.SetBool("isGrounded", true);
        }

        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        // rb.AddForce(1f,0f,1f, ForceMode.Impulse);

        // Rotates bot model
        Vector3 targetDir = target - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(new Vector3(targetDir.x, 0f, targetDir.z));
        playerModel.rotation = Quaternion.Slerp(playerModel.rotation, newRotation, rotateSpeed*Time.deltaTime);

        // Animator Variables
        anim.SetFloat("speed", 1);
    }

    public void knockBack(Vector3 dir)
    {
        knockBackCounter = knockBackTime;
        moveDirection = dir * knockBackForce;
        moveDirection.y = knockBackForce;
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("snakes");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if(other.CompareTag("farmObjects"))
    //     {
    //         Vector3 hitDir = other.transform.position - transform.position;
    //         hitDir = hitDir.normalized;
    //         Vector3 pos = transform.position;

    //         knockBackCounter = knockBackTime;
    //         pos = hitDir * knockBackForce;
    //         pos.y = knockBackForce;

    //         // transform.position = pos; //Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);
    //         controller.Move(pos * Time.deltaTime);
    //         Debug.Log("Test");
    //     }
    // }
}
