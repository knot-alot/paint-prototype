using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField]
    ParticleSystem praticle;
    [SerializeField]
    ParticleSystem deathParticle;
    [SerializeField] Camera cam;
    [SerializeField] private int maxHealth = 200;
    private int currentHealth;

    public PaintTank paintTank;

    public GameObject text;

    public bool usePaint = true;
    // Start is called before the first frame update

    [Header("Enemy Respawn")]
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform respawnPointRed;

    void Start()
    {
        this.currentHealth = this.maxHealth;
        this.paintTank.SetMaxFill(this.maxHealth);

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 angle = this.cam.transform.localEulerAngles;

        //if (Input.GetButtonDown("UnlimPaint")) this.usePaint = !this.usePaint;

        if (Input.GetButtonDown("Fire"))
        {

            this.praticle.Play();
        }

        if (Input.GetButton("Fire") && usePaint) TakeDamage(1);

        else if (Input.GetButtonUp("Fire"))
        {
            this.praticle.Stop();
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

            if (interactable) this.text.SetActive(true);

        }

        if (Input.GetButton("player2_X"))
        {
            if (interactable) this.AddPaint(1);
        }

        if (currentHealth == 0) this.PlayerDeath();

    }


    public void TakeDamage(int damage)
    {
        this.currentHealth -= damage;

        if (this.currentHealth < 0) this.currentHealth = 0;

        paintTank.SetFill(this.currentHealth);

    }

    public void AddPaint(int paint)
    {
        this.currentHealth += paint;

        if (this.currentHealth > this.maxHealth) this.currentHealth = this.maxHealth;

        paintTank.SetFill(this.currentHealth);
    }

    void PlayerDeath()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        if (gameObject.tag == "enemy")
        {
            currentHealth = 1000;
            player.transform.position = respawnPointRed.transform.position;
        }

        //Destroy(this.gameObject);
    }


}
