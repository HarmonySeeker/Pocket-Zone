using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IDataPersistence
{
    public GridLayoutGroup inventoryContent;
    public GameObject itemTemplate;
    [SerializeField] private List<Tuple<Collectible, GameObject>> inventory;
    
    private bool wasCreated = false;
    private GameObject newItem;
    private Tuple<Collectible, GameObject> tempTuple;

    private void Awake()
    {
        inventory = new List<Tuple<Collectible, GameObject>> ();
        tempTuple = null;
    }

    public void AddCollectible(Collectible collectible)
    {
        if (collectible.MaxStack != 1)
        {
            foreach (Tuple<Collectible, GameObject> item in inventory)
            {
                if (item.Item1.InventoryName == collectible.InventoryName)
                {
                    if (item.Item1.CurrentStack + collectible.CurrentStack <= collectible.MaxStack)
                    {
                        item.Item1.CurrentStack += collectible.CurrentStack;
                        item.Item2.GetComponentInChildren<TextMeshProUGUI>().text = item.Item1.CurrentStack.ToString();
                        wasCreated = true;
                        break;
                    } else {
                        tempTuple = item;
                    }
                }
            }
        } else {
            GameObject newItem = Instantiate(itemTemplate);
            newItem.name = collectible.InventoryName;
            newItem.GetComponentInChildren<Image>().sprite=collectible.InventoryImage;
            newItem.GetComponentInChildren<TextMeshProUGUI>().text = "";
            inventory.Add(new Tuple<Collectible, GameObject>(collectible, newItem));
            newItem.transform.SetParent(inventoryContent.transform, false);
            ButtonDeleter deleteTemp = newItem.GetComponentInChildren<ButtonDeleter>();
            deleteTemp.manager = this;
            deleteTemp.gameObject.SetActive(false);

            wasCreated = true;
        }

        if (!wasCreated)
        {
            if (tempTuple != null)
            {
                int tempDiff = tempTuple.Item1.MaxStack - tempTuple.Item1.CurrentStack;
                tempTuple.Item1.CurrentStack = tempTuple.Item1.MaxStack;
                collectible.CurrentStack -= tempDiff;

                GameObject newItem = Instantiate(itemTemplate);
                newItem.name = collectible.InventoryName;
                newItem.GetComponentInChildren<Image>().sprite = collectible.InventoryImage;
                tempTuple.Item2.GetComponentInChildren<TextMeshProUGUI>().text = tempTuple.Item1.CurrentStack.ToString();
                newItem.GetComponentInChildren<TextMeshProUGUI>().text = collectible.CurrentStack.ToString();
                inventory.Add(new Tuple<Collectible, GameObject>(collectible, newItem));
                newItem.transform.SetParent(inventoryContent.transform, false);

                ButtonDeleter deleteTemp = newItem.GetComponentInChildren<ButtonDeleter>();
                deleteTemp.manager = this;
                deleteTemp.gameObject.SetActive(false);

                tempTuple = null;
            } else {
                GameObject newItem = Instantiate(itemTemplate);
                newItem.name = collectible.InventoryName;
                newItem.GetComponentInChildren<Image>().sprite = collectible.InventoryImage;
                newItem.GetComponentInChildren<TextMeshProUGUI>().text = collectible.CurrentStack.ToString();
                ButtonDeleter deleteTemp = newItem.GetComponentInChildren<ButtonDeleter>();
                deleteTemp.manager = this;
                deleteTemp.gameObject.SetActive(false);

                inventory.Add(new Tuple<Collectible, GameObject>(collectible, newItem));
                newItem.transform.SetParent(inventoryContent.transform, false);
            }
        }

        wasCreated = false;
    }

    public void RemoveCollectible(GameObject inventoryItem)
    {
        foreach (Tuple<Collectible, GameObject> item in inventory)
        {
            if (item.Item2 == inventoryItem)
            {
                item.Item1.collected = false;
                inventory.Remove(item);
                Destroy(inventoryItem);
                break;
            }
        }
    }

    public void LoadData(GameData data)
    {
        //do nothing (i should have, but it's 3 am)
    }

    public void SaveData(ref GameData data)
    {
        //do nothing
    }
}
