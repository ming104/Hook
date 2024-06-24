using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI achievementTitleText;
    public TextMeshProUGUI achievementDescriptionText;

    public TextMeshProUGUI rewardStar;
    public Toggle isLockedToggle;

    //private DataManager data;
    // Start is called before the first frame update
    void Start()
    {
        //isLockedToggle.isOn = false;
        //GetComponent<Button>().interactable = false;
        //ResetAchievementList();
    }

    public void ResetAchievementList()
    {
        var data = DataManager.Instance.LoadAchievements();

        achievementTitleText.text = data.achievements[id].name;
        achievementDescriptionText.text = data.achievements[id].description;
        rewardStar.text = $"{data.achievements[id].reward}";
        
        //isLockedToggle.isOn = false;
        //GetComponent<Button>().interactable = false;
        if (data.achievements[id].isUnlocked == false) 
        {
            isLockedToggle.isOn = false;
            GetComponent<Button>().interactable = false;
        }
        else
        {
            if (data.achievements[id].isRewardCollected == false)
            {
                isLockedToggle.isOn = false;
                GetComponent<Button>().interactable = true;
            }
            else
            {
                isLockedToggle.isOn = true;
                GetComponent<Button>().interactable = false;
            }
        }
    }

    public void AchievementButtonClick()
    {
        DataManager.Instance.ChangeLocked(id, true, true);
        isLockedToggle.isOn = true;
        GameManager.Instance.currentStar += DataManager.Instance.LoadAchievements().achievements[id].reward;
        DataManager.Instance.GameDataSave(GameManager.Instance.currentStar, DataManager.Instance.GameDataLoad().currentScore);
        GetComponent<Button>().interactable = false;
    }
}
