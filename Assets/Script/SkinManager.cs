using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;



public class SkinManager : MonoBehaviour
{
    #region Singleton
    private static SkinManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }
    public static SkinManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion

    public List<GameObject> skinContent;

    public SkinSet[] skinSetting;
    
    // Start is called before the first frame update
    void Start()
    {
        skinSetting = skinContent[UIManager.Instance.skinIndex].GetComponentsInChildren<SkinSet>();
        
        foreach (SkinSet skinset in skinSetting)
        {
            skinset.SkinUpdate();
        }
    }

    // Update is called once per frame
    // void Update()
    // {
    //     UnlockSkin();
    // }

    public void SkinSelected(int type)
    {
        skinSetting = skinContent[UIManager.Instance.skinIndex].GetComponentsInChildren<SkinSet>();
        foreach (SkinSet skinset in skinSetting)
        {
            skinset.UISkinUpdate(type);
        }
    }

    public void UnlockSkin()
    {
        SkinSet[] skinsetting = skinContent[UIManager.Instance.skinIndex].GetComponentsInChildren<SkinSet>();
        foreach (SkinSet skinset in skinsetting)
        {
            skinset.GetComponent<Button>().interactable = false;
            skinset.ResetSetting();
        }
    }

    public void SkinRandomPick()
    {
        if (GameManager.Instance.currentStar >= 500)
        {
            GameManager.Instance.PickAchievement();
            GameManager.Instance.currentStar -= 500;
            int randomNumber = Random.Range(1, 70); // 시험 common만
            if (randomNumber is > 0 and <= 70)
            {
                Debug.Log("Common");
                List<string> GetingSkin = DataManager.Instance.FullSkinDataLoad(UIManager.Instance.skinIndex, 0);
                int rand = Random.Range(0, GetingSkin.Count);
                DataManager.Instance.SkinDataSave(UIManager.Instance.skinIndex, 0, GetingSkin[rand]);
                UnlockSkin();
            }
            else if (randomNumber is > 70 and <= 90)
            {
                Debug.Log("rare");
                List<string> GetingSkin = DataManager.Instance.FullSkinDataLoad(UIManager.Instance.skinIndex, 1);
                int rand = Random.Range(0, GetingSkin.Count);
                DataManager.Instance.SkinDataSave(UIManager.Instance.skinIndex, 1, GetingSkin[rand]);
                UnlockSkin();
            }
            else if (randomNumber is > 90 and <= 100)
            {
                Debug.Log("epic");
                List<string> GetingSkin = DataManager.Instance.FullSkinDataLoad(UIManager.Instance.skinIndex, 2);
                int rand = Random.Range(0, GetingSkin.Count);
                DataManager.Instance.SkinDataSave(UIManager.Instance.skinIndex, 2, GetingSkin[rand]);
                UnlockSkin();
            }
            DataManager.Instance.GameDataSave(GameManager.Instance.currentStar, DataManager.Instance.GameDataLoad().currentScore);
        }
    }
}
