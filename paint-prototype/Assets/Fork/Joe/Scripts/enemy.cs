using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField]
    ParticleSystem praticle;
    [SerializeField]
    ParticleSystem deathParticle;
    [SerializeField] private int maxHealth = 200;
    private int currentHealth;

    public bool usePaint = true;
    // Start is called before the first frame update

    void Start()
    {
        this.currentHealth = this.maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 angle = this.cam.transform.localEulerAngles;

        //praticle.transform.localEulerAngles = angle;

        //text.SetActive(false);
        //Ray ray = this.cam.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //Interactable interactable = null;

        //if (Physics.Raycast(ray, out hit, 30))
        //{
        //    interactable = hit.collider.GetComponent<Interactable>();

        //    if (interactable) this.text.SetActive(true);

        //}
        if (currentHealth == 0) this.PlayerDeath();

    }


    public void TakeDamage(int damage)
    {
        this.currentHealth -= damage;

        if (this.currentHealth < 0) this.currentHealth = 0;

    }

    public void AddPaint(int paint)
    {
        this.currentHealth += paint;

        if (this.currentHealth > this.maxHealth) this.currentHealth = this.maxHealth;

    }

    void PlayerDeath()
    {
        Instantiate(this.deathParticle, this.transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }


}

