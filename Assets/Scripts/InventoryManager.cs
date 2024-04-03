using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GridLayoutGroup inventoryContent;
    public GameObject itemTemplate;
    [SerializeField] private List<Tuple<CollectibleAbstract, GameObject>> inventory;
    private bool wasCreated = false;

    private void Awake()
    {
        inventory = new List<Tuple<CollectibleAbstract, GameObject>> ();
    }

    public void AddCollectible(CollectibleAbstract collectible)
    {
        if (collectible.MaxStack != 1)
        {
            foreach (Tuple<CollectibleAbstract, GameObject> item in inventory)
            {
                if (item.Item1.InventoryName == collectible.InventoryName)
                {
                    if (item.Item1.CurrentStack + collectible.CurrentStack <= collectible.MaxStack)
                    {
                        item.Item1.CurrentStack += collectible.CurrentStack;
                        item.Item2.GetComponentInChildren<TextMeshProUGUI>().text = item.Item1.CurrentStack.ToString();
                    } else {
                        int tempDiff = item.Item1.MaxStack - item.Item1.CurrentStack;
                        item.Item1.CurrentStack = item.Item1.MaxStack;
                        collectible.CurrentStack -= tempDiff;
                        
                        GameObject newItem = Instantiate(itemTemplate);
                        newItem.GetComponent<Image>().sprite = collectible.InventoryImage;
                        newItem.GetComponentInChildren<TextMeshProUGUI>().text = collectible.CurrentStack.ToString();
                        inventory.Add(new Tuple<CollectibleAbstract, GameObject>(collectible, newItem));
                        newItem.transform.SetParent(inventoryContent.transform, false);
                    }

                    wasCreated = true;
                    break;
                }
            }
        } else {
            GameObject newItem = Instantiate(itemTemplate);
            newItem.GetComponent<SpriteRenderer>().sprite= collectible.InventoryImage;
            newItem.GetComponentInChildren<TextMeshProUGUI>().text = "";
            inventory.Add(new Tuple<CollectibleAbstract, GameObject>(collectible, newItem));
            newItem.transform.SetParent(inventoryContent.transform, false);

            wasCreated = true;
        }

        if (!wasCreated)
        {
            GameObject newItem = Instantiate(itemTemplate);
            newItem.GetComponent<SpriteRenderer>().sprite = collectible.InventoryImage;
            if (collectible.MaxStack != 1)
            {
                newItem.GetComponentInChildren<TextMeshProUGUI>().text = collectible.CurrentStack.ToString();
            } else {
                newItem.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
            inventory.Add(new Tuple<CollectibleAbstract, GameObject>(collectible, newItem));
            newItem.transform.SetParent(inventoryContent.transform, false);

            wasCreated = false;
        }
    }
}
