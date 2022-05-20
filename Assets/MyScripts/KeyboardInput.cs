using UnityEngine;
using Photon.Pun.Demo.PunBasics;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

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
        //reset pre position when the player was falled out road
        private Vector3 currentPos;
        public GameObject playerMgr;
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
            //currentPos = new Vector3(14.43f, 0.88f, 3f);
        }
        public override Vector2 GenerateInput() {
            if (isReady == true)
            {
                if(this.ownerKart.gameObject.GetComponent<PlayerManager>().isRotated == false)
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
            ResetPlayerPosition();
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
                if (Input.touchCount == 2)
                {
                
                    if (NitroScript.instance != null)
                    {

                        isBootNitro = true;
                        if (NitroScript.instance.playerNitroSlider.value >= 1f)
                        {
                            //this.gameObject.GetComponent<ArcadeKart>().baseStats.TopSpeed += 50;
                            if (this.ownerKart.gameObject.GetComponent<ArcadeKart>() != null && PlayerManager.instance.isLocalPlayer == true)
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

        public void ResetPlayerPosition()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerMgr.transform.position = currentPos;
            }
            StartCoroutine(GetCurrentPosition());
        }

        public IEnumerator GetCurrentPosition()
        {
            Vector3 prePosition = playerMgr.transform.position;
            yield return new WaitForSeconds(2);
            currentPos = prePosition;
        }

        // not working
        private void OnTriggerStay(Collider other)
        {
            if(other.gameObject.name == "GroundPlane")
            {
                playerMgr.transform.position = new Vector3(14.43f, 0.88f, 3f);
                Debug.Log("on the ground plane !!!!!!!!1");
            }
            
        }
    }
}
