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

    void Start()
    {
        this.currentHealth = this.maxHealth;
        this.paintTank.SetMaxFill(this.maxHealth);

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 angle = this.cam.transform.localEulerAngles;

        if (Input.GetButtonDown("UnlimPaint")) this.usePaint = !this.usePaint;

        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (currentHealth > 0)
        //    {
        //        this.praticle.Play();
        //    }

        //}
        //if (Input.GetMouseButton(0))
        //{
        //    if (this.usePaint) this.TakeDamage(1);
        //}

        //else if (Input.GetMouseButtonUp(0))
        //{
        //    this.praticle.Stop();
        //}



        praticle.transform.localEulerAngles = angle;


        text.SetActive(false);
        Ray ray = this.cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Interactable interactable = null;

        if (Physics.Raycast(ray, out hit, 30))
        {
            interactable = hit.collider.GetComponent<Interactable>();

            if (interactable) this.text.SetActive(true);

        }

        if (Input.GetKey(KeyCode.E))
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
        Instantiate(this.deathParticle, this.transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }


}

