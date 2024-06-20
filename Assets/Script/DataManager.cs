using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.U2D.Animation;
using UnityEngine;

// 게임 데이터 -----------------------
[Serializable]
public class GameData
{
    public int star;
    public int currentScore;
    public int bestScore;
}
//-----------------------------------
// 스킨 데이터-------------------------
[Serializable]
public class SkinList
{
    public List<string> common = new List<string>();
    public List<string> rare = new List<string>();
    public List<string> epic = new List<string>();
}

[Serializable]
public class SkinData
{
    public SkinList ball = new SkinList();
    public SkinList line = new SkinList();
    public SkinList hook = new SkinList();
    public SkinList trail = new SkinList();
    
    public string ballSkin = "White";
    public string lineSkin= "White";
    public string hookSkin= "White";
    public string trailSkin= "White";
}

[Serializable]
public class FullSkinData
{
    public SkinList ball = new SkinList();
    public SkinList line = new SkinList();
    public SkinList hook = new SkinList();
    public SkinList trail = new SkinList();
}
//------------------------------------

public class DataManager : MonoBehaviour
{
    #region Singleton
    private static DataManager instance = null;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        if (!File.Exists(Application.persistentDataPath + "/SkinData.json"))
        {
            SkinData skinData = new SkinData
            {
                ball = new SkinList { common = { "White" } },
                line = new SkinList { common = { "White" } },
                hook = new SkinList { common = { "White" } },
                trail = new SkinList { common = { "White" } }
            };
            string jsonData = JsonUtility.ToJson(skinData, true); // 수정했던 SkinData를 받아와서 Json파일로 만듦
            File.WriteAllText(Application.persistentDataPath + "/SkinData.json", jsonData);
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
// 스킨 데이터 저장
/// <summary>
/// 스킨을 저장하는 매우 간단한 코드였으면 좋겠는거
/// </summary>
/// <param name="type">여기다가 ball, line, hook, trail순으로 0123임</param>
/// <param name="rarity">여기다가 common, rare, epic순으로 012임</param>
/// <param name="skinName">뜬 스킨 이름을 적으면 되는 부분</param>
   public void SkinDataSave(int type, int rarity, string skinName) // type과 희귀도, 이름 받아옴
    {
        SkinData skinData = LoadSkinData(); // 기존 데이터를 받아와서 skinData에 넣음

        if (!SkinExists(skinData, type, rarity, skinName)) // 만약 이미 있는 스킨이 아닌 경우
        {
            AddSkin(skinData, type, rarity, skinName); // skinData와 타입 희귀도 이름으로 Add할지 확인함
            SaveSkinData(skinData); // 저장하는 곳
        }
        else // 이미 스킨이 있는 경우 (수정이 필요함)
        {
            Debug.LogWarning("Skin already exists: " + skinName);
            GameManager.Instance.currentStar += 50;
        }
    }

    private SkinData LoadSkinData() // 기존의 데이터를 받아오는 코드
    {
        string filePath = Application.persistentDataPath + "/SkinData.json"; // 좌표 지정
        if (File.Exists(filePath)) // 파일이 존재하는지 확인
        {
            try
            {
                string jsonData = File.ReadAllText(filePath); // 파일이 존재하니 파일의 값을 읽음
                return JsonUtility.FromJson<SkinData>(jsonData); // 그걸 jsonData로 바꿔서 리턴함
            }
            catch (Exception e) // 만약 에러뜨면 이걸로 보임
            {
                Debug.LogError("Error reading skin data file: " + e.Message);
            }
        }

        Debug.LogWarning("파일이 없거나 읽을 수 없음 SkinData 생성함");
        return new SkinData // 없으니까 리턴 하는데 새로운 데이터를 넣어 리턴함
        {
            ball = new SkinList { common = { "White" } },
            line = new SkinList { common = { "White" } },
            hook = new SkinList { common = { "White" } },
            trail = new SkinList { common = { "White" } }
        };
    }

    private void SaveSkinData(SkinData skinData)
    {
        string filePath = Application.persistentDataPath + "/SkinData.json"; // 파일 저장 위치
        try
        {
            string jsonData = JsonUtility.ToJson(skinData, true); // 수정했던 SkinData를 받아와서 Json파일로 만듦
            File.WriteAllText(filePath, jsonData); // 파일을 적어 저장함
        }
        catch (Exception e)
        {
            Debug.LogError("Error writing skin data file: " + e.Message); // 에러가 날 수 있다네요
        }
    }

    private bool SkinExists(SkinData skinData, int type, int rarity, string skinName) // 스킨이 이미 존재하는지 확인하는 코드
    {
        List<string> targetList = GetTargetList(skinData, type, rarity); // 리스트를 반환 받고
        return targetList.Contains(skinName); // 있는지 확인함 
    }

    private void AddSkin(SkinData skinData, int type, int rarity, string skinName) // 스킨을 추가하는 코드
    {
        List<string> targetList = GetTargetList(skinData, type, rarity); // 타입, 희귀도, 스킨이름을 토대로 리스트를 받아옴
        targetList.Add(skinName); // 추가함
    }

    private List<string> GetTargetList(SkinData skinData, int type, int rarity) // skinData.ball.common 같은 걸 반환해 줌
    {
        SkinList targetSkinList = type switch // targetSkinList에다가 SkinData.[int ??]f를 넣음 
        {
            0 => skinData.ball, // 0 = ball
            1 => skinData.line, // 1 = line
            2 => skinData.hook, // 2 = hook
            3 => skinData.trail, // 3 = trail
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Invalid type") // default
        };

        return rarity switch // 바로 위에서 받은 걸 바탕으로 레어도를 확인 함
        {
            0 => targetSkinList.common, // 0 = common
            1 => targetSkinList.rare, // 1 = rare
            2 => targetSkinList.epic, // 2 = epic
            _ => throw new ArgumentOutOfRangeException(nameof(rarity), "Invalid rarity") // default
        }; // 이로써 SkinData.ball.common같은걸 반환함
    }
// 스킨 데이터 저장 끝=====
    public SkinData SkinDataLoad()
    {
        string filePath = Application.persistentDataPath + "/SkinData.json";
        
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            SkinData skinData = JsonUtility.FromJson<SkinData>(jsonData);
            return skinData;
        }
        else
        {
            return null;
        }
    }
    
    public List<string> FullSkinDataLoad(int type, int rarity)
    {
        FullSkinData skinData;

        if (Resources.Load("GameData/fullSkinData"))
        {
            TextAsset textData = Resources.Load("GameData/fullSkinData") as TextAsset;
            skinData = JsonUtility.FromJson<FullSkinData>(textData.ToString());
        }
        else
        {
            skinData = new FullSkinData();
        }

        return FullGetTargetList(skinData, type, rarity);
    }

    private List<string> FullGetTargetList(FullSkinData skinData, int type, int rarity)
    {
        SkinList targetSkinList = type switch // switch로 -> SkinList 반환
        {
            0 => skinData.ball, // 0 = ball
            1 => skinData.line, // 1 = line
            2 => skinData.hook, // 2 = hook
            3 => skinData.trail, // 3 = trail
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Invalid type") // default
        };

        return rarity switch
        {
            0 => targetSkinList.common, // 0 = common
            1 => targetSkinList.rare, // 1 = rare
            2 => targetSkinList.epic, // 2 = epic
            _ => throw new ArgumentOutOfRangeException(nameof(rarity), "Invalid rarity") // default
        };
    }

    public void FullGameDataCreate() // 삭제할 것
    {
        string filePath = Application.persistentDataPath + "/fullSkinData.json";
        FullSkinData fullSkinData = new FullSkinData();
        string changeGameData = JsonUtility.ToJson(fullSkinData, true);
        File.WriteAllText(filePath, changeGameData);
    }

    #endregion
}
