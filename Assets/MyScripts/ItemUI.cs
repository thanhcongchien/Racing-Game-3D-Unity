using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : TargetObject
{

    public Sprite[] itemIcon;
    Transform targetTransform;

    Renderer targetRenderer;

    CanvasGroup _canvasGroup;

    Vector3 targetPosition;

    public GameObject your_item;


    void Awake()
    {
        _canvasGroup = this.GetComponent<CanvasGroup>();

        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if (ItemManager.itemManagerInstance.current_Item == "Banana")
        // {
        //     your_item.GetComponent<Image>().sprite = items_possible[0];
        // }
        // else if (ItemManager.itemManagerInstance.current_Item == "Bomb")
        // {
        //     your_item.GetComponent<Image>().sprite = items_possible[1];
        // }
        // else if (ItemManager.itemManagerInstance.current_Item == "Gun")
        // {
        //     your_item.GetComponent<Image>().sprite = items_possible[2];
        // }
        if (ItemManager.itemManagerInstance.current_Item != "")
        {
            handOn_Item_UI(ItemManager.itemManagerInstance.current_Item);
        }
        else
        {
            your_item.GetComponent<Image>().gameObject.SetActive(false);
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if ((layerMask.value & 1 << other.gameObject.layer) > 0 && other.gameObject.CompareTag("Player"))
        {

        }
    }

    public void handOn_Item_UI(string current_Item_Handon)
    {
        if (your_item != null)
        {
            if (Photon.Pun.Demo.PunBasics.PlayerManager.instance.isLocalPlayer == true)
            {
                if (current_Item_Handon != "")
                {
                    if (current_Item_Handon == "Banana")
                    {
                        your_item.GetComponent<Image>().gameObject.SetActive(true);
                        your_item.GetComponent<Image>().sprite = itemIcon[0];
                    }
                    else if (current_Item_Handon == "Bomb")
                    {
                        your_item.GetComponent<Image>().gameObject.SetActive(true);
                        your_item.GetComponent<Image>().sprite = itemIcon[1];
                    }
                    else if (current_Item_Handon == "Gun")
                    {
                        your_item.GetComponent<Image>().gameObject.SetActive(true);
                        your_item.GetComponent<Image>().sprite = itemIcon[2];
                    }
                }
                else
                {
                    your_item.GetComponent<Image>().gameObject.SetActive(false);
                }
            }
        }

    }

}
