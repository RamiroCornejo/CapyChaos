using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAssingment : MonoBehaviour
{
    public static CustomAssingment instance;

    [SerializeField] List<ShopItem> items = new List<ShopItem>();

    public Character character;
    public DestCorpse corpse;

    public Character characterPrefab;
    public DestCorpse corpsePrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
#if UNITY_EDITOR
        DontDestroyOnLoad(this.gameObject);
#endif
    }
    bool alreadyAgregate;

    public void AddItem(ShopItem[] item)
    {
        if (alreadyAgregate) return;

        for (int i = 0; i < item.Length; i++) { items.Add(item[i]); items[i].RefreshItem(this); }

        alreadyAgregate = true;
    }

    public void ModifyItem(int index, ShopItem item)
    {
        items[index] = item;
        items[index].RefreshItem(this);
    }


    public void AssignCustoms()
    {
        var spawners = FindObjectsOfType<CharacterSpawner>();
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].prefab = character;
        }
        Main.instance.corpsePrefab = corpse;

        var collectables = FindObjectsOfType<Collectable>();
        var collItm = (CollectItm)items[1];
        for (int i = 0; i < collectables.Length; i++)
        {
            collectables[i].prefab = collItm.collectableGO;
            collectables[i].collectParticle = collItm.particle;
        }

        UIManager.instance.SetCollectables(collItm.spriteOK, collItm.spriteNull);

        var goal = FindObjectOfType<Goal>();
        var goalItm = items[3] as GoalItm;
        goal.decoration = goalItm.goal;
    }

}
