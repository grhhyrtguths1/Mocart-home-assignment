using System;
using UnityEngine;

/// <summary>
/// InteractableObject class allows an object to be hovered over, clicked to open a details panel, 
/// and revert visual changes upon unhovering. Each instance of this class uses an Action to close 
/// any previously opened panels, ensuring only one panel is open at a time.
/// </summary>
public class InteractableObject : MonoBehaviour
{
    [Tooltip(" UI panel to display details of this product")]
    [SerializeField] private GameObject productDetailsPanel;
    [Tooltip("Material to display when the object is hovered over")]
    [SerializeField] private Material hoverMaterial;
    [Tooltip("MeshRenderer component of the object")]
    [SerializeField] private MeshRenderer meshRenderer;
    
    private Material defaultMaterial;
    private static Action openProductDetailsPanel;

    /// <summary>
    /// Initializes the default material and subscribes the ClosePanel method to the static Action.
    /// </summary>
    private void Start()
    {
        // Add ClosePanel to the Action, so it closes this panel if another one is opened
        openProductDetailsPanel += ClosePanel;
        productDetailsPanel.SetActive(false);
        defaultMaterial = meshRenderer.material;
    }

    /// <summary>
    /// Called when the mouse enters the object's collider. Changes the material to the hover material.
    /// </summary>
    private void OnMouseEnter()
    {
        meshRenderer.material = hoverMaterial;
    }

    /// <summary>
    /// Called when the mouse exits the object's collider. Reverts the material to the default material.
    /// </summary>
    private void OnMouseExit()
    {
        meshRenderer.material = defaultMaterial;
    }

    /// <summary>
    /// Closes the product details panel. Subscribed to the static Action, so it can be closed by any instance.
    /// </summary>
    private void ClosePanel()
    {
        productDetailsPanel.SetActive(false);
    }

    /// <summary>
    /// Called when the object is clicked. Opens the product details panel and triggers the static Action 
    /// to close any previously opened panels.
    /// </summary>
    private void OnMouseDown()
    {
        // Close any other open panels by invoking the Action and open this object's details panel
        openProductDetailsPanel?.Invoke();
        productDetailsPanel.SetActive(true);
    }
}
