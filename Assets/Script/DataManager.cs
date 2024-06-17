using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.U2D.Animation;
using UnityEngine;

[Serializable]
public class GameData
{
    public int star;
    public int currentScore;
    public int bestScore;
}
[Serializable]
public class BallSkinData
{
    public string ballSkinName;
}
[Serializable]
public class LineSkinData
{
    public string lineSkinName;
}
[Serializable]
public class HookSkinData
{
    public string hookSkinName ;
}

[Serializable]
public class SkinData
{
    public List<BallSkinData> ballSkin;
    public List<LineSkinData> lineSkin;
    public List<HookSkinData> hookSkin;
}

public class DataManager : MonoBehaviour
{
    #region Singleton
    private static DataManager instance = null;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("DataManager instance is null!");
                return null;
            }
            return instance;
        }
    }
    #endregion

    #region GameData
    public void GameDataSave(int currentStarCount, int currentScore)
    {
        string filePath = Application.persistentDataPath + "/GameData.json";
        GameData gameData = null; // 기본값으로 null 할당

        // 파일 존재 여부 확인
        if (File.Exists(filePath))
        {
            try
            {
                string jsonData = File.ReadAllText(filePath); // 파일에서 json 문자열을 읽음
                gameData = JsonUtility.FromJson<GameData>(jsonData); // json 문자열을 GameData으로 변환
            }
            catch (Exception e)
            {
                Debug.LogError("Error reading game data file: " + e.Message);
            }
        }

        // 파일이 없거나 읽기 실패 시 기본 GameData생성
        if (gameData == null)
        {
            Debug.LogWarning("파일이 없거나 읽을 수 없음 gamedata 생성함");
            gameData = new GameData();
            gameData.star = 0; // 기본값 지정
            gameData.currentScore = 0;
            gameData.bestScore = 0;
        }

        // GameManager.Instance의 null 확인
        if (GameManager.Instance != null)
        {
            gameData.star = currentStarCount; // 현재 star 값을 업데이트
            gameData.currentScore = currentScore;
            if (currentScore > GameDataLoad().bestScore)
            {
                gameData.bestScore = currentScore;
            }
        }
        else
        {
            Debug.LogError("게임매니저가 없음!");
            return;
        }

        // 업데이트된 GameData를 json으로 변환
        string changeGameData = JsonUtility.ToJson(gameData, true);

        // JSON 문자열을 파일에 씀
        try
        {
            File.WriteAllText(filePath, changeGameData);
        }
        catch (Exception e)
        {
            Debug.LogError("Error writing game data file: " + e.Message);
        }
    }

    
    public GameData GameDataLoad()
    {
        string filePath = Application.persistentDataPath + "/GameData.json";
        if (File.Exists(filePath))
        {
            try
            {
                string jsonData = File.ReadAllText(filePath); // 파일에서 json 문자열을 읽음
                GameData gameData = JsonUtility.FromJson<GameData>(jsonData); // json 문자열을 GameData 으로 변환
                return gameData;
            }
            catch (Exception e)
            {
                Debug.LogError("Error reading game data file: " + e.Message);
                return new GameData(); // 파일 읽기 실패 시 기본 GameData 반환
            }
        }
        else
        {
            Debug.LogWarning("파일을 찾을 수 없음. 새로 생성함");
        
            GameData gameData = new GameData(); // 새로운 GameData 생성
            gameData.star = 0; // 기본값 지정
            gameData.currentScore = 0;
            gameData.bestScore = 0;
            SaveGameDataToFile(gameData); // 기본 데이터를 파일에 저장
            return gameData; // 기본 GameData 객체 반환
        }
    }

    //기본 데이터를 파일에 저장하는 기능
    private void SaveGameDataToFile(GameData gameData)
    {
        string filePath = Application.persistentDataPath + "/GameData.json";
        try
        {
            string jsonData = JsonUtility.ToJson(gameData, true); // GameData 객체를 JSON 문자열로 변환
            File.WriteAllText(filePath, jsonData); // JSON 문자열을 파일에 씀
        }
        catch (Exception e)
        {
            Debug.LogError("Error writing game data file: " + e.Message);
        }
    }
    #endregion GameData

    #region SkinData

    public void SkinDataSave()
    {
        string filePath = Application.persistentDataPath + "/SkinData.json";
        SkinData skinData = null;
        
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath); // 파일에서 json 문자열을 읽음
            skinData = JsonUtility.FromJson<SkinData>(jsonData); // json 문자열을 GameData으로 변환
        }
        else
        {
            skinData = new SkinData();
            skinData.hookSkin = new List<HookSkinData>();
            skinData.lineSkin = new List<LineSkinData>();
            skinData.ballSkin = new List<BallSkinData>();
        }
        string changeSkinData = JsonUtility.ToJson(skinData, true);
        File.WriteAllText(filePath, changeSkinData);
    }

    public SkinData SkinDataLoad()
    {
        string filePath = Application.persistentDataPath + "/SkinData.json";
        if (File.Exists(filePath))
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                SkinData gameData = JsonUtility.FromJson<SkinData>(jsonData);
                return gameData;
            }
            catch (Exception e)
            {
                Debug.LogError("Error reading game data file: " + e.Message);
                return new SkinData();
            }
        }
        else
        {
            Debug.LogWarning("파일을 찾을 수 없음. 새로 생성함");
        
            SkinData skinData = new SkinData();
            skinData = new SkinData();
            skinData.hookSkin = new List<HookSkinData>();
            skinData.lineSkin = new List<LineSkinData>();
            skinData.ballSkin = new List<BallSkinData>();
            string jsonData = JsonUtility.ToJson(skinData, true); // GameData 객체를 JSON 문자열로 변환
            File.WriteAllText(filePath, jsonData); // JSON 문자열을 파일에 씀
            return skinData;
        }
    }

    #endregion
}
