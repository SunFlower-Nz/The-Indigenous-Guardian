using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public class BasicInfoChar
{
    public BasicStats baseInfo;
    public TypeCharacter typeChar;
}

public class PlayerStatsController : MonoBehaviour
{
    public static PlayerStatsController intance;

    public int xpMuiltiply =1;
    public float xpFirtLevel = 100;
    public float difficultFactor = 1.5f;
    public List<BasicInfoChar> baseInfoChars;



    // Start is called before the first frame update
    void Start()
    {
        intance = this;
        DontDestroyOnLoad(gameObject);
        //Application.LoadLevel("Gameplay");

        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public static void AddXp(float xpAdd)
    {
        float newXp = (GetCurrentXp()+ xpAdd) * PlayerStatsController.intance.xpMuiltiply;
        PlayerPrefs.SetFloat("currentXp", newXp);
    }

    public static float GetCurrentXp()
    {
        return (PlayerPrefs.GetFloat("currentXp"));
    }

    public static int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("currentLevel");
    }

    public static void AddLevel()
    {
        int newLevel = GetCurrentLevel() + 1;
        PlayerPrefs.SetInt("currentLevel", newLevel);
    }

    public static float GetNextXp()
    {
        return PlayerStatsController.intance.xpFirtLevel * (GetCurrentLevel() + 1);
    }

    public static TypeCharacter GetTypeCharacter()
    {
        int typeId = PlayerPrefs.GetInt("TypeCharacter");

        if(typeId == 0)
        {
            return TypeCharacter.Warrior;
        }
        else if(typeId == 1)
        {
            return TypeCharacter.Wizard;
        }
        else if(typeId == 2)
            return TypeCharacter.Archer;
        

        return TypeCharacter.Warrior;
    }

    public static void SetTypeCharacter(TypeCharacter newType)
    {
        PlayerPrefs.SetInt("TypeCharacter", (int)newType);
    }

    public BasicStats GetBasicStats(TypeCharacter type)
    {
        foreach(BasicInfoChar info in baseInfoChars)
        {
            if (info.typeChar == type)
                return info.baseInfo;
        }
        return baseInfoChars[0].baseInfo;
    }

}
