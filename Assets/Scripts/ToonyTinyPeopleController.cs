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
