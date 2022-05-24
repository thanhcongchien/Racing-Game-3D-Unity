using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    public Button useItemButton;
    // Start is called before the first frame update
    void Start()
    {
        useItemButton.GetComponent<Button>();
        useItemButton.onClick.AddListener(useItem);
    }

    public void useItem()
    {
        if (useItemButton != null)
        {
            if (Photon.Pun.Demo.PunBasics.PlayerManager.instance.isLocalPlayer == true)
            {
                if (ItemManager.itemManagerInstance.current_Item == "Gun")
                {
                    Gun.gunInstance.shootGun();
                }
                else
                {
                    ItemManager.itemManagerInstance.ReadyToUseIem();
                }


            }
        }
    }
}
