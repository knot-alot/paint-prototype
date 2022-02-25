using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    bool _isdirty = false;
    [SerializeField] TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {

        pl = this.GetComponent<Player>();
        plMov = this.GetComponent<Assassin>();
        plCam = this.GetComponent<PlayerCamera>();
        runDeath();
    }

    public void runDeath()
    {
        _isdirty = true;
        time_remaing = respawnDelay;
        plMov.GetComponent<Rigidbody>().velocity = Vector3.zero;
        pl.praticle.Stop();
        pl.enabled = false;
        plMov.enabled = false;
        plCam.enabled = false;

        Vector3 deathPosition = gameObject.transform.position;
        gameObject.transform.position = respawnZone.transform.position;
        camObj.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));

        Instantiate(deathParticle, deathPosition, Quaternion.identity);

    }

    float time_remaing;
    private void Update()
    {
        if (_isdirty)
        {
            time_remaing -= Time.deltaTime;
            if (time_remaing <= 0)
            {
                _isdirty = false;
                pl.currentHealth = pl.maxHealth;
                gameObject.transform.position = respawnPoint.transform.position;

                plCam.enabled = true;
                pl.enabled = true;
                plMov.enabled = true;
                pl.TakeDamage(1);
            }
            text.text = ((int)time_remaing).ToString();
        }
    }



}
