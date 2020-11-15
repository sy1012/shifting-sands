using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem
{
    private static CraftingSystem _current;
    public static CraftingSystem current
    {
        get
        {
            if (_current == null) { _current = new CraftingSystem(); }
            return _current;
        }
    }

    public const int maxIngredients = 3;
    private ItemData[] ingredients;

    // Event Telling others what item has been crafter
    public event EventHandler onCraftSuccess;
    public class onCraftSuccessArgs : EventArgs { public ItemData result; public onCraftSuccessArgs(ItemData item) { this.result = item; } }

    //Recipes
    List<CraftingRecipe> recipes;

    private CraftingSystem()
    {
        ingredients = new ItemData[maxIngredients];
        recipes = Resources.Load<CraftingRecipeCollection>("CraftingRecipes").recipes;
    }

    private bool IsEmpty(int index) { return ingredients[index] == null; }
    private void SetItem(Item item, int index)
    {
        ingredients[index] = item.data;
    }
    public void RemoveItem(int index)
    {
        SetItem(null, index);
    }

    public void ClearItems()
    {
        ingredients = new ItemData[maxIngredients];
    }

    public void AddItems()
    {
        ingredients = new ItemData[maxIngredients];
    }

    /// <summary>
    /// Main interface for adding to crafing system. TODO: Make drag and drop call this
    /// </summary>
    /// <param name="item"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool TryAddItem(Item item, int index)
    {
        if (IsEmpty(index))
        {
            SetItem(item, index);
            //Auto craft if possible.
            UpdateCrafting();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateCrafting()
    {
        ItemData craftingResult = null;
        foreach (var recipe in recipes)
        {
            craftingResult = recipe.TryToCraft(ingredients);
            if (craftingResult != null)
            {
                // Successful match from ingredients to recipe. Consume Inputs and publish result
                for (int i = 0; i < ingredients.Length; i++)
                {
                    ingredients[i] = null;
                }
                onCraftSuccess?.Invoke(null, new onCraftSuccessArgs(craftingResult));
            }
        }
    }

    public ItemData Craft(ItemData[] ingredients)
    {
        ItemData craftingResult = null;
        foreach (var recipe in recipes)
        {
            craftingResult = recipe.TryToCraft(ingredients);
            if (craftingResult != null)
            {
                return craftingResult;
            }
        }
        return null;
    }
}
