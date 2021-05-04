using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeScript : MonoBehaviour
{
    public GameObject Bwater;
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

        }
    }
}
