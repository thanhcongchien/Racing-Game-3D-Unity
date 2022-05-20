using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rb;

    public float throwForceUp;
    public float throwForceForward;

    public float lifetime;

    [HideInInspector]
    public string whoThrewBanana;
    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move();
        lifetime += Time.deltaTime;
    }

    // public void Move()
    // {
    //     rb.AddForce(Vector3.down * 5 * Time.deltaTime, ForceMode.Acceleration);

    // }

    // private void OnCollisionStay(Collision collision)
    // {
    //     if (collision.gameObject.tag == "Straight (5)Straight (5)")
    //     {
    //         groundNormalRotation();

    //         rb.velocity = Vector3.zero;
    //         // GetComponent<Animator>().SetTrigger("LandGround");
    //     }
    // }

    public void Banana_thrown(float extraForward)
    {
        // rb.AddForce(transform.up * throwForceUp * Time.deltaTime, ForceMode.Impulse);
        // rb.AddForce(-transform.forward * (throwForceForward + extraForward) * Time.deltaTime, ForceMode.Impulse);
    }

    void groundNormalRotation()
    {
        //ground normal rotation
        Ray ground = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ground, out hit, 10))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up * 2, hit.normal) * transform.rotation, 9f * Time.deltaTime);
        }
    }

     private void OnCollisionEnter(Collision collision){
            //  yield return new WaitForSeconds(0.01f);
            // if (collision.gameObject.tag == "Banana Peel(Clone)")//regular banana
            // {
                // if (this.lifetime > 0.5f)
                // {
                //     // StartCoroutine(hitByBanana());
                //     Debug.Log("hit by banana");
                //     Destroy(collision.gameObject);
                // }
                // if (itemManagerInstance.StarPowerUp)
                // {
                //     Destroy(collision.gameObject);
                // }
            }
        //  }

}