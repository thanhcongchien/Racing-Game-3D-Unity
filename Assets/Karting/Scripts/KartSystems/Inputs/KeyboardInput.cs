﻿using UnityEngine;
using Photon.Pun.Demo.PunBasics;
using System.Collections.Generic;
using System.Collections;

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
        public GameObject[] DriffVFX;
        private bool isReady = false;
        private void Awake()
        {
            if(instance == null)
            {
                instance = GetComponent<KeyboardInput>();
            }
        }
        private void Start()
        {
            StartCoroutine(isReadyToStart());
        }
        public override Vector2 GenerateInput() {
            if (isReady == true)
            {
                return new Vector2
                {
                    x = Input.GetAxis(Horizontal),
                    y = Input.GetAxis(Vertical)
                };
            }
            return new Vector2 { };

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
                            if (this.ownerKart.gameObject.GetComponent<ArcadeKart>() != null && Photon.Pun.Demo.PunBasics.PlayerManager.instance.isLocalPlayer == true)
                            {

                            this.ownerKart.gameObject.GetComponent<ArcadeKart>().baseStats.TopSpeed += 50;
                            if (this.ownerKart.gameObject.GetComponent<ArcadeKart>().baseStats.TopSpeed > 20)
                            {
                                NitroVFX.gameObject.SetActive(true);
                                foreach (GameObject driff in DriffVFX)
                                {
                                    driff.gameObject.SetActive(true);
                                }
                            }
                            }
                            resetNitro = true;
                        }
                        Debug.Log("baseStats.TopSpeed after pressing space is " + this.ownerKart.gameObject.GetComponent<ArcadeKart>().baseStats.TopSpeed);
                    }
                    else
                    {
                        Debug.Log("asdsadasdas");
                    }
                    print("space key was pressed");
                }
        }

        // After counting 3, 2, 1 then player can run
        public IEnumerator isReadyToStart()
        {
            yield return new WaitForSeconds(3f);
            this.isReady = true;
        }
    }
}
