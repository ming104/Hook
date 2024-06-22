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

    public TextMeshPro tapToGo;
    
    //SkinSetting------------
    public TextMeshProUGUI selectText; // ball, Line, Hook 변경되는 텍스트
    public TextMeshProUGUI rarityText; // 희귀도 텍스트
    public TextMeshProUGUI newSkinText;
    public GameObject sampleBall; // 현재 장착하고 있는 ball 프리뷰
    public GameObject[] skinSelectMenu;
    public int skinIndex;

    public SkinManager SkinInstance;
    // Start is called before the first frame update
    void Start()
    {
        StartMainUIActive(true);
        SkinUIActive(false);
        ScoreTextActive(false);
        SelectedBall();
        ScoreSet();
        
        SkinInstance = SkinManager.Instance;
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
        tapToGo.gameObject.SetActive(true);
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
        skinIndex = 0;
        selectText.text = "Ball";
        newSkinText.text = "Buy new Ball Skin";
        SkinSelectedUI(0);
        SkinInstance.SkinSelected(0);
    }
    public void SelectedLine()
    {
        skinIndex = 1;
        selectText.text = "Rope";
        newSkinText.text = "Buy new Rope Skin";
        SkinSelectedUI(1);
        SkinInstance.SkinSelected(1);
    }
    public void SelectedHook()
    {
        skinIndex = 2;
        selectText.text = "Hook";
        newSkinText.text = "Buy new Hook Skin";
        SkinSelectedUI(2);
        SkinInstance.SkinSelected(2);
    }
    public void SelectedTrail()
    {
        skinIndex = 3;
        selectText.text = "Trail";
        newSkinText.text = "Buy new Trail Skin";
        SkinSelectedUI(3);
        SkinInstance.SkinSelected(3);
    }

    public void SkinSelectedUI(int count)
    {
        for (int i = 0; i < skinSelectMenu.Length; i++)
        {
            skinSelectMenu[i].SetActive(false);
        }
       skinSelectMenu[count].SetActive(true);
       SkinInstance.UnlockSkin();
    }
    //==========
}
