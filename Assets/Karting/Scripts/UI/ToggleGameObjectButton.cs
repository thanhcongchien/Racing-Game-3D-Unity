using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleGameObjectButton : MonoBehaviour
{
    public GameObject objectToToggle;
    public GameObject mainScenekart;
    public bool resetSelectionAfterClick;

    void Update()
    {
        if (objectToToggle.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel))
        {
            SetGameObjectActive(false);
        }
    }

    public void SetGameObjectActive(bool active)
    {
        objectToToggle.SetActive(active);
        
        if(active){
            if (mainScenekart != null)
            mainScenekart.SetActive(!active);

        if (resetSelectionAfterClick)
            SoundManager.Instance.PlaySFX(SoundManager.CLICK_SFX);
            EventSystem.current.SetSelectedGameObject(null);
        }else{
            SoundManager.Instance.PlaySFX(SoundManager.CLOSE_SFX);
        }
        
    }
}
