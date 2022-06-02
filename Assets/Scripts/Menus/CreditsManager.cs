using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    public static CreditsManager instance { get; private set; }

    [SerializeField] string[] creditsNames;

    int currentName = 0;

    CorpsePool corpsePool;
    [SerializeField] DestCorpse corpsePrefab = null;

    private void Awake()
    {
        instance = this;
        corpsePool = new GameObject($"{corpsePrefab.name} pool").AddComponent<CorpsePool>();
        corpsePool.transform.SetParent(transform);
        corpsePool.Configure(corpsePrefab);
        corpsePool.Initialize(3);
    }
    public void ActiveCorpse(Transform root)
    {
        DestCorpse aS = corpsePool.Get();
        aS.transform.position = root.position;
        aS.transform.eulerAngles = root.eulerAngles;
        aS.Disarm();
    }

    public string GetName()
    {
        if (currentName == 0) RandomizeNames();

        string nameToGet = creditsNames[currentName];

        currentName += 1;

        if (currentName >= creditsNames.Length) currentName = 0;

        return nameToGet;
    }

    void RandomizeNames()
    {
        for (int i = 0; i < creditsNames.Length; i++)
        {
            int randomNumber = Random.Range(i, creditsNames.Length);
            string randomName = creditsNames[randomNumber];
            creditsNames[randomNumber] = creditsNames[i];
            creditsNames[i] = randomName;
        }
    }

    public void ReturnCorpse(DestCorpse corpse)
    {
        corpsePool.ReturnToPool(corpse);
    }
}
