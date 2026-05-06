using StudentSimulator.Domain.Interfaces;
using StudentSimulator.Items;

namespace StudentSimulator.Shops;

public static class SupermarketFactory
{
    public static Supermarket CreateSupermarket()
    {
        
        var productRange = new Dictionary<IItem, int>();
        
        AddProduct(productRange, 1, 55);  
        AddProduct(productRange, 2, 110); 
        AddProduct(productRange, 3, 15); 
        AddProduct(productRange, 4, 130);  
        AddProduct(productRange, 5, 100);
        AddProduct(productRange, 6, 20);  
        AddProduct(productRange, 7, 20); 
        AddProduct(productRange, 8, 20); 
        AddProduct(productRange, 9, 20);  
        AddProduct(productRange, 10, 25);
        AddProduct(productRange, 11, 70);  
        AddProduct(productRange, 12, 55); 
        AddProduct(productRange, 13, 40); 
        AddProduct(productRange, 14, 70);  

        return new Supermarket(1, "ФОРА", productRange);        
    }

    private static void AddProduct(Dictionary<IItem, int> range, int id, int price)
    {
        var item = ItemsRepository.GetById(id);
        if (item != null)
        {
            range.Add(item, price);
        }
    }
}