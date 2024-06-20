using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }
    public static UIManager Instance
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

    public TextMeshProUGUI mainScore;
    public TextMeshProUGUI mainBestScore;
    
    public TextMeshProUGUI starCountText;
    public TextMeshProUGUI scoreText;
    public GameObject startMainUI;

    public GameObject SkinUI;
    
    //SkinSetting------------
    public TextMeshProUGUI selectText; // ball, Line, Hook 변경되는 텍스트
    public TextMeshProUGUI rarityText; // 희귀도 텍스트
    public TextMeshProUGUI newSkinText;
    public GameObject sampleBall; // 현재 장착하고 있는 ball 프리뷰
    public GameObject[] SkinSelectMenu;
    public int skinIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        StartMainUIActive(true);
        SkinUIActive(false);
        ScoreTextActive(false);
        SelectedBall();
        ScoreSet();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"{GameManager.Instance.Score}";
        starCountText.text = $"{GameManager.Instance.currentStar}";
    }
    
    public void GameStartButton()
    {
        GameManager.Instance.IsGameStartChangeValue(true);
        ScoreTextActive(true);
        StartMainUIActive(false);
    }

    public void StartMainUIActive(bool isActive)
    {
        startMainUI.SetActive(isActive);
    }

    public void ScoreSet()
    {
        var gameData = DataManager.Instance.GameDataLoad();
        mainScore.text = $"{gameData.currentScore}";
        mainBestScore.text = $"{gameData.bestScore}";
    }


    public void ScoreTextActive(bool isActive)
    {
        scoreText.gameObject.SetActive(isActive);
    }

    public void SkinUIActive(bool isActive)
    {
        SkinUI.SetActive(isActive);
    }
    
    public void SkinButton()
    {
        StartMainUIActive(false);
        SkinUIActive(true);
    }

    public void SkinButtonOut()
    {
        StartMainUIActive(true);
        SkinUIActive(false);
        SelectedBall();
    }
    
    // SkinSetting
    public void SelectedBall()
    {
        selectText.text = "Ball";
        newSkinText.text = "Buy new Ball Skin";
        SkinSelectedUI(0);
        skinIndex = 0;
        SkinManager.Instance.UnlockSkin();
    }
    public void SelectedLine()
    {
        selectText.text = "Rope";
        newSkinText.text = "Buy new Rope Skin";
        SkinSelectedUI(1);
        skinIndex = 1;
        SkinManager.Instance.UnlockSkin();
    }
    public void SelectedHook()
    {
        selectText.text = "Hook";
        newSkinText.text = "Buy new Hook Skin";
        SkinSelectedUI(2);
        skinIndex = 2;
        SkinManager.Instance.UnlockSkin();
    }
    public void SelectedTrail()
    {
        selectText.text = "Trail";
        newSkinText.text = "Buy new Trail Skin";
        SkinSelectedUI(3);
        skinIndex = 3;
        SkinManager.Instance.UnlockSkin();
    }

    public void SkinSelectedUI(int count)
    {
        for (int i = 0; i < SkinSelectMenu.Length; i++)
        {
            SkinSelectMenu[i].SetActive(false);
        }
        SkinSelectMenu[count].SetActive(true);
    }
    //==========
}
