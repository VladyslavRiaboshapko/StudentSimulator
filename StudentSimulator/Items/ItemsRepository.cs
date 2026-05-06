using StudentSimulator.Items.Drinks;
using StudentSimulator.Items.Foods;
using StudentSimulator.Domain.Interfaces;

namespace StudentSimulator.Items;

public static class ItemsRepository
{
    private static readonly List<IItem> AllItems = new()
    {
        new Food(1, "Хот-Дог", 10, 0.25),
        new Food(2, "Шаурма", 5, 0.4),
        new Food(3, "Мівіна", 5, 0.16),
        new Food(4, "Піцца", 2, 0.5),
        new Food(5, "Пельмені", 1, 0.6),
        new Food(6, "Біг-Мак", 3, 0.2),
        new StandartDrink(7, "Вода Моршинська", 4, 0.5),
        new StandartDrink(8, "Coca-Cola", 4, 0.25),
        new StandartDrink(9, "Sprite", 4, 0.25),
        new StandartDrink(10, "Fanta", 4, 0.25),
        new StandartDrink(11, "Lipton", 2, 1),
        new CoffeineDrink(12, "Monster Energy", 2, 0.5, 20),
        new CoffeineDrink(13, "Red Bull", 4, 0.25, 12),
        new CoffeineDrink(13, "Кава Espresso", 1, 0.05, 15),
        new CoffeineDrink(14, "Лате", 3, 0.3, 25)
    };

    public static IItem? GetById(int id)
    {
        for(int i = 0; i < AllItems.Count; i++)
        {
            if(AllItems[i].Id == id)
            {
                return AllItems[i];
            }
        }

        return null;
    }

    public static int Count()
    {
        return AllItems.Count;
    }
}