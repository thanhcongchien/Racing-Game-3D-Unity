using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {

        //     var bulllet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        //     bulllet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.up * bulletSpeed;
        //     //Amount of bullets
        //     bullletAmount--;
        //     if (bullletAmount == 0)
        //     {
        //         this.GunObj.gameObject.SetActive(false);
        //         bullletAmount = 5;
        //         ItemManager.itemManagerInstance.current_Item = "";
        //     }
        // }
    }

    public void shootGun(){
        var bulllet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bulllet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.up * bulletSpeed;
            //Amount of bullets
            bullletAmount--;
            if (bullletAmount == 0)
            {
                Debug.Log("No more bullets");
                this.GunObj.gameObject.SetActive(false);
                bullletAmount = 5;
                ItemManager.itemManagerInstance.current_Item = "";
            }
    }

}
