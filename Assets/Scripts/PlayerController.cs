using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    public bool enableInput;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        enableInput = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float veriticalInput = Input.GetAxis("Vertical");
        if(knockBackCounter < 0)
        {
            float yStore = moveDirection.y;

            moveDirection = (transform.forward * veriticalInput) + (transform.right * horizontalInput);
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = yStore;
            
            if(controller.isGrounded)
            {
                moveDirection.y = 0f;
                if(Input.GetButtonDown("Jump")) 
                {
                    moveDirection.y = jumpForce;
                    anim.SetBool("isGrounded", false);
                }
                else
                {
                    anim.SetBool("isGrounded", true);
                }
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * Time.deltaTime * gravityScale);
        controller.Move(moveDirection * Time.deltaTime);

        // Moving Player in different directoins based on camera look direction
        if(horizontalInput != 0 || veriticalInput != 0) 
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.rotation = Quaternion.Slerp(playerModel.rotation, newRotation, rotateSpeed*Time.deltaTime);
        }

        // Animator Variables
        anim.SetFloat("speed", (Mathf.Abs(horizontalInput) + Mathf.Abs(veriticalInput)));
        if(transform.position.y < -30f)
        {
            transform.position = new Vector3(99f, 19f, 161f);
        }
    }

    public void knockBack(Vector3 dir)
    {
        knockBackCounter = knockBackTime;
        moveDirection = dir * knockBackForce;
       moveDirection.y = knockBackForce;
    }

    public void knockBackX(Vector3 dir)
    {
        knockBackCounter = knockBackTime;
        moveDirection = dir * knockBackForce;
     //   moveDirection.y = knockBackForce;
    }
    public void setEnableInput(bool b)
    {
        enableInput = b;
    }
    
    

        private void OnTriggerEnter(Collider other)
    {

       if(other.CompareTag("cup")  && other.gameObject !=null)
        {
        GameObject found = new List<GameObject>(GameObject.FindGameObjectsWithTag("cup"))
        .Find(g => g.transform.IsChildOf( this.transform));
        found.GetComponent<Renderer>().enabled = true;
        moveSpeed += 10f;
        // ThisAgent.acceleration = ThisAgent.acceleration+100f;
        other.GetComponent<Renderer>().enabled = false;
        Debug.Log("Player Took cup!!");
        GameObject.FindGameObjectWithTag("winningCup").GetComponent<AudioSource>().Play();

        GameObject ps = Instantiate(
           other.gameObject.GetComponent<Winningcup>().psfireworks,  other.gameObject.transform.position, 
           UnityEngine.Quaternion.LookRotation(transform.position));
    
        GameObject ps2 = Instantiate(
           other.gameObject.GetComponent<Winningcup>().bridgeExplosion,  other.gameObject.transform.position, 
           UnityEngine.Quaternion.LookRotation(transform.position));

        Destroy(other.gameObject);
        }
        if(other.CompareTag("bridge"))
        {
            other.gameObject.GetComponent<Renderer>().enabled=true;
        }
    }

}
