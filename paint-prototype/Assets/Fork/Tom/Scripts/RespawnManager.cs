using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{

    [Header("Player Respawn")]
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform respawnPoint;

    [SerializeField]
    private Transform respawnZone;
    [SerializeField]
    public float respawnDelay = 5.0f;

    [SerializeField]
    ParticleSystem deathParticle;

    Player pl;
    Assassin plMov;
    PlayerCamera plCam;

    [SerializeField]
    GameObject camObj;
    // Start is called before the first frame update
    void Start()
    {
        pl = this.GetComponent<Player>();
        plMov = this.GetComponent<Assassin>();
        plCam = this.GetComponent<PlayerCamera>();
        StartCoroutine(playerDeath());
    }

    public void runDeath()
    {
        StartCoroutine(playerDeath());
    }


    IEnumerator playerDeath()
    {
        plMov.GetComponent<Rigidbody>().velocity = Vector3.zero;
        pl.praticle.Stop();
        pl.enabled = false;
        plMov.enabled = false;
        plCam.enabled = false;

        Vector3 deathPosition = gameObject.transform.position;
        gameObject.transform.position = respawnZone.transform.position;
        Quaternion camRot = camObj.transform.rotation;
        camObj.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));

        Instantiate(deathParticle, deathPosition, Quaternion.identity);

        yield return new WaitForSeconds(respawnDelay);

        pl.currentHealth = pl.maxHealth;
        gameObject.transform.position = respawnPoint.transform.position;
        camObj.transform.rotation = camRot;

        plCam.enabled = true;
        pl.enabled = true;
        plMov.enabled = true;
        pl.TakeDamage(1);
    }

}
