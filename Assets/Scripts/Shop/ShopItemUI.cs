using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.SimpleLocalization;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] Image itemSprite = null;
    [SerializeField] TextMeshProUGUI nameTxt = null;
    [SerializeField] GameObject costGO = null;
    [SerializeField] TextMeshProUGUI costTxt = null;
    [SerializeField] GameObject soldGO = null;
    [SerializeField] GameObject equipedGO = null;
    [SerializeField] Button itemButton = null;

    bool isSolded;
    bool isEquiped;
    ShopItem itemToRepresent;
    int index;

    public void SetUI(ShopItem item, bool _isSolded, bool _isEquiped, int starAmmount, int _index)
    {
        index = _index;
        itemToRepresent = item;
        itemSprite.sprite = item.shopSprite;
        nameTxt.text = item.itemName;
        var loc = nameTxt.gameObject.AddComponent<LocalizedTextPro>();
        loc.LocalizationKey = item.itemName;

        if (_isSolded)
        {
            isSolded = true;
            if (_isEquiped)
            {
                equipedGO.SetActive(true);
                isEquiped = true;
                itemButton.interactable = false;
                itemButton.GetComponent<Image>().color = new Color32(92, 92, 92, 255);
            }
            else
            {
                soldGO.SetActive(true);
                itemButton.interactable = true;
                itemButton.GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            costGO.SetActive(true);
            costTxt.text = item.cost.ToString();

            if (item.cost > starAmmount)
                costTxt.color = Color.red;
            else
                costTxt.color = Color.white;
        }
    }

    public void Buy()
    {
        if (isSolded)
        {
            Shop.instance.EquipOther(index);
        }
        else
        {
            Shop.instance.DispatchMessage(index, itemToRepresent.cost, nameTxt.text);
        }
    }

    public void RefreshUI(bool equiped)
    {
        isSolded = true;
        isEquiped = equiped;
        costGO.SetActive(false);

        equipedGO.SetActive(isEquiped);
        soldGO.SetActive(!isEquiped);
        itemButton.interactable = !isEquiped;
        itemButton.GetComponent<Image>().color = isEquiped ? new Color32(92, 92, 92, 255) : new Color32(255, 255, 255, 255);
    }

    public void RefreshCost(int starAmmount)
    {
        if (itemToRepresent.cost > starAmmount)
            costTxt.color = Color.red;
        else
            costTxt.color = Color.white;
    }
}
