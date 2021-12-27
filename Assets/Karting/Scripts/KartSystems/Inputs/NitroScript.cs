using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KartGame.KartSystems;
using UnityEngine.EventSystems;
using Photon.Pun.Demo.PunBasics;
//using Photon.Pun;





    public class NitroScript : MonoBehaviour
	{

		public static NitroScript instance;

		#region Private Fields

		[Tooltip("Pixel offset from the player target")]
		[SerializeField]
		private Vector3 screenOffset = new Vector3(0f, 30f, 0f);

		[Tooltip("UI Text to display Player's Nitro")]
		[SerializeField]
		private Text NitroTxt;

		[Tooltip("UI Slider to display Player's Nitro")]
		[SerializeField]
		public Slider playerNitroSlider;

		public GameObject fillNitroBar;

		public Sprite[] nitroIcon;
		public GameObject NitroStatus;

		float characterControllerHeight;

		public List<GameObject> childrenObj;

		Transform targetTransform;

		Renderer targetRenderer;

		CanvasGroup _canvasGroup;

		Vector3 targetPosition;

		#endregion

		#region MonoBehaviour Messages

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during early initialization phase
		/// </summary>
		///

		void Awake()
		{

		fillNitroBar.GetComponent<Image>().gameObject.SetActive(false);
			_canvasGroup = this.GetComponent<CanvasGroup>();

			this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
		if(instance == null)
        {
			instance = GetComponent<NitroScript>();
        }

		if (playerNitroSlider != null)
		{
			playerNitroSlider.value = 0f;
		}
	}

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity on every frame.
		/// update the health slider to reflect the Player's health
		/// </summary>
		void Update()
		{
			// Destroy itself if the target is null, It's a fail safe when Photon is destroying Instances of a Player over the network
			if (playerNitroSlider == null)
			{
				Destroy(this.gameObject);
				return;
			}

			// update nitro icon
			if(playerNitroSlider.value < 1f && playerNitroSlider.value >= 0f)
			{
				NitroStatus.GetComponent<Image>().sprite = nitroIcon[0];
			}
			else
			{
				NitroStatus.GetComponent<Image>().sprite = nitroIcon[1];
			}

			// reset speed after out of nitro
			if(playerNitroSlider.value == 0)
			{
				ArcadeKart.Instance.baseStats.TopSpeed = 20;
			}

			// rotate nitro items
			//setAnimationForItems();
		}
		

		// update nitro bar
		public void BootNitroProcessBar()
		{
			
			if (playerNitroSlider != null)
			{
			// if take a nitro item then process bar will be add (0.2)

			if (playerNitroSlider.value <= 1f && playerNitroSlider.value >= 0f)
			{
				if(Photon.Pun.Demo.PunBasics.PlayerManager.instance.isLocalPlayer == true)
                {
					//playerNitroSlider.value += 0.2f;
					if(Photon.Pun.Demo.PunBasics.PlayerManager.instance.isPicked == true)
                    {
						fillNitroBar.GetComponent<Image>().gameObject.SetActive(true);
						playerNitroSlider.value += Photon.Pun.Demo.PunBasics.PlayerManager.instance.nitroItem;
                    }
                    else
                    {
						playerNitroSlider.value += 0;

					}

				}
			}

				if (!KeyboardInput.instance.isBootNitro && playerNitroSlider.value >= 1f)
				{
					AfterBootNitro();
				}
				else
				{
					Debug.Log("not boot nitro");
				}

			}

	}

	public void UpdateSpeed()
    {
		//PhotonView gameObj = gameObject.GetComponent<PhotonView>();
		//if (gameObj != null)
		//{
		//	//Destroy component that enables the player to move if it's not the instance you are controlling, preventing moving all the players on you local instance
		//	//at the same time.
		//	if (!gameObj.IsMine)
		//	{
		//		gameObject.GetComponent<ArcadeKart>();
		//	}
		//}
		//ArcadeKart.Instance.baseStats.TopSpeed += 50;
	}

		

		// decrease nitro process bar after booting
		public void AfterBootNitro()
		{
		//do
		//{
		//	Debug.Log("decrease nitro");
		//	playerNitroSlider.value -= 0.1f;
		//} while (playerNitroSlider.value > 0f);

		if (playerNitroSlider.value <= 1f && playerNitroSlider.value >= 0f)
		{
            if (KeyboardInput.instance.resetNitro &&  Photon.Pun.Demo.PunBasics.PlayerManager.instance.isLocalPlayer == true)
            {
				playerNitroSlider.value -= 0.2f * Time.deltaTime;
				if (playerNitroSlider.value == 0f )
				{
					KeyboardInput.instance.resetNitro = false;
					KeyboardInput.instance.NitroVFX.gameObject.SetActive(false);
					foreach (GameObject driff in KeyboardInput.instance.DriffVFX)
					{
						driff.gameObject.SetActive(false);
					}
					fillNitroBar.GetComponent<Image>().gameObject.SetActive(false);
				}
			}
			
            Debug.Log("decrease nitro");
        }
	}

    void LateUpdate()
    {

        // Do not show the UI if we are not visible to the camera, thus avoid potential bugs with seeing the UI, but not the player itself.
        if (targetRenderer != null)
        {
            this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
        }

        // #Critical
        // Follow the Target GameObject on screen.
        if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
            targetPosition.y += characterControllerHeight;
            if (Camera.main != null)
            {
                this.transform.position = Camera.main.WorldToScreenPoint(targetPosition);
            }
            else
            {
                Debug.LogError("Camera main is null");
            }
        }
    }




    #endregion


	//public void setAnimationForItems()
 //   {
	//	foreach(Transform child in transform)
 //       {
	//		if(child.gameObject.name == "Checkpoint")
 //           {
	//			childrenObj.Add(child.gameObject);
	//			if(childrenObj != null && childrenObj.Count > 0)
 //               {
	//				child.transform.Rotate(new Vector3(0f, 0f, 100f) * Time.deltaTime);
	//				Debug.Log("rotate items !!!!!!!!!");
	//			}
 //           }
 //       }
 //   }

    #region Public Methods

    public void SetNitro()
		{

		}

		#endregion
	}	

