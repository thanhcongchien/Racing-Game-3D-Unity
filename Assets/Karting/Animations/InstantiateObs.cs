using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObs : MonoBehaviour
{
    public Transform chien;
    public Rigidbody ball;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBall", 3, 3);
    }

    // Update is called once per frame

    void SpawnBall()
    {
        Rigidbody ballObj;
        ballObj = Instantiate(ball, chien.position, chien.rotation);
    }

}
