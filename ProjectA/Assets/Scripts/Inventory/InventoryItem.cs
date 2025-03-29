using System;

[Serializable]
public class InventoryItem
{
    public ItemData data;
    public int stack;

    public InventoryItem(ItemData itemData)
    {
        data = itemData;
        AddStack();
    }

    public void AddStack() => stack++;
    public void RemoveStack() => stack--;
}
