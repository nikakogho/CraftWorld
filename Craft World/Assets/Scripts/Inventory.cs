using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public List<Item> items;
    List<ItemSlot> slots = new List<ItemSlot>();
    public Item[] startItems;
    public static Inventory instance;
    public Text amountText;
    public GameObject itemSlotPrefab;
    public Transform itemGrid;
    GameMaster master;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        master = GameMaster.instance;

        foreach (Item item in startItems)
        {
            AddItem(item);
        }
    }

    public void AddItem(Item item)
    {
        items.Add(item);

        ItemSlot slot = Instantiate(itemSlotPrefab, transform.position, Quaternion.identity, itemGrid).GetComponent<ItemSlot>();
        slot.item = item;
        slot.Apply();
        slots.Add(slot);

        amountText.text = items.Count.ToString();

        master.ItemAdded();
    }
}
