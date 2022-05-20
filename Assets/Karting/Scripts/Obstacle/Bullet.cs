using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float life = 3;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    

    
}
