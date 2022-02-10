using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    ParticleSystem praticle;
    [SerializeField]Camera cam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 angle = cam.transform.localEulerAngles;
        if (Input.GetMouseButtonDown(0)) {
            praticle.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            praticle.Stop();
        }
        praticle.transform.localEulerAngles = angle; 
       
    }

}
