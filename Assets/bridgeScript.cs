using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeScript : MonoBehaviour
{
    public GameObject Bwater;
    public GameObject Dlight1;
    public GameObject Dlight2;

    // Start is called before the first frame update
    void Start()
    {
        Bwater.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("bots"))
        {
            Bwater.SetActive(true);
            Dlight1.SetActive(false);
            Dlight2.SetActive(false);


        }
    }
}
