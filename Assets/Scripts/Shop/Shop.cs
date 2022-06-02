using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.SimpleLocalization;

public class Shop : MonoBehaviour
{
    public static Shop instance { get; private set; }


    bool firstOpen;

    [SerializeField] GameObject main = null;

    [SerializeField] ShopPageData[] pages = new ShopPageData[0];
    [SerializeField] ShopPage pageButtonPrefab = null;
    [SerializeField] Transform buttonParent = null;
    [SerializeField] Transform scrollViewParent = null;
    [SerializeField] TextMeshProUGUI starTxt = null;

    [SerializeField] ShopMessage message;

    [Header("Localized Messages")]
    [SerializeField] LocalizedString msg_do_you_want_buy;
    [SerializeField] LocalizedString msg_do_yo_want_equip;
    [SerializeField] LocalizedString msg_no_enough_Stars;

    ShopPage[] myPages = new ShopPage[0];
    int[] equipedItems = new int[0];

    CustomsSaveData shopData;

    int currentPageIndex = 0;
    int currentItemIndex = 0;

    private void Start()
    {
        var saveData = new LevelsSaveData();
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
            shopData = SaveDataUtilities.RefreshCustomSaveData(pages[0].items.Length, pages[1].items.Length, pages[2].items.Length, pages[3].items.Length, shopData);
        }
        else
        {
            shopData = SaveDataUtilities.CreateCustomSaveData(pages[0].items.Length, pages[1].items.Length, pages[2].items.Length, pages[3].items.Length, saveData);
            BinarySerialization.Serialize(SaveDataUtilities.ShopSDName, shopData);
        }

        equipedItems = new int[pages.Length];
        equipedItems[0] = shopData.skinEquiped;
        equipedItems[1] = shopData.collectableEquiped;
        equipedItems[2] = shopData.spawnEquiped;
        equipedItems[3] = shopData.goalEquiped;


        CustomAssingment.instance.AddItem(new ShopItem[4] { pages[0].items[shopData.skinEquiped], pages[1].items[shopData.collectableEquiped],
        pages[2].items[shopData.spawnEquiped], pages[3].items[shopData.goalEquiped]});

        instance = this;
    }

    public void Open()
    {
        main.SetActive(true);
        if (!firstOpen)
        {
            firstOpen = true;
            myPages = new ShopPage[pages.Length];

            for (int i = 0; i < myPages.Length; i++)
            {
                myPages[i] = Instantiate(pageButtonPrefab, buttonParent);
                myPages[i].Create(scrollViewParent, pages[i], GetTypeOfShop(i), equipedItems[i], shopData.starAmmount, i);
                myPages[i].Close();
            }

            myPages[2].gameObject.SetActive(false);
        }
        RefreshStarUI();
        myPages[currentPageIndex].Open(shopData.starAmmount);
    }

    public void Close()
    {
        myPages[currentPageIndex].Close();
        main.SetActive(false);

        currentPageIndex = 0;
    }

    public void ChangePage(int index)
    {
        myPages[currentPageIndex].Close();
        currentPageIndex = index;
        myPages[currentPageIndex].Open(shopData.starAmmount);
    }

    public void EquipOther(int newEquip)
    {
        myPages[currentPageIndex].RefreshEquiped(equipedItems[currentPageIndex], newEquip);
        equipedItems[currentPageIndex] = newEquip;
        SaveIndex(currentPageIndex, newEquip);
        message.Close();
        message.gameObject.SetActive(false);
        CustomAssingment.instance.ModifyItem(currentPageIndex, pages[currentPageIndex].items[newEquip]);
    }


    public void DispatchMessage(int index, int cost, string _name)
    {
        currentItemIndex = index;
        message.gameObject.SetActive(true);
        if (cost > shopData.starAmmount) message.DispatchMessage(new UnityEngine.Events.UnityAction(Ok), msg_no_enough_Stars.Text);
        else
            message.DispatchQuestion(new UnityEngine.Events.UnityAction(Yes), new UnityEngine.Events.UnityAction(No), msg_do_you_want_buy.Text + " " + _name + "?");
    }


    void Yes()
    {
        shopData.starAmmount -= pages[currentPageIndex].items[currentItemIndex].cost;
        SaveBool(currentPageIndex, currentItemIndex);
        myPages[currentPageIndex].RefreshCost(shopData.starAmmount);
        myPages[currentPageIndex].SoldItem(currentItemIndex);
        RefreshStarUI();

        message.DispatchQuestion(new UnityEngine.Events.UnityAction(()=>EquipOther(currentItemIndex)), new UnityEngine.Events.UnityAction(No), msg_do_yo_want_equip.Text);
    }





    void No()
    {
        message.Close();
        message.gameObject.SetActive(false);
    }

    void Ok()
    {
        message.Close();
        message.gameObject.SetActive(false);
    }


    void RefreshStarUI()
    {
        starTxt.text = shopData.starAmmount.ToString();
    }

    public bool[] GetTypeOfShop(int index)
    {
        if (index == 0)
        {
            return shopData.skinsSolded;
        }
        else if (index == 1)
        {
            return shopData.collectablesSolded;
        }
        else if (index == 2)
        {
            return shopData.spawnSolded;
        }
        else if (index == 3)
        {
            return shopData.goalSolded;
        }
        else return default;
    }

    public void SaveIndex(int indexPage, int equipedToSave)
    {
        if (indexPage == 0)
        {
            shopData.skinEquiped = equipedToSave;
        }
        else if (indexPage == 1)
        {
            shopData.collectableEquiped = equipedToSave;
        }
        else if (indexPage == 2)
        {
            shopData.spawnEquiped = equipedToSave;
        }
        else if (indexPage == 3)
        {
            shopData.goalEquiped = equipedToSave;
        }
        BinarySerialization.Serialize(SaveDataUtilities.ShopSDName, shopData);
    }

    public void SaveBool(int indexPage, int sold)
    {
        if (indexPage == 0)
        {
            shopData.skinsSolded[sold] = true;
        }
        else if (indexPage == 1)
        {
            shopData.collectablesSolded[sold] = true;
        }
        else if (indexPage == 2)
        {
            shopData.spawnSolded[sold] = true;
        }
        else if (indexPage == 3)
        {
            shopData.goalSolded[sold] = true;
        }

        BinarySerialization.Serialize(SaveDataUtilities.ShopSDName, shopData);
    }
}