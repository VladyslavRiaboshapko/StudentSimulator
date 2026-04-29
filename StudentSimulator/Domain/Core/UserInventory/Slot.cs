using StudentSimulator.Domain.Interfaces;

namespace StudentSimulator.Domain.Core.UserInventory;

public class Slot
{
    public IItem Item {get; private set;}
    public int Amount {get; set;}

    public bool AddItem(IItem item, int count)
    {
        if(count <= 0)
        {
            throw new ArgumentException("Count shoud be greater than 0!");
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
            throw new ArgumentException("Count shoud be greater than 0!");
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