using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class SkinObject
{
    public string skinName;
    public GameObject skinContent;
    public GameObject skinImage;
    public GameObject skinLocker;
}

public class SkinSet : MonoBehaviour
{
    public SkinObject skinObject;
    
    public Sprite sprite;
    public Color32 inputColor;

    void Start() // 뽑기시스템 + 가지고있는거 켜지고 버튼 인터렉트도 키고 + 뽑기하면 초기화까지 해야겠따.
    {
        ResetSetting();
    }

    public void ResetSetting()
    {
        DataManager dataManager = DataManager.Instance;
        int skinIndex = UIManager.Instance.skinIndex;
        switch (skinIndex)
        {
            case 0: // ball
                if (dataManager.SkinDataLoad().ball.common.Contains(skinObject.skinName))
                {
                    skinObject.skinLocker.SetActive(false);
                    skinObject.skinContent.GetComponent<Button>().interactable = true;
                }

                if (dataManager.SkinDataLoad().ball.rare.Contains(skinObject.skinName))
                {
                    skinObject.skinLocker.SetActive(false);
                    skinObject.skinContent.GetComponent<Button>().interactable = true;
                }

                if (dataManager.SkinDataLoad().ball.epic.Contains(skinObject.skinName))
                {
                    skinObject.skinLocker.SetActive(false);
                    skinObject.skinContent.GetComponent<Button>().interactable = true;
                }
                break;
            case 1: // Line
                if (dataManager.SkinDataLoad().line.common.Contains(skinObject.skinName))
                {
                    skinObject.skinLocker.SetActive(false);
                    skinObject.skinContent.GetComponent<Button>().interactable = true;
                }

                if (dataManager.SkinDataLoad().line.rare.Contains(skinObject.skinName))
                {
                    skinObject.skinLocker.SetActive(false);
                    skinObject.skinContent.GetComponent<Button>().interactable = true;
                }

                if (dataManager.SkinDataLoad().line.epic.Contains(skinObject.skinName))
                {
                    skinObject.skinLocker.SetActive(false);
                    skinObject.skinContent.GetComponent<Button>().interactable = true;
                }
                break;
            case 2: // Hook
                if (dataManager.SkinDataLoad().hook.common.Contains(skinObject.skinName))
                {
                    skinObject.skinLocker.SetActive(false);
                    skinObject.skinContent.GetComponent<Button>().interactable = true;
                }

                if (dataManager.SkinDataLoad().hook.rare.Contains(skinObject.skinName))
                {
                    skinObject.skinLocker.SetActive(false);
                    skinObject.skinContent.GetComponent<Button>().interactable = true;
                }

                if (dataManager.SkinDataLoad().hook.epic.Contains(skinObject.skinName))
                {
                    skinObject.skinLocker.SetActive(false);
                    skinObject.skinContent.GetComponent<Button>().interactable = true;
                }
                break;
            case 3: // Trail
                if (dataManager.SkinDataLoad().trail.common.Contains(skinObject.skinName))
                {
                    skinObject.skinLocker.SetActive(false);
                    skinObject.skinContent.GetComponent<Button>().interactable = true;
                }

                if (dataManager.SkinDataLoad().trail.rare.Contains(skinObject.skinName))
                {
                    skinObject.skinLocker.SetActive(false);
                    skinObject.skinContent.GetComponent<Button>().interactable = true;
                }

                if (dataManager.SkinDataLoad().trail.epic.Contains(skinObject.skinName))
                {
                    skinObject.skinLocker.SetActive(false);
                    skinObject.skinContent.GetComponent<Button>().interactable = true;
                }
                break;
                
        }
        
    }
}
