using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class LanguageData
{
    public string language;
}

[Serializable]
public class LevelsSaveData
{
    public bool[] levelsCompleted;
    public bool[] capibaraClear;
    public bool[] collectableClear;
    public bool[] timeClear;
    public bool[] crownObtain;
}

[Serializable]
public class Settings
{
    public bool mute;
}

[Serializable]
public class CustomsSaveData
{
    public int starAmmount;

    public bool[] skinsSolded;
    public bool[] collectablesSolded;
    public bool[] spawnSolded;
    public bool[] goalSolded;

    public int skinEquiped = 0;
    public int collectableEquiped = 0;
    public int spawnEquiped = 0;
    public int goalEquiped = 0;
}

public static class SaveDataUtilities
{
    public static string LevelSaveDataName = "SaveData";
    public static string SettingsName = "Settings";
    public static string ShopSDName = "SDCustom";

    public static LevelsSaveData CreateSaveData()
    {
        LevelsSaveData saveData = new LevelsSaveData();
        int lenght = SceneManager.sceneCountInBuildSettings - 1;
        saveData.levelsCompleted = new bool[lenght];
        saveData.capibaraClear = new bool[lenght];
        saveData.collectableClear = new bool[lenght];
        saveData.timeClear = new bool[lenght];
        saveData.crownObtain = new bool[lenght];

        return saveData;
    }

    public static CustomsSaveData CreateCustomSaveData(int skinLength, int collectableLength, int spawnLength, int goalLength, LevelsSaveData saveData)
    {
        CustomsSaveData customSaveData = new CustomsSaveData();

        customSaveData.skinsSolded = new bool[skinLength];
        customSaveData.collectablesSolded = new bool[collectableLength];
        customSaveData.spawnSolded = new bool[spawnLength];
        customSaveData.goalSolded = new bool[goalLength];

        customSaveData.skinsSolded[0] = true;
        customSaveData.collectablesSolded[0] = true;
        customSaveData.spawnSolded[0] = true;
        customSaveData.goalSolded[0] = true;

        for (int i = 0; i < saveData.levelsCompleted.Length; i++)
        {
            if (saveData.levelsCompleted[i]) customSaveData.starAmmount += 1;
            else break;
            if (saveData.capibaraClear[i]) customSaveData.starAmmount += 1;
            if (saveData.collectableClear[i]) customSaveData.starAmmount += 1;
            if (saveData.timeClear[i]) customSaveData.starAmmount += 1;
        }
        return customSaveData;
    }

    public static CustomsSaveData RefreshCustomSaveData(int skinLength, int collectableLength, int spawnLength, int goalLength, CustomsSaveData data)
    {
        CustomsSaveData customSaveData = new CustomsSaveData();

        customSaveData.skinsSolded = new bool[skinLength];
        customSaveData.collectablesSolded = new bool[collectableLength];
        customSaveData.spawnSolded = new bool[spawnLength];
        customSaveData.goalSolded = new bool[goalLength];
        for (int i = 0; i < customSaveData.skinsSolded.Length; i++)
        {
            if (i >= data.skinsSolded.Length) break;
            customSaveData.skinsSolded[i] = data.skinsSolded[i];
        }
        for (int i = 0; i < customSaveData.collectablesSolded.Length; i++)
        {
            if (i >= data.collectablesSolded.Length) break;
            customSaveData.collectablesSolded[i] = data.collectablesSolded[i];
        }
        for (int i = 0; i < customSaveData.spawnSolded.Length; i++)
        {
            if (i >= data.spawnSolded.Length) break;
            customSaveData.spawnSolded[i] = data.spawnSolded[i];
        }
        for (int i = 0; i < customSaveData.goalSolded.Length; i++)
        {
            if (i >= data.goalSolded.Length) break;
            customSaveData.goalSolded[i] = data.goalSolded[i];
        }

        customSaveData.skinEquiped = skinLength <= data.skinEquiped ? 0 : data.skinEquiped;
        customSaveData.collectableEquiped = collectableLength <= data.collectableEquiped ? 0 : data.collectableEquiped;
        customSaveData.spawnEquiped = spawnLength <= data.spawnEquiped ? 0 : data.spawnEquiped;
        customSaveData.goalEquiped = goalLength <= data.goalEquiped ? 0 : data.goalEquiped;

        customSaveData.starAmmount = data.starAmmount;


        return customSaveData;
    }

    public static LevelsSaveData RefreshSaveData(LevelsSaveData saveData)
    {
        if (saveData.levelsCompleted.Length == SceneManager.sceneCountInBuildSettings - 1) return saveData;

        LevelsSaveData newSaveData = CreateSaveData();

        for (int i = 0; i < newSaveData.levelsCompleted.Length; i++)
        {
            if (i >= saveData.levelsCompleted.Length) break;
            newSaveData.levelsCompleted[i] = saveData.levelsCompleted[i];
            newSaveData.capibaraClear[i] = saveData.capibaraClear[i];
            newSaveData.collectableClear[i] = saveData.collectableClear[i];
            newSaveData.timeClear[i] = saveData.timeClear[i];
            newSaveData.crownObtain[i] = saveData.timeClear[i];
        }

        return newSaveData;
    }
}

public static class CoroutineUtility
{
    public static IEnumerator ScaleThings(float scaleSpeed, Transform transformToScale, Vector3 from, Vector3 to)
    {
        float currentLerp = 0;

        while (currentLerp < 1)
        {
            currentLerp += Time.deltaTime * scaleSpeed;
            transformToScale.localScale = Vector3.Lerp(from, to, currentLerp);

            yield return new WaitForEndOfFrame();
        }
    }
}