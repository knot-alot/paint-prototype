using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    ParticleSystem praticle;
    [SerializeField] Camera cam;
    public int maxHealth = 1000;
    public int currentHealth;

    public PaintTank paintTank;

    public GameObject text;
   // Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        paintTank.SetMaxFill(maxHealth);
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 angle = cam.transform.localEulerAngles;
        if (Input.GetMouseButton(0))
        {
            TakeDamage(1);
            if (currentHealth > 0) {
                praticle.Play();
            }
        
        }
        if (Input.GetMouseButtonUp(0))
        {
            praticle.Stop();
        }
        praticle.transform.localEulerAngles = angle;

    
    text.SetActive(false);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Interactable interactable = null;

        if (Physics.Raycast(ray, out hit, 30))
        {
            interactable = hit.collider.GetComponent<Interactable>();

            if (interactable) text.SetActive(true);
          
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (interactable) AddPaint(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0) currentHealth = 0;

        paintTank.SetFill(currentHealth);
    }

    void AddPaint(int paint)
    {
        currentHealth += paint;

        if (currentHealth > maxHealth) currentHealth = maxHealth;

        paintTank.SetFill(currentHealth);
    }

  
}
