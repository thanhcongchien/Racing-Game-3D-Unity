using UnityEngine;
using Photon.Pun.Demo.PunBasics;
using System.Collections.Generic;
namespace KartGame.KartSystems {

    public class KeyboardInput : BaseInput
    {
        public static KeyboardInput instance;
        public string Horizontal = "Horizontal";
        public string Vertical = "Vertical";
        public bool isBootNitro = false;
        public bool resetNitro = false;
        public GameObject ownerKart;
        public GameObject NitroVFX;

        private void Awake()
        {
            if(instance == null)
            {
                instance = GetComponent<KeyboardInput>();
            }
        }
        public override Vector2 GenerateInput() {
                return new Vector2 {
                x = Input.GetAxis(Horizontal),
                y = Input.GetAxis(Vertical)
            };             
        }

        void Update()
        {
            NitroButton();
           
            if (NitroScript.instance != null)
            {
                if (resetNitro)
                {
                    NitroScript.instance.AfterBootNitro();
                }

            }
        }
        
        public void NitroButton()
        {
                if (Input.GetKeyDown("space"))
                {

                    if (NitroScript.instance != null)
                    {

                        isBootNitro = true;
                        if (NitroScript.instance.playerNitroSlider.value >= 1f)
                        {
                            //this.gameObject.GetComponent<ArcadeKart>().baseStats.TopSpeed += 50;
                            if (ownerKart.gameObject.GetComponent<ArcadeKart>() != null && Photon.Pun.Demo.PunBasics.PlayerManager.instance.isLocalPlayer == true)
                            {
                                NitroVFX.gameObject.SetActive(true);
                                ownerKart.gameObject.GetComponent<ArcadeKart>().baseStats.TopSpeed += 50;
                            }
                            resetNitro = true;
                        }
                        Debug.Log("baseStats.TopSpeed after pressing space is " + ownerKart.gameObject.GetComponent<ArcadeKart>().baseStats.TopSpeed);
                    }
                    else
                    {
                        Debug.Log("asdsadasdas");
                    }
                    print("space key was pressed");
                }
        }
    }
}
