namespace MenuAPI.Model
{
    public class Menu
    {
        string name, description;
        int id, price;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public int Id { get => id; set => id = value; }
        public int Price { get => price; set => price = value; }
    }
}
