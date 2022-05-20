using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject GunObj;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 0.1f;
    public int bullletAmount = 5;
    public static Gun gunInstance;
    void Awake()
    {
        if (gunInstance == null)
        {
            gunInstance = GetComponent<Gun>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            var bulllet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bulllet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.up * bulletSpeed;
            //Amount of bullets
            bullletAmount--;
            if (bullletAmount == 0)
            {
                this.GunObj.gameObject.SetActive(false);
                bullletAmount = 5;
                ItemManager.itemManagerInstance.current_Item = "";
            }
        }
    }

}
