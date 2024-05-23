using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatternManager : MonoBehaviour
{
    [Serializable]
    public class Pattern
    {
        public List<GameObject> Map;
    }
    
    public GameObject Player;
    private Vector3 beforePlayerPosition;

   
    
    public List<Pattern> MapPattern;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x > beforePlayerPosition.x + 44.5f)
        {
            //맵 생성
            int mapLevel = GameManager.Instance.Level;
            int mapDifficulty = Random.Range(0, mapLevel+1);
            var newpattern = Instantiate(MapPattern[mapDifficulty].Map[Random.Range(0, MapPattern[mapDifficulty].Map.Count)]);
            newpattern.transform.position = new Vector3(Player.transform.position.x + 40f, 15f, 0f);
            newpattern.GetComponent<Block>().Player = Player;

            beforePlayerPosition.x = Player.transform.position.x;
        }
    }
}
