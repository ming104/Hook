using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPanel : MonoBehaviour
{
    public GameObject achievementContent;

    public AchievementButton[] _achievements;
    // Start is called before the first frame update
    void Start()
    {
        //_achievements = achievementContent.GetComponentsInChildren<AchievementButton>();
    }

    private void OnEnable()
    {
        if (_achievements.Length == 0)
        {
            _achievements = achievementContent.GetComponentsInChildren<AchievementButton>();
            foreach (var achievement in _achievements)
            {
                achievement.ResetAchievementList();
            }
        }
        else
        {
            foreach (var achievement in _achievements)
            {
                achievement.ResetAchievementList();
            }
        }
    }
        
}
