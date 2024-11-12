using UnityEngine;

/// <summary>
/// Represents a product object in the scene that can be initialized with product data,
/// manages its display color, and interfaces with the UIManager to display and update product information.
/// </summary>
public class ProductObject : MonoBehaviour
{
    [Tooltip("MeshRenderer component used to display the product's color")]
    [SerializeField] private MeshRenderer meshRenderer; 
    [Tooltip("Reference to the UIManager for managing UI interactions")]
    [SerializeField] private UIManager UIManager; 

    private Product productData; 

    /// <summary>
    /// Initializes the product object with specific product data, sets up the UI display,
    /// and applies a random color to differentiate it visually in the scene.
    /// </summary>
    /// <param name="product">The product data used for initialization.</param>
    public void Init(Product product)
    {
        if (!UIManager)
        {
            Debug.LogWarning("UIManager is not assigned.");
            return;
        }

        productData = product;
        UIManager.InitializeUI(product, UpdateData);
        meshRenderer.material.color = GetRandomColor();
    }

    /// <summary>
    /// Generates a random color for the product's visual representation.
    /// </summary>
    /// <returns>A randomly generated Color.</returns>
    private Color GetRandomColor()
    {
        return Random.ColorHSV();
    }

    /// <summary>
    /// Updates the product data with new information and refreshes the UI display.
    /// This method serves as a callback for UI updates.
    /// </summary>
    /// <param name="newProductData">The updated product data.</param>
    private void UpdateData(Product newProductData)
    {
        // Update local product data and refresh UI display with updated product data
        productData = newProductData;
        UIManager.UpdateText(productData);
    }
}
