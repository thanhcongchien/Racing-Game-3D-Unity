using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DestroyBallObject", 11, 5);
    }

    // Update is called once per frame
   public void FixedUpdate() {
    GetComponent<Rigidbody>().AddForce(Physics.gravity * 2f, ForceMode.Acceleration);
}
    private void DestroyBallObject()
    {
        Destroy(this.gameObject);
    }
}
