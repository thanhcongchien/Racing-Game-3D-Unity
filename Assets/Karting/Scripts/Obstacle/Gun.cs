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

    public void shootGun(){
        var bulllet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bulllet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.up * bulletSpeed;
            //Amount of bullets
            bullletAmount--;
            SoundManager.Instance.PlaySFX(SoundManager.SHOOT_SFX);
            if (bullletAmount == 0)
            {
                Debug.Log("No more bullets");
                this.GunObj.gameObject.SetActive(false);
                bullletAmount = 5;
                ItemManager.itemManagerInstance.current_Item = "";
            }
    }

}
