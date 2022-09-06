using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTest : MonoBehaviour
{
    private Transform shotPoint;
    public GameObject KalebRagdoll; 
    // Start is called before the first frame update
    void Start()
    {
        shotPoint = this.gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")){
            Instantiate(KalebRagdoll, shotPoint.position, Quaternion.identity);
        }
    }
}
