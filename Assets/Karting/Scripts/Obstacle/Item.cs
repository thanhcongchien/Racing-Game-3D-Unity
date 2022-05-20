using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : TargetObject

{

    public string[] item_name = new string[] { "Banana", "Bomb", "Gun" };
    // public int itemIndex;
    public string itemInBox;
    [Tooltip("Destroy the spawned spawnPrefabOnPickup gameobject after this delay time. Time is in seconds.")]
    public float destroySpawnPrefabDelay = 10;
    [Tooltip("Destroy this gameobject after collectDuration seconds")]
    public float collectDuration = 0f;
    void Awake()
    {
        int itemIndex = Random.Range(0, item_name.Length);
        itemInBox = item_name[itemIndex];
    }
    void Start()
    {

    }

    void OnCollect()
    {
        if (CollectSound)
        {
            AudioUtility.CreateSFX(CollectSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        }
        // ItemManager.itemManagerInstance.current_Item = itemInBox;
        // ItemManager.itemManagerInstance.start_select = true;
        Objective.OnUnregisterPickup(this);
        TimeManager.OnAdjustTime(TimeGained);

        Destroy(gameObject, collectDuration);
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            var itemManager = other.gameObject;
            if (itemManager.GetComponent<ItemManager>() != null)
            {
                itemManager.GetComponent<ItemManager>().current_Item = itemInBox;
                itemManager.GetComponent<ItemManager>().start_select = true;
            }

            OnCollect();



        }
    }



}