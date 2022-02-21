using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reservoir : MonoBehaviour
{

    [SerializeField]
    ParticleSystem deathParticle;

    public int maxHealth = 100;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == 0) Death();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0) currentHealth = 0;
    }

    void Death()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
