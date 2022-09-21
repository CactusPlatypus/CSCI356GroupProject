using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnedObject;
    [SerializeField] private float cooldown = 5f;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(cooldown);

        GameObject spawned = Instantiate(spawnedObject, transform.position, Quaternion.identity);
        // Clean up Kalebs after 5 seconds
        Destroy(spawned, 5f);

        StartCoroutine(Spawn());
    }
}
