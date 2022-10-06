using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTest : MonoBehaviour
{
    private Transform player;
    private Transform shotPoint;
    private AudioSource sound;

    public GameObject KalebRagdoll;
    public GameObject explosion;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        sound = GetComponent<AudioSource>();
        shotPoint = transform.GetChild(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            float shootForce = Vector3.Distance(player.position, transform.position) * 100f;
            Shoot(shootForce);
        }
    }

    void Shoot(float shootForce)
    {
        sound.Play();

        // edit(hallam): Keep Kaleb unparented so the cannon doesn't move him after being shot
        GameObject kaleb = Instantiate(KalebRagdoll, shotPoint.position, Quaternion.identity);
        GameObject explosionI = Instantiate(explosion, shotPoint.position, Quaternion.identity, transform);

        // Destroy prefabs after 10 seconds
        Destroy(kaleb, 10f);
        Destroy(explosionI, 10f);

        // Takes forward direction of cannon, and applies it to Kaleb
        Vector3 force = transform.forward * shootForce;

        // This line is long because it is grabbing Kaleb's spine's rigidbody, not his overall one
        kaleb.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
}
