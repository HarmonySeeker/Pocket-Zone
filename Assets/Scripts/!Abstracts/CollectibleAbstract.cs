using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public abstract class CollectibleAbstract : MonoBehaviour
{
    public int MaxStack;
    public int CurrentStack;
    public Sprite InventoryImage;
    public string InventoryName;
}
