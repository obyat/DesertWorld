using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{

    public HealthManager theHeatlthMan;

    public Renderer theRend;
    public Material cpOff;
    public Material cpOn;

    // Start is called before the first frame update
    void Start()
    {
        theHeatlthMan = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void checkPointOn()
    {
        checkPoint[] checkPoints = FindObjectsOfType<checkPoint>();
        foreach(checkPoint cp in checkPoints)
        {
            cp.checkPointOff();
        }
        theRend.material = cpOn;
    }

    public void checkPointOff()
    {
        theRend.material = cpOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            theHeatlthMan.setSpawnPoint(transform.position);
            checkPointOn();
        }
    }
}
