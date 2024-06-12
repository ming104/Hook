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
    
    // Start is called before the first frame update
    void Start()
    {
        StartMainUIActive(true);
        scoreText.gameObject.SetActive(false);
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


    public void ScoreTextActive(bool isActive)
    {
        scoreText.gameObject.SetActive(isActive);
    }
}
