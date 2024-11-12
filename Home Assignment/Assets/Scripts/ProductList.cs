using System.Collections.Generic;

/// <summary>
/// Represents a collection of <see cref="Product"/> objects.
/// This class is designed to store a list of products and is marked as serializable 
/// to facilitate JSON deserialization and compatibility with Unity's Inspector.
/// </summary>
[System.Serializable]
public class ProductList
{
    public List<Product> products;
}