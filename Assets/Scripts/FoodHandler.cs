using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FoodHandler : MonoBehaviour
{
    [SerializeField] private FoodContainerSO foodContainer;
    [SerializeField] private FoodTypeListSO foodTypes;
    [SerializeField] private GameObject foodCellPrefab;
    [SerializeField] private int cellPageSize = 5;
    [SerializeField] private Button prevButton, nextButton;

    private List<FoodCell> _cellPage;

    public int currentPage = 0;
    public Action<FoodItem> OnFeed;

    private int _maxPageAmount;
    private int _maxPageCurrent;


    private void Awake()
    {
        Init();
        cellPageSize = GetComponent<GridLayoutGroup>().constraintCount;
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        ShowPage(currentPage);
    }

    private void Init()
    {
        _cellPage = new List<FoodCell>();
        _maxPageAmount = Mathf.CeilToInt((float) foodTypes.foodTypesList.Count / cellPageSize) - 1;
        for (int i = 0; i < cellPageSize; i++)
        {
            var go = Instantiate(foodCellPrefab, transform);
            _cellPage.Add(go.GetComponent<FoodCell>());
        }
    }

    private void ShowPage(int pageNumber)
    {
        _maxPageCurrent = Mathf.CeilToInt((float) foodContainer.container.Count / cellPageSize) - 1;
        for (int i = 0; i < cellPageSize; i++)
        {
            try
            {
                _cellPage[i].AddItem(foodContainer.container[i + pageNumber * cellPageSize]);
            }
            catch (Exception e)
            {
                _cellPage[i].AddItem(null);
            }
        }

        ShowButton();
    }

    public void CheckCellCount(FoodItem item)
    {
        if (item.Amount == 0)
        {
            bool isPageEmpty = true;
            foreach (var foodCell in _cellPage)
            {
                if (foodCell.gameObject.activeInHierarchy)
                {
                    isPageEmpty = false;
                    break;
                }
            }

            foodContainer.container.Remove(item);
            if (isPageEmpty && currentPage > 0)
            {
                _maxPageCurrent--;
                NextPage(-1);
            }

            if (!isPageEmpty)
                ShowPage(currentPage);
        }
    }

    public void AddRandomFoodItem()
    {
        int itemNumber = Random.Range(0, foodTypes.foodTypesList.Count);
        AddFood(new FoodItem(foodTypes.foodTypesList[itemNumber], 1));
    }

    private void AddFood(FoodItem item, int count = 1)
    {
        List<FoodItem> container = foodContainer.container;
        var findingItem = container.FirstOrDefault(a => a.FoodType == item.FoodType);
        if (findingItem != null)
            findingItem.AddItemCount(count);
        else
        {
            container.Add(item);
        }

        ShowPage(currentPage);
    }

    public void NextPage(int side)
    {
        int nextPage = currentPage + side;
        if (nextPage < 0)
        {
            currentPage = _maxPageCurrent;
        }
        else if (nextPage > _maxPageCurrent)
            currentPage = 0;
        else
        {
            currentPage = nextPage;
        }

        ShowPage(currentPage);
        ShowButton();
    }

    public void ShowButton()
    {
        prevButton.gameObject.SetActive(currentPage != 0);
        nextButton.gameObject.SetActive(currentPage != _maxPageCurrent);
    }
}
