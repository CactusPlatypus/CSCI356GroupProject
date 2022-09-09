using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTest : MonoBehaviour
{
    private Transform shotPoint;
    public GameObject KalebRagdoll;
    public GameObject explosion; 
    Vector3 m_NewForce;
    public float launchForce = 50.0f;
    
    void Start()
    {
        shotPoint = this.gameObject.transform.GetChild(0);
    }

    void Update()
    {
        if (Input.GetKeyDown("space")){
            Shoot();
        }
    }
    // Method currently called by spacebar, can be called by anything
    void Shoot(){
        gameObject.GetComponent<AudioSource>().Play();

        GameObject kaleb = Instantiate(KalebRagdoll, shotPoint.position, Quaternion.identity, this.gameObject.transform);
        GameObject explosionI = Instantiate(explosion, shotPoint.position, Quaternion.identity, this.gameObject.transform);

        StartCoroutine(DestroyExplosion(4f, explosionI));
        
        // Takes forward direction of cannon, and applies it to Kaleb
        m_NewForce = new Vector3(this.gameObject.transform.forward.x*launchForce, this.gameObject.transform.forward.y*launchForce, this.gameObject.transform.forward.z*launchForce);
        // This line is fucked because it is grabbing Kaleb's spine's rigidbody, not his overall one
        kaleb.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<Rigidbody>().AddForce(m_NewForce, ForceMode.Impulse);
    }
    IEnumerator DestroyExplosion(float seconds, GameObject explosionI){
        yield return new WaitForSeconds(seconds);
        Destroy(explosionI);
    }
}
