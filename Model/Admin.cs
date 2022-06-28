namespace MenuAPI.Model
{
    public class Admin
    {
        string name, password;
        int id;

        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public int Id { get => id; set => id = value; }
    }
}
