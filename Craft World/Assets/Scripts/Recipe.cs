using UnityEngine;

[CreateAssetMenu(fileName = "Item From Items", menuName = "Inventory/Recipe")]
public class Recipe : ScriptableObject {
    public Item item1, item2;
    public Item outcome;
    public Item[] bonusOutcomes;
    public IngredientBlueprint[] possibleOutcomes;
}

[System.Serializable]
public class IngredientBlueprint
{
    public Item item;
    [Range(0f, 1f)]
    public float chance = 0.5f;
}
