namespace ProductManagement.Domain.Entities
{

    public class Product : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }
        public decimal Price { get; private set; }
        public string ImageUrl { get; private set; }

        private  Product() { }

        public Product(string name, string description, int categoryId, decimal price, string imageUrl)
        {
            Name = name;
            Description = description;
            CategoryId = categoryId;
            Price = price;
            ImageUrl = imageUrl;
        }

        public void Update(string name, string description, int categoryId, decimal price, string imageUrl, bool isActive)
        {
            Name = name;
            Description = description;
            CategoryId = categoryId;
            Price = price;
            ImageUrl = imageUrl;

            if (isActive) SetActive();
            else SetInactive();

            UpdateLastUpdatedAt();
        }

    }
}
