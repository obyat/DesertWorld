    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToonyTinyPeopleController : MonoBehaviour
{
    //Reference to nav mesh agent
	private UnityEngine.AI.NavMeshAgent ThisAgent = null;
    private Animator animator;
    private GameObject[] Destinations;
    public string dest;
    private int nextIndex=1;
    public string finalDest;
    public Rigidbody body = null;
    public float knockBackTime;
    private int prev_dest = 0;
    private AudioSource trophySound;

    // Start is called before the first frame update
    void Start()
    {
        dest = "Dest1";
        ThisAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        body = GetComponent<Rigidbody>();
        animator = transform.GetComponent<Animator>();
        Destinations = GameObject.FindGameObjectsWithTag(dest);
        ThisAgent.SetDestination(Destinations[Random.Range(0,Destinations.Length)].transform.position);
        ThisAgent.isStopped = false;
        ThisAgent.stoppingDistance = 2f;
        ThisAgent.speed = 8f;
        prev_dest = 0;
        animator.SetBool("isMoving", true);
        knockBackTime = 2f;
     
    }

    // Update is called once per frame
    void Update()
    {
        knockBackTime -= Time.deltaTime;
        if(body.velocity.magnitude <= 0.05f && knockBackTime < 0)
        {
            body.isKinematic = true;
            ThisAgent.enabled = true;
        } 
        if (ThisAgent.remainingDistance <= ThisAgent.stoppingDistance && dest == finalDest)
        {
            animator.SetBool("isMoving", false);
        }
        else if (ThisAgent.remainingDistance <= ThisAgent.stoppingDistance)
        {
            updateDest();
            animator.SetBool("isMoving", true);
            ThisAgent.Resume();

        }
    }

    private void updateDest()
    {
        nextIndex++;
        dest = "Dest" + nextIndex;
        Destinations = GameObject.FindGameObjectsWithTag(dest);
        prev_dest = Random.Range(0,Destinations.Length);
        ThisAgent.SetDestination(Destinations[prev_dest].transform.position);

    }
public void knockBack(Vector3 dir)
    {
        /*
        1. each agent has dest
        2. each agent has their own dest updated

        -> for all instances of agent -> dest = dest11

        */
        // ThisAgent.isStopped = true;
        // body.isKinematic = false;
        // ThisAgent.enabled = false;
        // body.AddForce(dir*100, ForceMode.Impulse);
        // knockBackTime = 0.1f;
        // animator.SetBool("isMoving", true);
        ThisAgent.SetDestination(Destinations[prev_dest].transform.position);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Vector3 hitDirBot = transform.position - other.transform.position;
            Vector3 hitDirPlayer = other.transform.position - transform.position;
            hitDirBot = hitDirBot.normalized;
            this.knockBack(hitDirBot);
            FindObjectOfType<PlayerController>().knockBackX(hitDirPlayer);

        }

        if(other.CompareTag("cup") && other.gameObject !=null )
        {
        GameObject found = new List<GameObject>(GameObject.FindGameObjectsWithTag("cup"))
        .Find(g => g.transform.IsChildOf( this.transform));
        found.GetComponent<Renderer>().enabled = true;
    
        ThisAgent.speed += 10f;
        other.GetComponent<Renderer>().enabled = false;
        Debug.Log("AI Took cup!!");
        GameObject.FindGameObjectWithTag("winningCup").GetComponent<AudioSource>().Play();
        
        GameObject ps = Instantiate(
           other.gameObject.GetComponent<Winningcup>().psfireworks,  other.gameObject.transform.position, 
           UnityEngine.Quaternion.LookRotation(transform.position));
        
        Destroy(other.gameObject);
    
        }
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;

// public class ToonyTinyPeopleController : MonoBehaviour
// {
//     //Reference to nav mesh agent
// 	private UnityEngine.AI.NavMeshAgent ThisAgent = null;
//     private Animator animator;
//     private GameObject[] Destinations;
//     public string dest;
//     private int nextIndex;
//     public string finalDest;
//     public Rigidbody body = null;
//     public GameObject bots;
//     public NavMeshAgent navAgent;
//     private Animator anim;
//     bool knockBackbool;
//     Vector3 direction;

//     // Start is called before the first frame update
//     void Start()
//     {
//         dest = "Dest1";
//         ThisAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
//         body = GetComponent<Rigidbody>();
//         animator = transform.GetComponent<Animator>();
//         Destinations = GameObject.FindGameObjectsWithTag(dest);
//         ThisAgent.SetDestination(Destinations[Random.Range(0,Destinations.Length)].transform.position);
//         ThisAgent.isStopped = false;
//         ThisAgent.stoppingDistance = 2f;
//         ThisAgent.speed = 8f;
//         bots = GameObject.FindGameObjectWithTag("bots");
//         Vector3 direction;


//         animator.SetBool("isMoving", true);
//     }
//     /*
// make function with input of speed and last destination
// save these vars

// to simulate force
// make speed drastic
// set destination behind them

// reset to initial saved vars
// exit function

//     */

//     // Update is called once per frame
//     void Update()
//     {
//         // Vector3 ap = ThisAgent.transform.position;
//         // ap.x = ap.x;
//         // ap.y = ap.y;
//         // ap.z = ap.z;
        
//         // ThisAgent.velocity = new Vector3(ap.x, ap.y, ap.z);
//         ThisAgent.speed = 5f;
//         // ThisAgent.acceleration = 50f;
// /*
// 1. set openning of the gate as destination (behind gate so they have to wait for animation)
// 2. trigger on destination -> 
// 3. set cup as destination
// 4. cannons with obsticle from random directions with force (existing code in hw5)
// 5. -> wait 3f seconds destroy cannon Object.Destroy(gameObject, 2.0f);
// 6. use existing fans and slime
// 7. if ANYONE including player touches cup new destinatin for all -> outside gate
// => updatedes + 1 (reset all dest? or skip one?)
// */
//         if(body.velocity.magnitude <= 0.5f)
//         {
//             body.isKinematic = true;
//             ThisAgent.enabled = true;
//             // Debug.Log(body.velocity.magnitude);
//         }
//         if (ThisAgent.remainingDistance <= ThisAgent.stoppingDistance && dest == finalDest)
//         {
//             animator.SetBool("isMoving", false);
//         }
//         //ele if close to gate dest call gate dest
//         else if (ThisAgent.remainingDistance <= ThisAgent.stoppingDistance)
        
//         {
//             updateDest();
//         }
//     }

//         //inside gate destin


//     private void updateDest()
//     {

//         nextIndex++;
//         dest = "Dest" + nextIndex;
//         Destinations = GameObject.FindGameObjectsWithTag(dest);
//         ThisAgent.SetDestination(Destinations[Random.Range(0,Destinations.Length)].transform.position);
//     }
//     public void knockBack(Vector3 dir)
//     {
//         // body.isKinematic = false;
//         // ThisAgent.enabled = false;
//         // // body.AddForce(dir * ThisAgent.speed);
//         // body.AddForce(-100, -100, -100, ForceMode.Force);
//         // // Debug.Log(body.velocity.magnitude);
//         // // Debug.Log(dir * ThisAgent.speed);
//         // bots.GetComponent<NavMeshAgent>().isStopped = true;
//         // bots.GetComponent<Rigidbody>().isKinematic = false;
//         // bots.GetComponent<NavMeshAgent>().enabled = false;
//         // bots.GetComponent<Rigidbody>().AddForce(-transform.forward*10, ForceMode.Impulse);
//         // // Debug.Log("POLICE DIST   " + Mathf.Abs(policeX - transform.position.x));
//     }


// ----------------
// |f 
// |    f
// |               
// -----------------

// //get cup if player touches cup update des
// //-> cup wil have to be in its own script
// //dust
//     private void OnTriggerEnter(Collider other)
//     {
//         if(other.CompareTag("Player"))
//         {
//             Vector3 hitDir = other.transform.position - transform.position;
//             hitDir = hitDir.normalized;
//             hitDir.y = ThisAgent.speed;
//             FindObjectOfType<PlayerController>().knockBack(hitDir);
//             knockBack(hitDir);
//         }
//         //get tag of cup from here and from player if on trigger enter = cup this.speed++
//             //thisagent.cup.rendermersh
//             //or player same 
//             //play shiney sound


//     }

    
// }
