using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {
    public Item item;
    public Image image;
    public Button button;
    Crafter crafter;
    GameMaster master;
    public bool isRealSlot;

    [ContextMenu("Apply")]
    public void Apply()
    {
        image.sprite = item.icon;
    }

    void Awake()
    {
        //Apply();
    }

    void Start()
    {
        master = GameMaster.instance;

        if (isRealSlot)
        {
            crafter = Crafter.instance;
            crafter.onComboChanged += UpdateInteractability;
        }
    }

    void UpdateInteractability()
    {
        if(crafter.item1 == null && crafter.item2 == null)
        {
            button.interactable = true;
        }
        else if(crafter.item1 != null && crafter.item2 != null)
        {
            button.interactable = true;
        }
        else
        {
            Item item2 = crafter.item1 ?? crafter.item2;  //crafter.item1 != null ? crafter.item1 : crafter.item2

            bool comboExists = false;

            foreach(var recipe in master.Recipes)
            {
                if((recipe.item1 == item && recipe.item2 == item2) || (recipe.item2 == item && recipe.item1 == item2))
                {
                    comboExists = true;
                    break;
                }
            }

            button.interactable = comboExists;
        }
    }

    public void Pick()
    {
        if (crafter.chosenItem != item)
            crafter.chosenItem = item;
        else crafter.chosenItem = null;

        if (crafter.item1 == null)
        {
            CraftSlot.first.Pick();
        }
        else if (crafter.item2 == null)
        {
            CraftSlot.second.Pick();
        }
    }
}
