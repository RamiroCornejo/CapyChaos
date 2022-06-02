using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleLocalization;

public class ShopPage : MonoBehaviour
{
    [SerializeField] ShopScrollView scrollPrefab = null;
    [SerializeField] ShopItemUI itemPrefab = null;

    ShopScrollView scroll;
    ShopItemUI[] items = new ShopItemUI[0];
    [SerializeField] TMPro.TextMeshProUGUI buttonText = null;
    [SerializeField] UnityEngine.UI.Image buttonImg = null;
    int index;

    public void Create(Transform scrollViewParent, ShopPageData data, bool[] solded, int equiped, int starAmmount, int _index)
    {
        index = _index;
        scroll = Instantiate(scrollPrefab, scrollViewParent);
        buttonText.text = data.pageName;
        var loc = buttonText.gameObject.AddComponent<LocalizedTextPro>();
        loc.LocalizationKey = data.pageName;

        items = new ShopItemUI[data.items.Length];

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = Instantiate(itemPrefab, scroll.itemsParent);
            items[i].SetUI(data.items[i], solded[i], i == equiped, starAmmount, i);
        }
    }

    public void Open(int starAmmount)
    {
        scroll.gameObject.SetActive(true);
        scroll.verticalBar.value = 1;
        GetComponent<UnityEngine.UI.Button>().interactable = false;
        buttonImg.color = Color.white;

        RefreshCost(starAmmount);
    }


    public void Close()
    {
        scroll.gameObject.SetActive(false);
        GetComponent<UnityEngine.UI.Button>().interactable = true;
        buttonImg.color = new Color32(82, 82, 82, 255);
    }

    public void RefreshCost(int starAmmount)
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].RefreshCost(starAmmount);
        }
    }

    public void RefreshEquiped(int prevEquiped, int newEquiped)
    {
        items[prevEquiped].RefreshUI(false);
        items[newEquiped].RefreshUI(true);
    }

    public void SoldItem(int itemSolded)
    {
        items[itemSolded].RefreshUI(false);
    }

    public void ChangePage()
    {
        Shop.instance.ChangePage(index);
    }
}
