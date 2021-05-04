using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
        public Material nightbox;

    // Start is called before the first frame update
    void Start()
    {
        
    }
  private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("bots"))
        {
            RenderSettings.skybox = nightbox;

        }
    }
}
