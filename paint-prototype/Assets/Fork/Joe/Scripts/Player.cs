using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem praticle;
    
    [SerializeField] Camera cam;
    public int maxHealth = 1000;
    public int currentHealth;

    public PaintTank paintTank;

    public GameObject text;

    public bool usePaint = true;

    [SerializeField]
    public RespawnManager respawnManager;

    //[Header("Player Respawn")]
    //[SerializeField]
    //private Transform player;

    //[SerializeField]
    //private Transform respawnPointBlue;

    //[SerializeField]
    //public float respawnDelay = 1f;

    // Start is called before the first frame update

    void Start()
    {
        currentHealth = maxHealth;
        paintTank.SetMaxFill(maxHealth);
        respawnManager = this.GetComponent<RespawnManager>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 angle = cam.transform.localEulerAngles;

        if (Input.GetButtonDown("UnlimPaint")) usePaint = !usePaint;

        if (Input.GetMouseButtonDown(0))
        {
            
            if (currentHealth > 0) {
                
                praticle.Play();
            }
        
        }

        if(Input.GetMouseButton(0) && usePaint) TakeDamage(1);

        else if (Input.GetMouseButtonUp(0))
        {
            praticle.Stop();
        }

        

        praticle.transform.localEulerAngles = angle;

    
       text.SetActive(false);

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        Interactable interactable = null;
        int layermask = 1 << 8;
        layermask = ~layermask;
        if (Physics.Raycast(transform.position, fwd, out hit, 100.0f, layermask))
        {
            interactable = hit.collider.GetComponent<Interactable>();

            if (interactable) text.SetActive(true);
          
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (interactable) AddPaint(1);
        }

        if (currentHealth == 0) PlayerDeath();
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0) currentHealth = 0;

        paintTank.SetFill(currentHealth);
    }

    public void AddPaint(int paint)
    {
        currentHealth += paint;

        if (currentHealth > maxHealth) currentHealth = maxHealth;

        paintTank.SetFill(currentHealth);
    }

    void PlayerDeath()
    {
        respawnManager.runDeath();
    }



}
