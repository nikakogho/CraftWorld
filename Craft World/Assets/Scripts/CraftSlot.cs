using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour {
    public Item item;
    public int index = 1;
    Crafter crafter;
    Image image;

    public static CraftSlot first, second;

    void Awake()
    {
        image = GetComponent<Image>();

        if (index == 1) first = this;
        else second = this;
    }

    void Start()
    {
        crafter = Crafter.instance;
    }

    void AddItem()
    {
        if (crafter.chosenItem == null)
        {
            Clear();
        }
        else
        {
            item = crafter.chosenItem;
            image.sprite = item.icon;
        }

        crafter.AddItem(this);
    }

    public void Clear()
    {
        item = null;
        image.sprite = crafter.nothingIcon;
    }

    public void Pick()
    {
        AddItem();
        crafter.chosenItem = null;
    }
}
