using System.Text.Json.Serialization;
using StudentSimulator.Domain.Interfaces;

namespace StudentSimulator.Domain.Core.UserInventory;

public class Slot
{
    public IItem Item {get; set;}
    public int Amount {get; set;}

    [JsonConstructor]
    public Slot(IItem item, int amount)
    {
        Item = item;
        Amount = amount;
    }

    public Slot()
    {
        Item = null;
        Amount = 0;
    }

    public bool AddItem(IItem item, int count)
    {
        if(count <= 0)
        {
            throw new ArgumentException("Кількість повинна бути більше 0!");
        }
        if (Amount == 0)
        {
            Item = item;
        }
        else if(Item.Id != item.Id || Amount == Item.MaxStack)
        {
            return false;
        }

        Amount += count;

        if(Amount > Item.MaxStack)
        {
            Amount = Item.MaxStack;
        }

        return true;
    }

    public bool RemoveItems(int count)
    {
        if (count == 0)
        {
            throw new ArgumentException("Кількість повинна бути більше 0!");
        }

        if (Amount == 0)
        {
            return false;
        }

        int toRemove = Math.Min(count, Amount);
        Amount -= toRemove;

        if (Amount == 0)
        {
            Item = null; 
        }

        return true;
    }
}