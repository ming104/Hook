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

    public Toggle isLockedToggle;

    //private DataManager data;
    // Start is called before the first frame update
    void Start()
    {
        var data = DataManager.Instance.LoadAchievements();

        achievementTitleText.text = data.achievements[id].name;
        achievementDescriptionText.text = data.achievements[id].description;

        isLockedToggle.isOn = data.achievements[id].isUnlocked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
