using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMesh : MonoBehaviour
{

    public float lookRadius = 10f;
    public Animator anim;

    GameObject target;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        // agent = GetComponent<NavMeshAgent>();
        target = FindClosestEnemy();
        anim.SetFloat("speed", 1);
    }

    // Update is called once per frame
    void Update()
    {
        target = FindClosestEnemy();

        agent.SetDestination(target.transform.position);

        float dist = Vector3.Distance(transform.position, target.transform.position);

        if(dist < 5f)
        {
            anim.SetBool("isGrounded", false);
        }
        else
        {
            anim.SetBool("isGrounded", true);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
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

}
