public class CraftingSystem
{
    public const int maxIngredients = 3;
    private Item[] items; 
    public CraftingSystem()
    {
        items = new Item[maxIngredients];
    }
    private bool IsEmpty(int index) { return items[index] == null; }
    private void SetItem(Item item, int index)
    {
        items[index] = item;
    }
    private void RemoveItem(Item item, int index)
    {
        SetItem(null, index);
    }
    private bool TryAddItem(Item item, int index)
    {
        if (IsEmpty(index))
        {
            SetItem(item, index);
            return true;
        }
        else
        {
            return false;
        }
    }
}

