using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FoodCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cellCountTxt;
    [SerializeField] private Image cellImg;
    [SerializeField] private FoodItem item;

    public Button button;

    public FoodItem Item
    {
        get => item;
        set => item = value;
    }


    public void AddItem(FoodItem newItem)
    {
        item = newItem;
        CellUpdate();
    }

    public void CellUpdate()
    {
        gameObject.SetActive(Item != null);
        if (Item == null)
        {
            Debug.Log("Cell item is null");
            return;
        }

        if (item.Amount == 0)
        {
            gameObject.SetActive(false);
            Item.ResetItem();
            return;
        }

        cellCountTxt.text = item.Amount.ToString();
        cellImg.sprite = item.FoodType.icon;
    }

    public void DeleteItem()
    {
        Item.ResetItem();
        CellUpdate();
    }

    public void ButtonClick(int amount = 1)
    {
        var parent = transform.parent;
        parent.GetComponent<FoodHandler>().OnFeed.Invoke(item);
        Item.AddItemCount(-amount);
        CellUpdate();
        parent.GetComponent<FoodHandler>().CheckCellCount(item);
    }
}
