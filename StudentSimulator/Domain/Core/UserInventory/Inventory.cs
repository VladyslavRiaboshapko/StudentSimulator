using System.Text.Json.Serialization;
using StudentSimulator.Domain.Interfaces;

namespace StudentSimulator.Domain.Core.UserInventory;

public class Inventory
{
    public int Capacity { get; private set; }
    public List<Slot> Slots {get;} = new();

    [JsonConstructor]
    public Inventory(int capacity = 5)
    {
        for (int i = 0; i < capacity; i++)
        {
            Slots.Add(new Slot());
        }
    }

    public bool AddItem(IItem item, int count)
    {
        int remaining = count;

        for (int i = 0; i < Slots.Count; i++)
        {
            var slot = Slots[i];

            if (Slots[i].Amount > 0 && Slots[i].Item.Id == item.Id && Slots[i].Amount < item.MaxStack)
            {
                int canAdd = item.MaxStack - Slots[i].Amount;
                int toAdd = (remaining > canAdd) ? canAdd : remaining;
                
                Slots[i].AddItem(item, toAdd);
                remaining -= toAdd;

                if (remaining <= 0)
                {
                    return true;
                }
            }
        }

        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].Amount == 0)
            {
                int toAdd = (remaining > item.MaxStack) ? item.MaxStack : remaining;
                
                Slots[i].AddItem(item, toAdd);
                remaining -= toAdd;
            }
            if (remaining <= 0)
            {
                return true;
            }
        }

        return remaining <= 0;
    }

    public bool RemoveItemById(int id, int count)
    {
        if (GetTotalCountById(id) < count)
        {
            return false; 
        }
        int remaining = count;

        for (int i = Slots.Count - 1; i >= 0; i--)
        {
            var slot = Slots[i];

            if (slot.Amount > 0 && slot.Item.Id == id)
            {
                int toRemove = (remaining > slot.Amount) ? slot.Amount : remaining;
                
                slot.RemoveItems(toRemove);
                remaining -= toRemove;
            }

            if (remaining <= 0)
            {
                return true;
            }
        }

        return remaining <= 0;
    }

    public int GetTotalCountById(int id)
    {
        int total = 0;
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].Amount > 0 && Slots[i].Item.Id == id)
            {
                total += Slots[i].Amount;
            }
        }

        return total;
    }
}
