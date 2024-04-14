using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    Sprite[] item_sprites;

    [SerializeField]
    GameObject inventory_entry;

    [SerializeField]
    Transform inventory_root;

    List<int> item_counts = new List<int>();

    void Start()
    {
        foreach (Sprite sprite in item_sprites)
        {
            GameObject entry = Instantiate(inventory_entry, inventory_root);
            entry.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            entry.transform.GetChild(1).GetComponent<TMP_Text>().text = "0";
            //entry.SetActive(false);
            item_counts.Add(0);
        }
    }

    public void AdjustItem(int item_type, int adjustment)
    {
        item_counts[item_type] += adjustment;
        if (item_counts[item_type] > 0)
        {
            //inventory_root.GetChild(item_type).gameObject.SetActive(true);
        }
        inventory_root.GetChild(item_type).GetChild(1).GetComponent<TMP_Text>().text = item_counts[item_type].ToString();
    }

    public int GetCount(int item_type)
    {
        return item_counts[item_type];
    }
}
