/// <summary>
/// Represents a product with a name, description, and price.
/// </summary>
[System.Serializable]
public class Product
{
    public string name;
    public string description;
    public float price;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Product"/> class with the specified name, description, and price.
    /// </summary>
    /// <param name="name">The name of the product.</param>
    /// <param name="description">The description of the product.</param>
    /// <param name="price">The price of the product.</param>
    public Product(string name, string description, float price)
    {
        this.name = name;
        this.description = description;
        this.price = price >= 0 ? price : 0;
    }
}