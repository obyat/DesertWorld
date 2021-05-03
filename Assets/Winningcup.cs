using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winningcup : MonoBehaviour
{
    public ParticleSystem fireworks;
    public AudioSource soruce;
    public GameObject psfireworks;
    public ParticleSystem psglow;

    // Start is called before the first frame update
    void Start()
    {
        psglow.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
