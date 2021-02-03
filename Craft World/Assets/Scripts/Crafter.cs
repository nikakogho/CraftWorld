using UnityEngine;
using System.Collections.Generic;

public class Crafter : MonoBehaviour {
    public static Crafter instance;
    public Item item1, item2;
    Recipe outcome = null;
    GameMaster master;
    Inventory inventory;
    public Transform craftGrid;
    public GameObject outcomePrefab;
    public GameObject noOutcomeImage;
    public List<GameObject> outcomeSlots;
    public List<Item> newItems;
    public Sprite nothingIcon;
    public GameObject confirmer;
    public Item chosenItem;
    public delegate void OnComboChanged();
    public OnComboChanged onComboChanged;
    public CraftSlot slot1, slot2;

    void Awake()
    {
        instance = this;
        onComboChanged += UpdateOutcome;
    }

    void Start()
    {
        master = GameMaster.instance;
        inventory = Inventory.instance;
    }

    public void Confirm()
    {
        item1 = null;
        item2 = null;
        outcome = null;

        foreach(Item item in newItems)
        {
            inventory.AddItem(item);
        }

        newItems.Clear();

        slot1.Clear();
        slot2.Clear();

        onComboChanged.Invoke();
    }

    public void AddItem(CraftSlot slot)
    {
        if (slot.index == 1) item1 = slot.item;
        else item2 = slot.item;

        onComboChanged.Invoke();
    }

    void UpdateOutcome()
    {
        outcome = null;

        foreach(GameObject g in outcomeSlots)
        {
            Destroy(g);
        }

        newItems.Clear();
        outcomeSlots.Clear();
        noOutcomeImage.SetActive(true);
        confirmer.SetActive(false);
        if (item1 == null || item2 == null) return;

        foreach (Recipe recipe in master.Recipes)
        {
            if((recipe.item1 == item1 && recipe.item2 == item2) || (recipe.item1 == item2 && recipe.item2 == item1))
            {
                outcome = recipe;
                break;
            }
        }

        if(outcome != null)
        {
            noOutcomeImage.SetActive(false);
            confirmer.SetActive(true);
            SpawnSlot(outcome.outcome);

            foreach(Item item in outcome.bonusOutcomes)
            {
                SpawnSlot(item);
            }

            foreach(var possible in outcome.possibleOutcomes)
            {
                if(Random.Range(0f, 1f) < possible.chance)
                {
                    SpawnSlot(possible.item);
                }
            }
        }
    }

    void SpawnSlot(Item item)
    {
        GameObject g = Instantiate(outcomePrefab, transform.position, Quaternion.identity, craftGrid);
        ItemSlot slot = g.GetComponent<ItemSlot>();
        slot.item = item;
        slot.Apply();
        outcomeSlots.Add(g);

        bool isThere = false;

        foreach(Item i in inventory.items)
        {
            if(item == i)
            {
                isThere = true;
                break;
            }
        }

        if (!isThere)
        {
            newItems.Add(item);
        }
    }
}
