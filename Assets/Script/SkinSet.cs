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
    
    public Sprite skinSprite;
    public Color32 inputColor;

    void Update() // 뽑기시스템 + 가지고있는거 켜지고 버튼 인터렉트도 키고 + 뽑기하면 초기화까지 해야겠따.
    {
        //ResetSetting();
    }

    public void UISkinUpdate(int type)
    {
        var SampleBallImage = UIManager.Instance.sampleBall.GetComponent<Image>();
        switch (type)
        {
            case 0:
                if (DataManager.Instance.SkinDataLoad().ballSkin.Contains(skinObject.skinName))
                {
                    SampleBallImage.sprite = skinSprite;
                    SampleBallImage.color = new Color32(inputColor.r, inputColor.g, inputColor.b, 255);
                }
                break;
            case 1:
                if (DataManager.Instance.SkinDataLoad().lineSkin.Contains(skinObject.skinName))
                {
                    SampleBallImage.sprite = skinSprite;
                    SampleBallImage.color = new Color32(inputColor.r, inputColor.g, inputColor.b, 255);
                }
                break;
            case 2:
                if (DataManager.Instance.SkinDataLoad().hookSkin.Contains(skinObject.skinName))
                {
                    SampleBallImage.sprite = skinSprite;
                    SampleBallImage.color = new Color32(inputColor.r, inputColor.g, inputColor.b, 255);
                }
                break;
            case 3:
                if (DataManager.Instance.SkinDataLoad().trailSkin.Contains(skinObject.skinName))
                {
                    SampleBallImage.sprite = skinSprite;
                    SampleBallImage.color = new Color32(inputColor.r, inputColor.g, inputColor.b, 255);
                }
                break;
        }
    }

    public void SkinUpdate()
    {
        if (DataManager.Instance.SkinDataLoad().ballSkin.Contains(skinObject.skinName))
        {
            GameManager.Instance.player.GetComponent<SpriteRenderer>().sprite = skinSprite;
            GameManager.Instance.player.GetComponent<SpriteRenderer>().color = new Color32(inputColor.r, inputColor.g, inputColor.b, 255);
        }
        if (DataManager.Instance.SkinDataLoad().lineSkin.Contains(skinObject.skinName))
        {
            var gradientLine = new Gradient();

            // Blend color from red at 0% to blue at 100%
            var colorsLine = new GradientColorKey[1];
            colorsLine[0] = new GradientColorKey(new Color32(inputColor.r, inputColor.g, inputColor.b, 255), 0.0f);

            // Blend alpha from opaque at 0% to transparent at 100%
            var alphasLine = new GradientAlphaKey[1];
            alphasLine[0] = new GradientAlphaKey(1.0f, 0.0f);

            gradientLine.SetKeys(colorsLine, alphasLine);
            GameManager.Instance.line.GetComponent<LineRenderer>().colorGradient = gradientLine;
        }
        if (DataManager.Instance.SkinDataLoad().hookSkin.Contains(skinObject.skinName))
        {
            GameManager.Instance.hook.GetComponent<SpriteRenderer>().sprite = skinSprite;
            GameManager.Instance.hook.GetComponent<SpriteRenderer>().color = new Color32(inputColor.r, inputColor.g, inputColor.b, 255);
        }
        if (DataManager.Instance.SkinDataLoad().trailSkin.Contains(skinObject.skinName))
        {
            var gradientTrail = new Gradient();

            // Blend color from red at 0% to blue at 100%
            var colorsTrail = new GradientColorKey[2];
            colorsTrail[0] = new GradientColorKey(new Color32(inputColor.r, inputColor.g, inputColor.b, 255), 0.0f);
            colorsTrail[1] = new GradientColorKey(Color.white, 1.0f);

            // Blend alpha from opaque at 0% to transparent at 100%
            var alphasTrail = new GradientAlphaKey[1];
            alphasTrail[0] = new GradientAlphaKey(1.0f, 0.0f);

            gradientTrail.SetKeys(colorsTrail, alphasTrail);
            GameManager.Instance.player.GetComponent<TrailRenderer>().colorGradient = gradientTrail;
        }
    }

    public void ResetSetting()
    {
        DataManager dataManager = DataManager.Instance;
        int skinIndex = UIManager.Instance.skinIndex;
        
        for (int i = 0; i < 3; i++)
        {
            if (GetTargetList(dataManager.SkinDataLoad(),skinIndex,i).Contains(skinObject.skinName))
            {
                skinObject.skinLocker.SetActive(false);
                GetComponent<Button>().interactable = true;
            }
        }
    }
    private List<string> GetTargetList(SkinData skinData, int type, int rarity)
    {
        SkinList targetSkinList = type switch
        {
            0 => skinData.ball,
            1 => skinData.line,
            2 => skinData.hook,
            3 => skinData.trail,
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Invalid type")
        };

        return rarity switch
        {
            0 => targetSkinList.common,
            1 => targetSkinList.rare,
            2 => targetSkinList.epic,
            _ => throw new ArgumentOutOfRangeException(nameof(rarity), "Invalid rarity")
        };
    }

    public void SkinSelect(int type)
    {
        var skinPreview = UIManager.Instance.sampleBall.GetComponent<Image>();
        skinPreview.sprite = this.skinSprite;
        skinPreview.color = new Color32(inputColor.r, inputColor.g, inputColor.b, 255);

        switch (type)
        {
            case 0 : // ball
                GameManager.Instance.player.GetComponent<SpriteRenderer>().sprite = skinSprite;
                GameManager.Instance.player.GetComponent<SpriteRenderer>().color = new Color32(inputColor.r, inputColor.g, inputColor.b, 255);
                DataManager.Instance.SkinDataSave(0, skinObject.skinName);
                break;
            
            case 1 : // line
                var gradientLine = new Gradient();

                // Blend color from red at 0% to blue at 100%
                var colorsLine = new GradientColorKey[1];
                colorsLine[0] = new GradientColorKey(new Color32(inputColor.r, inputColor.g, inputColor.b, 255), 0.0f);

                // Blend alpha from opaque at 0% to transparent at 100%
                var alphasLine = new GradientAlphaKey[1];
                alphasLine[0] = new GradientAlphaKey(1.0f, 0.0f);

                gradientLine.SetKeys(colorsLine, alphasLine);
                GameManager.Instance.line.GetComponent<LineRenderer>().colorGradient = gradientLine;
                
                DataManager.Instance.SkinDataSave(1, skinObject.skinName);
                break;
            
            case 2 : // hook
                GameManager.Instance.hook.GetComponent<SpriteRenderer>().sprite = skinSprite;
                GameManager.Instance.hook.GetComponent<SpriteRenderer>().color = new Color32(inputColor.r, inputColor.g, inputColor.b, 255);
                DataManager.Instance.SkinDataSave(2, skinObject.skinName);
                break;
            
            case 3 : // trail
                var gradientTrail = new Gradient();

                // Blend color from red at 0% to blue at 100%
                var colorsTrail = new GradientColorKey[2];
                colorsTrail[0] = new GradientColorKey(new Color32(inputColor.r, inputColor.g, inputColor.b, 255), 0.0f);
                colorsTrail[1] = new GradientColorKey(Color.white, 1.0f);

                // Blend alpha from opaque at 0% to transparent at 100%
                var alphasTrail = new GradientAlphaKey[1];
                alphasTrail[0] = new GradientAlphaKey(1.0f, 0.0f);

                gradientTrail.SetKeys(colorsTrail, alphasTrail);
                GameManager.Instance.player.GetComponent<TrailRenderer>().colorGradient = gradientTrail;
                
                DataManager.Instance.SkinDataSave(3, skinObject.skinName);
                break;
        }
        
    }
}
