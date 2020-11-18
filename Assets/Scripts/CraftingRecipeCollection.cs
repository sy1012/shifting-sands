using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Recipe Collection", menuName = "Crafting Recipe Collection")]
public class CraftingRecipeCollection : ScriptableObject
{
    [SerializeField]
    public List<CraftingRecipe> recipes;
}

