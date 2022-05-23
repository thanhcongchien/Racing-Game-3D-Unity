using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager itemManagerInstance;
    private PlayerManager playerManager;
    public bool start_select = false;

    public Sprite[] items_possible;
    public GameObject[] item_gameobjects;
    public GameObject your_item;

    [Header("ITEMS")]
    public GameObject Shell;
    public GameObject Banana;
    public GameObject Bomb;
    public GameObject Gun;
    public Transform shellSpawnPos;
    public Transform backshellPos; //also for bananas
    public Transform BananaSpawnPos;
    public Transform coinSpawnPos;
    public Transform bombSpawnPos;

    // [HideInInspector]
    public int item_index = 0;
    // [HideInInspector]
    public int tripleItemCount = 3;
    // [HideInInspector]
    public string current_Item;

    // health progress when recieved damage
    public bool isDamageRecieved = false;
    public float baseDamage = 1;

    public float damageRecieved;
    public bool isUsedItem = false;

    void Awake()
    {
        if (itemManagerInstance == null)
        {
            itemManagerInstance = GetComponent<ItemManager>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (playerManager.hasitem)
        // {
        if (start_select) //this ensures item select process does not begin until player has used up curret item
        {



        //     if (current_Item == "Banana")
        //     {
        //         if (Input.GetKeyDown(KeyCode.Space))
        //         {
        //             StartCoroutine(spawnBanana(1));
        //         }

        //     }
        //     else if (current_Item == "Bomb")
        //     {
        //         if (Input.GetKeyDown(KeyCode.Space))
        //         {
        //             StartCoroutine(spawnBomb(-1));
        //         }

        //     }
            if (current_Item == "Gun")
            {
                StartCoroutine(getGun());
            }

        }
        // }
    }

    public void ReadyToUseIem()
    {
        if (start_select) //this ensures item select process does not begin until player has used up curret item
        {



            if (current_Item == "Banana")
            {
                  StartCoroutine(spawnBanana(1));
                

            }
            else if (current_Item == "Bomb")
            {
                    StartCoroutine(spawnBomb(-1));

            }

        }
    }


    IEnumerator spawnBanana(int direction)
    {
        GameObject clone;
        if (direction == 1)//forward
        {
            if (tripleItemCount > 0)
            {
                yield return new WaitForSeconds(0.1f);
                clone = Instantiate(Banana, BananaSpawnPos.position, BananaSpawnPos.rotation);
                // clone.GetComponent<Banana>().Banana_thrown(transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity).z * 200);
                // clone.GetComponent<Banana>().whoThrewBanana = gameObject.name;
                tripleItemCount--;
                SoundManager.Instance.PlaySFX(SoundManager.ITEM_SFX);
                used_Item_Done();
            }

        }
    }

    IEnumerator spawnBomb(int direction)
    {
        GameObject clone;
        if (direction == -1)//forward
        {
            yield return new WaitForSeconds(0.1f);

            clone = Instantiate(Bomb, bombSpawnPos.position, bombSpawnPos.rotation);
            tripleItemCount--;
            SoundManager.Instance.PlaySFX(SoundManager.ITEM_SFX);
            used_Item_Done();
        }
    }
    IEnumerator getGun()
    {
        yield return new WaitForSeconds(0.1f);
        Gun.gameObject.SetActive(true);
        start_select = false;
    }


    void used_Item_Done()
    {
        if (tripleItemCount == 0)
        {
            start_select = false;
            current_Item = "";
            tripleItemCount = 3;
        }
    }

    // function has not tested 



    // function has not tested 
    // public void OnTriggerEnter(Collider other){
    //     if(other.gameObject.name == "bullet(Clone)"){
    //         isDamageRecieved = true;
    //         // damage of gun is 0.2
    //         dameRecipe(2);
    //     }
    // }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Bomb-Omb(Clone)")
        {
            isDamageRecieved = true;
            // damage of bomb is 0.3
            dameRecipe(0.3f);
        }
    }


    public void dameRecipe(float dameLevel)
    {
        damageRecieved = baseDamage * dameLevel;
        Debug.Log("damageRecieved: " + damageRecieved);
    }


}
