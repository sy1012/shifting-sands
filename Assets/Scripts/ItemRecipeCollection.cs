using UnityEngine;

[System.Serializable]
public class CraftingRecipe
{
    [SerializeField]
    private ItemData[] recipeInputs;
    [SerializeField]
    private ItemData output;
    public CraftingRecipe(ItemData output, ItemData[] inputs)
    {
        this.output = output;
        this.recipeInputs = inputs;
    }

    /// <summary>
    /// TODO: Known bug that one input item will get counted twice. Color inputs to fix this.
    /// </summary>
    /// <param name="inputs"></param>
    /// <returns></returns>
    public ItemData TryToCraft(ItemData[] inputs)
    {
        for (int i = 0; i < recipeInputs.Length; i++)
        {
            bool has = false;
            for (int j = 0; j < inputs.Length; j++)
            {
                if (inputs[j] != null && recipeInputs[i].description == inputs[j].description)
                {
                    //match
                    has = true;
                    break;
                }
            }
            if (!has)
            {
                //No match
                return null;
            }
        }
        //All ingredients present. Return output!
        return output;
    }
}

