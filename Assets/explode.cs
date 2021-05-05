

    

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
        //bots = GameObject.FindGameObjectWithTag("bots");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator KnockBackfan(){

        Debug.Log("HIT BOT");
        gameObject.GetComponent<Renderer>().enabled=true;

       //bots.GetComponent<NavMeshAgent>().velocity * 40 ;
       // bots.GetComponent<Rigidbody>().AddForce(-transform.forward*10, ForceMode.Impulse);

     yield return new WaitForSeconds(0.2f);

        
    }

private void OnTriggerEnter(Collider other)
{

        if(other.CompareTag("bots")){
            Debug.Log("hitfan");

        }

}
}