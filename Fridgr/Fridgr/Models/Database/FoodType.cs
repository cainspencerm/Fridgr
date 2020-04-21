namespace Fridgr.Models.Database
{
    public enum FoodType
    {
        Null = -1,

        FatSpreadAndOil = 0, // Fats, spreads, and oils
        Protein = 1, // Meat, poultry, fish, eggs, beans, and nuts
        DairyFood = 2, // Yogurt and cheese
        Grain = 3, // Wholemeal cereals and breads, potatoes, pasta, and rice
        FruitAndVegetable = 4, // Vegetables, salads, and fruit

        DairyDrink = 5, // Dairy (drink)
        SoftDrink = 6, // Soft drinks
        Juices = 7, // Juices
        Beer = 8, // Beer
        Cider = 9, // Cider
        Wine = 10, // Wine
        Spirits = 11, // Spirits
        CoffeeAndTea = 12, // Coffee, Tea

        Miscellaneous = 13 // Others
    }
}