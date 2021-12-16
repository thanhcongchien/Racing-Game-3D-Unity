﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
    [SerializeField] Color backgroundActiveColor;
    [SerializeField] Color handleActiveColor;
    [SerializeField] Image backgroundColor;
    [SerializeField] Sprite[] ButtonImg;

    Image backgroundImage, handleImage;

    Color backgroundDefaultColor, handleDefaultColor;

    Toggle toggle;

    Vector2 handlePosition;

    void Awake()
    {
        toggle = GetComponent<Toggle>();

        handlePosition = uiHandleRectTransform.anchoredPosition;

        backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();
        handleImage = uiHandleRectTransform.GetComponent<Image>();

        backgroundDefaultColor = backgroundImage.color;
        handleDefaultColor = handleImage.color;

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
            OnSwitch(true);
    }

    void OnSwitch(bool on)
    {
        uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition; // no anim
        //uiHandleRectTransform.DOAnchorPos(on ? handlePosition * -1 : handlePosition, .4f).SetEase(Ease.InOutBack);

        //backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor ; // no anim
        //backgroundImage.DOColor(on ? backgroundActiveColor : backgroundDefaultColor, .6f);

        //handleImage.color = on ? handleActiveColor : handleDefaultColor ; // no anim
        //handleImage.DOColor(on ? handleActiveColor : handleDefaultColor, .4f);
        if (toggle.isOn)
        {
            backgroundColor.GetComponent<Image>().sprite = MusicClass.Instance.ButtonImg[1];
            handleImage.GetComponent<Image>().sprite = MusicClass.Instance.ButtonImg[3];
            handleImage.GetComponent<Image>().color = new Color(0.2352941176470588f, 0.6823529411764706f, 0.2352941176470588f);
            MusicClass.Instance.PlayMusic();

        }
        else
        {
            backgroundColor.GetComponent<Image>().sprite = MusicClass.Instance.ButtonImg[0];
            handleImage.GetComponent<Image>().sprite = MusicClass.Instance.ButtonImg[2];
            handleImage.GetComponent<Image>().color = new Color(0.9137254901960784f, 0.1098039215686275f, 0.1372549019607843f);
            MusicClass.Instance.StopMusic();
        }
    }

    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
