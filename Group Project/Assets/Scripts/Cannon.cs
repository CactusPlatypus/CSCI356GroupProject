using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private CharacterController player;
    private Transform shotPoint;
    private AudioSource sound;
    private const float shootForce = 100f;

    public GameObject KalebRagdoll;
    public GameObject explosion;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
        sound = GetComponent<AudioSource>();
        shotPoint = transform.GetChild(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        sound.Play();

        // edit(hallam): Parent both to the cannon so it despawns with the road
        GameObject kaleb = Instantiate(KalebRagdoll, shotPoint.position, Quaternion.identity, transform);
        Instantiate(explosion, shotPoint.position, Quaternion.identity, transform);

        // Try to aim for the player based on their velocity
        Vector3 offset = player.transform.position + player.velocity * 0.5f;
        Vector3 direction = offset - shotPoint.position;
        Vector3 force = direction * shootForce;

        // This line is long because it is grabbing Kaleb's spine's rigidbody, not his overall one
        kaleb.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
}
