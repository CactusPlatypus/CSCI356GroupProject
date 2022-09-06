using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFly : MonoBehaviour
{
    public float launchForce = 50.0f;
    Vector3 m_NewForce;
    // Start is called before the first frame update
    void Awake()
    {
        m_NewForce = new Vector3(1*launchForce, 1.0f*launchForce, 0.0f);
        GetComponent<Rigidbody>().AddForce(m_NewForce, ForceMode.Impulse);
    }
}
