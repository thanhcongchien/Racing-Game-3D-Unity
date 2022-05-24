// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerManager.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Used in PUN Basics Tutorial to deal with the networked player instance
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using ExitGames.Client.Photon;

namespace Photon.Pun.Demo.PunBasics
{

#pragma warning disable 649

    /// <summary>
    /// Player manager.
    /// Handles fire Input and Beams.
    /// </summary>
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region Public Fields

        [Tooltip("The current Health of our player")]
        public float Health = 1f;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        [Tooltip("The current nitro of our player")]
        public float Nitro = 1f;
        #endregion

        #region Private Fields

        [Tooltip("The Player's UI GameObject Prefab")]
        [SerializeField]
        private GameObject playerUiPrefab;

        [Tooltip("The Beams GameObject to control")]
        [SerializeField]
        private GameObject beams;

        [Tooltip("The Player's Nitro GameObject")]
        [SerializeField]
        private GameObject NitroObj;

        [Tooltip("The Player's Item GameObject")]
        [SerializeField]
        private GameObject ItemObj;

        [Tooltip("The Health GameObject to control")]
        [SerializeField]
        private GameObject HealthObj;
        public static PlayerManager instance;
        public List<GameObject> nitroUI;

        public bool isLocalPlayer = false;
        public bool isPicked = false;
        public float nitroItem = 0.2f;

        //this.player
        public GameObject KartPlayer;


        // random skin of kart
        public Material[] materialList;
        public GameObject skinMaterialBody;
        public GameObject skinMaterialKart;
        private int index;

        private const byte COLOR_CHANGE = 1;
        //True, when the user is firing
        bool IsFiring;
        public float lerpTime = 1f;
        public bool isRotated = false;

        public bool hasitem = false; //true when player hits itembox

        public GameObject KartObj;

        #endregion

        #region MonoBehaviour CallBacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        /// 

        private void OnEnable()
        {
            int randomSkin = Random.Range(0, materialList.Length - 1);
            Debug.Log("index: " + randomSkin);
            object[] datas = new object[] { randomSkin };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(COLOR_CHANGE, datas, raiseEventOptions, SendOptions.SendReliable);
        }




        public void Awake()
        {

            if (instance == null)
            {
                instance = GetComponent<PlayerManager>();
            }

            if (this.beams == null)
            {
                Debug.LogError("<Color=Red><b>Missing</b></Color> Beams Reference.", this);
            }
            else
            {
                this.beams.SetActive(false);
            }

            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instanciation when levels are synchronized
            if (photonView.IsMine)
            {
                LocalPlayerInstance = gameObject;
                isLocalPlayer = true;
            }

            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(gameObject);


        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        public void Start()
        {
            CameraWork _cameraWork = gameObject.GetComponent<CameraWork>();

            if (_cameraWork != null)
            {
                if (photonView.IsMine)
                {
                    _cameraWork.OnStartFollowing();
                }
            }
            else
            {
                Debug.LogError("<Color=Red><b>Missing</b></Color> CameraWork Component on player Prefab.", this);
            }

            // Create the UI
            if (this.playerUiPrefab != null)
            {
                GameObject _uiGo = Instantiate(this.playerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
            else
            {
                Debug.LogWarning("<Color=Red><b>Missing</b></Color> PlayerUiPrefab reference on player Prefab.", this);
            }
            // this.nitroItem = 0f;

            //create the nitro UI
            if (this.NitroObj != null)
            {
                if (photonView.IsMine)
                {
                    GameObject _uiNitro = Instantiate(this.NitroObj);
                    _uiNitro.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
                }

            }
            else
            {
                Debug.LogWarning("<Color=Red><b>Missing</b></Color> NitroObj reference on player Prefab.", this);
            }
            //create the Item UI
            if (this.ItemObj != null)
            {
                if (photonView.IsMine)
                {
                    GameObject _uiItem = Instantiate(this.ItemObj);
                    _uiItem.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
                }

            }
            else
            {
                Debug.LogWarning("<Color=Red><b>Missing</b></Color> ItemObj reference on player Prefab.", this);
            }

            //create the Health UI
            if (this.HealthObj != null)
            {
                if (photonView.IsMine)
                {
                    GameObject _healthItem = Instantiate(this.HealthObj);
                    _healthItem.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
                }

            }
            else
            {
                Debug.LogWarning("<Color=Red><b>Missing</b></Color> HealthObj reference on player Prefab.", this);
            }



            //if (photonView.IsMine)
            //{
            //    RandomSkinKart();
            //}
            PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;


#if UNITY_5_4_OR_NEWER
            // Unity 5.4 has a new scene management. register a method to call CalledOnLevelWasLoaded.
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
#endif
        }


        public override void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
            // Always call the base to remove callbacks
            base.OnDisable();

#if UNITY_5_4_OR_NEWER
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
#endif
        }
        private void NetworkingClient_EventReceived(EventData photonEvent)
        {
            byte eventCode = photonEvent.Code;
            if (eventCode == COLOR_CHANGE)
            {
                object[] datas = (object[])photonEvent.CustomData;
                int randomskin = (int)datas[0];
                if (materialList.Length > 0)
                {
                    var photonViews = UnityEngine.Object.FindObjectsOfType<PhotonView>();
                    foreach (var view in photonViews)
                    {
                        var player = view.gameObject;
                        Debug.Log("PLAYER: " + player);
                        //Objects in the scene don't have an owner, its means view.owner will be null
                        GameObject skinMaterialBody = player.transform.Find("KartSuspension/Kart/Kart_Body").gameObject;
                        skinMaterialBody.GetComponent<SkinnedMeshRenderer>().material = materialList[randomskin];
                        Debug.Log("randomskin :" + randomskin);

                    }

                }

            }

        }


        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity on every frame.
        /// Process Inputs if local player.
        /// Show and hide the beams
        /// Watch for end of game, when local player health is 0.
        /// </summary>
        public void Update()
        {
            // we only process Inputs and check health if we are the local player
            if (photonView.IsMine)
            {
                this.ProcessInputs();
                isLocalPlayer = true;
                if (this.Health <= 0f)
                {
                    GameManager.Instance.LeaveRoom();
                }
            }
            else
            {
                isLocalPlayer = false;
            }

            if (this.beams != null && this.IsFiring != this.beams.activeInHierarchy)
            {
                this.beams.SetActive(this.IsFiring);
            }

            // if (isPicked == false)
            // {
            //     this.nitroItem = 0;

            // }
            if (isRotated == true)
            {
                rotateBanana();
            }

        }

        /// <summary>
        /// MonoBehaviour method called when the Collider 'other' enters the trigger.
        /// Affect Health of the Player if the collider is a beam
        /// Note: when jumping and firing at the same, you'll find that the player's own beam intersects with itself
        /// One could move the collider further away to prevent this or check if the beam belongs to the player.
        /// </summary>
        public void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }


            // We are only interested in Beamers
            // we should be using tags but for the sake of distribution, let's simply check by name.
            if (!other.name.Contains("Beam"))
            {
                return;
            }

            this.Health -= 0.1f;
        }

        /// <summary>
        /// MonoBehaviour method called once per frame for every Collider 'other' that is touching the trigger.
        /// We're going to affect health while the beams are interesting the player
        /// </summary>
        /// <param name="other">Other.</param>
        public void OnTriggerStay(Collider other)
        {

            // we dont' do anything if we are not the local player.
            if (!photonView.IsMine)
            {
                return;
            }

            // check colider with nitro items
            if (photonView.IsMine)
            {
                if (other.gameObject.name == "Donut")
                {
                    // this.isPicked = true;
                    this.nitroItem = 0.2f;
                    // if (this.isPicked == true)
                    // {
                        StartCoroutine(waitForAddingNitro());
                    // }

                }
                // else
                // {
                //     this.nitroItem = 0;
                // }
                if (other.gameObject.name == "Banana Peel(Clone)")
                {
                    isRotated = true;
                }
            }


            // We are only interested in Beamers
            // we should be using tags but for the sake of distribution, let's simply check by name.
            if (!other.name.Contains("Beam"))
            {
                return;
            }

            // we slowly affect health when beam is constantly hitting us, so player has to move to prevent death.
            this.Health -= 0.1f * Time.deltaTime;
        }

        public IEnumerator waitForAddingNitro()
        {
            yield return new WaitForSeconds(0.05f);
            // this.isPicked = false;
        }
        public IEnumerator waitForRotateBanana()
        {
            yield return new WaitForSeconds(2.0f);
            this.KartObj.transform.localRotation = Quaternion.identity;
            this.KartObj.transform.GetChild(1).gameObject.transform.localRotation = Quaternion.identity;
            Debug.Log("ROTATED" + this.KartObj.transform.GetChild(1).gameObject);
            this.isRotated = false;
        }
        public void RandomSkinKart()
        {
            if (materialList.Length > 0)
            {
                index = Random.Range(0, materialList.Length - 1);
                this.skinMaterialBody.GetComponent<SkinnedMeshRenderer>().material = materialList[index];
                this.skinMaterialKart.GetComponent<SkinnedMeshRenderer>().material = materialList[index];
                Debug.Log("index: " + index);
            }
        }

        void rotateBanana()
        {
            KartObj.transform.Rotate(Vector3.up, Time.deltaTime * lerpTime);
            StartCoroutine(waitForRotateBanana());
        }




#if !UNITY_5_4_OR_NEWER
        /// <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4.</summary>
        void OnLevelWasLoaded(int level)
        {
            this.CalledOnLevelWasLoaded(level);
        }
#endif


        /// <summary>
        /// MonoBehaviour method called after a new level of index 'level' was loaded.
        /// We recreate the Player UI because it was destroy when we switched level.
        /// Also reposition the player if outside the current arena.
        /// </summary>
        /// <param name="level">Level index loaded</param>
        void CalledOnLevelWasLoaded(int level)
        {
            // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
            if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                transform.position = new Vector3(0f, 5f, 0f);
            }

            //GameObject _uiGo = Instantiate(this.playerUiPrefab);
            //_uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);



            if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                transform.position = new Vector3(0f, 5f, 0f);
            }


        }

        #endregion

        #region Private Methods


#if UNITY_5_4_OR_NEWER
        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
        {
            this.CalledOnLevelWasLoaded(scene.buildIndex);
        }
#endif

        /// <summary>
        /// Processes the inputs. This MUST ONLY BE USED when the player has authority over this Networked GameObject (photonView.isMine == true)
        /// </summary>
        void ProcessInputs()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                // we don't want to fire when we interact with UI buttons for example. IsPointerOverGameObject really means IsPointerOver*UI*GameObject
                // notice we don't use on on GetbuttonUp() few lines down, because one can mouse down, move over a UI element and release, which would lead to not lower the isFiring Flag.
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //	return;
                }

                if (!this.IsFiring)
                {
                    this.IsFiring = true;
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                if (this.IsFiring)
                {
                    this.IsFiring = false;
                }
            }
        }

        #endregion

        #region IPunObservable implementation

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(this.IsFiring);
                stream.SendNext(this.Health);
            }
            else
            {
                // Network player, receive data
                this.IsFiring = (bool)stream.ReceiveNext();
                this.Health = (float)stream.ReceiveNext();
            }
        }

        #endregion
    }
}