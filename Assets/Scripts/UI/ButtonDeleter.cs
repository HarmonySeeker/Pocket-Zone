using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDeleter : MonoBehaviour
{
    public InventoryManager manager;
    public GameObject item;
    public void DeleteSelf()
    {
        manager.RemoveCollectible(item);
    }
}
