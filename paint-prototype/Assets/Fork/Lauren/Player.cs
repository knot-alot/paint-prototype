using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public PaintTank paintTank;

    public GameObject text;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        paintTank.SetMaxHealth(maxHealth);
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        text.SetActive(false);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Interactable interactable = null;

        if (Physics.Raycast(ray, out hit, 30))
        {
            interactable = hit.collider.GetComponent<Interactable>();

            if (interactable)
            {
                text.SetActive(true);
            }
          
        }

        if (Input.GetKey(KeyCode.Return))
        {
            TakeDamage(1);
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (interactable)
            {
                CollectPaint(1);
            }

        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        paintTank.SetHealth(currentHealth);
    }

    void CollectPaint(int paint)
    {
        currentHealth += paint;

        paintTank.SetHealth(currentHealth);
    }

  
}
