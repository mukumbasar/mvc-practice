namespace KitchenApp.Models
{
    public class RecipeItem
    {
        public int RecipeItemId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
    }
}