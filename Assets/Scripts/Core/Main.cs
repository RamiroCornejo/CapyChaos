using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevelopTools;
using System;
using UnityEngine.Events;

public class Main : MonoBehaviour
{
    public static Main instance { get; private set; }

    public int objectiveCharacter = 3;
    [SerializeField] int objectiveSeconds = 100;
    public float seconds;

    CorpsePool corpsePool;
    public DestCorpse corpsePrefab = null;

    bool gameOver;
    public LevelsSaveData saveData;
    CustomsSaveData shopData;

    int charactersOnGame;
    int charactersWin;
    int maxCharacters;
    int ammountToCollect;
    int currentCollect;

    [SerializeField] float fadeSpeed = 0.5f;
    [SerializeField] AudioSource musicSource = null;

    [SerializeField] ShopPageData[] pageData = new ShopPageData[0];

    bool fadingMusic;
    float fade;

    public UnityEvent OnBeginGame;

    private void Awake()
    {
        instance = this;

        if (BinarySerialization.IsFileExist(SaveDataUtilities.LevelSaveDataName))
        {
            saveData = BinarySerialization.Deserialize<LevelsSaveData>(SaveDataUtilities.LevelSaveDataName);
            saveData = SaveDataUtilities.RefreshSaveData(saveData);
        }
        else
        {
            saveData = SaveDataUtilities.CreateSaveData();
            BinarySerialization.Serialize(SaveDataUtilities.LevelSaveDataName, saveData);
        }

        if (BinarySerialization.IsFileExist(SaveDataUtilities.ShopSDName))
        {
            shopData = BinarySerialization.Deserialize<CustomsSaveData>(SaveDataUtilities.ShopSDName);
        }

#if UNITY_EDITOR
        if (CustomAssingment.instance != null)
        {
            CustomAssingment.instance.AssignCustoms();
        }
#else
        var collectables = FindObjectsOfType<Collectable>();
        var collItm = (CollectItm)pageData[0].items[shopData.collectableEquiped];
        for (int i = 0; i < collectables.Length; i++)
        {
            collectables[i].prefab = collItm.collectableGO;
            collectables[i].collectParticle = collItm.particle;
        }

        var goal = FindObjectOfType<Goal>();
        var goalItm = pageData[1].items[shopData.goalEquiped] as GoalItm;
        goal.decoration = goalItm.goal;
#endif

        corpsePool = new GameObject($"{corpsePrefab.name} pool").AddComponent<CorpsePool>();
        corpsePool.transform.SetParent(transform);
        corpsePool.Configure(corpsePrefab);
        corpsePool.Initialize(3);


    }
    private void Start()
    {
        fade = musicSource.volume;
#if !UNITY_EDITOR
        var collectables = FindObjectsOfType<Collectable>();
        var collItm = (CollectItm)pageData[0].items[shopData.collectableEquiped];
        for (int i = 0; i < collectables.Length; i++)
        {
            collectables[i].prefab = collItm.collectableGO;
            collectables[i].collectParticle = collItm.particle;
        }
        UIManager.instance.SetCollectables(collItm.spriteOK, collItm.spriteNull);

        var goal = FindObjectOfType<Goal>();
        var goalItm = pageData[1].items[shopData.goalEquiped] as GoalItm;
        goal.decoration = goalItm.goal;
#endif
        //Localization.instance.SaveLanguage("English");
    }


    public void BeginGame()
    {
        OnBeginGame.Invoke();
    }

    private void Update()
    {
        seconds += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
            UIManager.instance.ReturnToMenu();

        if (Input.GetKeyDown(KeyCode.R))
            UIManager.instance.Retry();

        if (fadingMusic)
        {
            fade -= Time.deltaTime * fadeSpeed;
            if (fade <= 0)
            {
                fade = 0;
                fadingMusic = false;
            }
            musicSource.volume = fade;
        }
    }

    public void FadeMusic()
    {
        fadingMusic = true;
    }

    public void AddCharacter(Character ch)
    {
        maxCharacters += 1;
        charactersOnGame += 1;
    }
    public void AddCollectable()
    {
        ammountToCollect += 1;
    }

    public void CollectItem()
    {
        currentCollect += 1;
    }

    public void RemoveCharacter(Character ch)
    {
        charactersOnGame -= 1;

        if(charactersOnGame == 0 && charactersWin >= objectiveCharacter)
        {
            WinScreen();
            return;
        }

        if (charactersOnGame < objectiveCharacter - charactersWin) LoseScreen();
    }

    public void CharacterWin(Character ch)
    {
        charactersOnGame -= 1;
        charactersWin += 1;

        if(charactersWin <= objectiveCharacter){

            UIManager.instance.ChangeCounter(objectiveCharacter - charactersWin);
        }

        if (charactersOnGame == 0 && charactersWin >= objectiveCharacter)
        {
            WinScreen();
        }
    }

    public void Save()
    {
        BinarySerialization.Serialize(SaveDataUtilities.LevelSaveDataName, saveData);
    }

    public void AddStars(int ammount)
    {
        if (shopData != null)
        {
            shopData.starAmmount += ammount;
            BinarySerialization.Serialize(SaveDataUtilities.ShopSDName, shopData);
        }
    }

    void WinScreen()
    {
        if (gameOver) return;
        gameOver = true;
        UIManager.instance.ShowWin(maxCharacters, charactersWin, ammountToCollect, currentCollect, objectiveSeconds, (int)seconds);
    }

    void LoseScreen()
    {
        if (gameOver) return;
        gameOver = true;
        UIManager.instance.ShowLose();
    }

    public void ActiveCorpse(Transform root)
    {
        DestCorpse aS = corpsePool.Get();
        aS.transform.position = root.position;
        aS.transform.eulerAngles = root.eulerAngles;
        aS.Disarm();
    }

    public void ActiveCorpse(Transform root, bool in_head, GameObject object_reference, Action OnEndEvent)
    {
        DestCorpse aS = corpsePool.Get();
        aS.transform.position = root.position;
        aS.transform.eulerAngles = root.eulerAngles;
        aS.Disarm(in_head, object_reference, OnEndEvent);
    }

    public void ReturnCorpse(DestCorpse corpse)
    {
        corpsePool.ReturnToPool(corpse);
    }
}
