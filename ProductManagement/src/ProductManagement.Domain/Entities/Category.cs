namespace ProductManagement.Domain.Entities
{

    public class Category : Entity
    {

        public string Name { get; private set; } 
        public string Description { get; private set; }
        public ICollection<Product> Products { get; private set; } = new List<Product>();

        private  Category(){}

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Update(string name , string description)
        {

            Name = name;
            Description = description;
            UpdateLastUpdatedAt();
        }
    }
}
