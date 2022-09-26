using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpin : MonoBehaviour
{
    // Start is called before the first frame update
    private float lifetime = 3.0f;
    private float spinSpeed = 300.0f;
    public void setLifeTime(float lifetime)
    {
        this.lifetime = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * spinSpeed);


        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
