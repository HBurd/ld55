using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    int item_type;

    public int GetItemType()
    {
        return item_type;
    }
}
