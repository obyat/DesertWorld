

    

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class explode : MonoBehaviour
{
    public LayerMask enemy;
    public float maxdamage = 40f;
    public float explosionforce = 1000f;
    public float maxlifetime = 5f;
    public float explosionradius = 5f;
    bool knockBackbool;
    Vector3 direction;
    public GameObject bots;

    // Start is called before the first frame update
    void Start()
    {
                bots = GameObject.FindGameObjectWithTag("bots");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator KnockBackfan(){
        knockBackbool = true;
        bots.GetComponent<NavMeshAgent>().isStopped = true;
        bots.GetComponent<Rigidbody>().isKinematic = false;
        bots.GetComponent<NavMeshAgent>().enabled = false;
        bots.AddComponent<Rigidbody>();
        Debug.Log("HIT BOT");

       bots.GetComponent<NavMeshAgent>().speed = 100f;//bots.GetComponent<NavMeshAgent>().velocity * 40 ;
       // bots.GetComponent<Rigidbody>().AddForce(-transform.forward*10, ForceMode.Impulse);

     yield return new WaitForSeconds(0.2f);
        knockBackbool = false;
        bots.GetComponent<NavMeshAgent>().isStopped = false;
        bots.GetComponent<Rigidbody>().isKinematic = true;
        bots.GetComponent<NavMeshAgent>().enabled = true;
        
    }

private void OnTriggerEnter(Collider other)
{

        if(other.CompareTag("bots")){
            Debug.Log("hitfan");
            StartCoroutine(KnockBackfan());
        }

}
}