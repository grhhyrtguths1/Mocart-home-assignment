using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// DataLoader class responsible for fetching product data from an API and spawning product objects in the scene.
/// </summary>
public class DataLoader : MonoBehaviour
{
    [Tooltip("URL to fetch product data")]
    [SerializeField] private string url = "https://homework.mocart.io/api/products";
    [Tooltip("Prefab to instantiate for each product")]
    [SerializeField] private ProductObject productPrefab;
    [Tooltip("Spacing between spawned product objects")]
    [SerializeField] private float itemSpacing = 2f;

    private ProductList productListContainer;
    private Vector3 spawnPosition = new(0, 0, 0);

    /// <summary>
    /// Start is called before the first frame update. Initiates the data fetching coroutine.
    /// </summary>
    private void Start()
    {
        StartCoroutine(GetDataFromAPI());
    }

    /// <summary>
    /// Coroutine to fetch product data from the specified API URL.
    /// </summary>
    /// <returns>IEnumerator for coroutine handling.</returns>
    private IEnumerator GetDataFromAPI()
    {
        using UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        // Check if the request was successful
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
            
            // Parse the JSON response into the ProductList container
            try
            {
                productListContainer = JsonUtility.FromJson<ProductList>(request.downloadHandler.text);
                if (productListContainer?.products == null || productListContainer.products.Count == 0)
                {
                    Debug.LogWarning("No products found in the data.");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Failed to parse JSON data: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("Error fetching data: " + request.error);
        }

        // Only create products if data was successfully parsed
        if (productListContainer is { products: not null })
        {
            CreateProducts();
        }
    }

    /// <summary>
    /// Creates and arranges ProductObject instances in the scene based on the fetched product data.
    /// </summary>
    private void CreateProducts()
    {
        if (productListContainer == null || productListContainer.products == null)
        {
            Debug.LogWarning("No product data available to create products.");
            return;
        }
        // Center products around spawnPosition     
        int productCount = productListContainer.products.Count;
        float totalWidth = (productCount - 1) * itemSpacing;
        Vector3 startPosition = spawnPosition - new Vector3(totalWidth / 2, 0, 0); 

        foreach (Product product in productListContainer.products)
        {
            ProductObject p = Instantiate(productPrefab, startPosition, Quaternion.identity);
            p.Init(product);
            startPosition.x += itemSpacing;
        }
    }
}
