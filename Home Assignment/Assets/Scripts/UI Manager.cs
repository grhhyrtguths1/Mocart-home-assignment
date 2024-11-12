using System;
using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the UI interactions for displaying and editing product details. 
/// Allows viewing, editing, and saving product information, and controls visibility 
/// and interactivity of UI components based on the edit mode.
/// </summary>
public class UIManager : MonoBehaviour
{ 
    [Header("Input Fields")]
    [Tooltip("Input field for editing the product name")]
    [SerializeField] private TMP_InputField nameEdit; 
    [Tooltip("Input field for editing the product description")]
    [SerializeField] private TMP_InputField descriptionEdit; 
    [Tooltip("Input field for editing the product price")]
    [SerializeField] private TMP_InputField priceEdit; 

    [Header("Text Fields")]
    [Tooltip("Text field for displaying the product name")]
    [SerializeField] private TMP_Text nameText; 
    [Tooltip("Text field for displaying the product price")]
    [SerializeField] private TMP_Text priceText; 
    [Tooltip("Text field for displaying the product description")]
    [SerializeField] private TMP_Text descriptionText; 

    [Header("Buttons")] 
    [Tooltip("Button to save changes")]
    [SerializeField] private GameObject saveButton; 
    [Tooltip("Button to enter edit mode")]
    [SerializeField] private GameObject editButton;
    [Tooltip("Button to cancel edit mode")]
    [SerializeField] private GameObject cancelButton; 
    
    [Header("Feedback Messages")]
    [Tooltip("Text field for displaying invalid price message")]
    [SerializeField] private GameObject invalidPriceMessage;
    [Tooltip("Text field for displaying changes saved message")]
    [SerializeField] private GameObject changesSavedMessage;
    
    private Action<Product> _onSave; 

    /// <summary>
    /// Initializes the UI with the specified product data and sets the save callback.
    /// </summary>
    /// <param name="product">The product to display and edit.</param>
    /// <param name="onSave">Callback to handle saving updated product data.</param>
    public void InitializeUI(Product product, Action<Product> onSave)
    { 
        _onSave = onSave;
        UpdateText(product);
    }

    /// <summary>
    /// Updates the displayed text fields with the current product data.
    /// </summary>
    /// <param name="product">The product data to display.</param>
    public void UpdateText(Product product)
    {
        nameText.text = product.name;
        priceText.text = product.price.ToString();
        descriptionText.text = product.description;
    }

    /// <summary>
    /// Closes the product details UI panel.
    /// </summary>
    public void OnExitClick()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Cancels any changes and exits edit mode, restoring the display-only view.
    /// </summary>
    public void OnCancelClick()
    {
        InEditMode(false);
        invalidPriceMessage.SetActive(false);
    }

    /// <summary>
    /// Saves the edited product data and invokes the save callback with the updated product.
    /// </summary>
    public void OnSaveClick()
    {
        bool changeMade = false;
        string name = nameText.text;
        string description = descriptionText.text;
        string price = priceText.text;

        // Override with any non-empty input field values
        if (!string.IsNullOrEmpty(nameEdit.text))
        {
            name = nameEdit.text;
            changeMade = true;
        }
        if (!string.IsNullOrEmpty(descriptionEdit.text))
        {
            description = descriptionEdit.text;
            changeMade = true;
        }
        if (!string.IsNullOrEmpty(priceEdit.text))
        {
            price = priceEdit.text;
            changeMade = true;
        }

        // Parse the price and invoke the onSave action with the updated product
        if (float.TryParse(price, out float parsedPrice) && parsedPrice >= 0)
        {
            invalidPriceMessage.SetActive(false);
            _onSave?.Invoke(new Product(name, description, parsedPrice));
            InEditMode(false);
            if (!changeMade) return;
            StartCoroutine(ShowSavedMessage());
        }
        else
        {
            invalidPriceMessage.SetActive(false);
            invalidPriceMessage.SetActive(true);
            Debug.LogWarning("Invalid price format. Could not save the product.");
        }
    }
    
    /// <summary>
    /// Enters edit mode, allowing the user to modify product details.
    /// </summary>
    public void OnEditClick()
    {
        ClearInputFields();
        InEditMode(true);
    }

    /// <summary>
    /// Disables the invalid price message and changes saved message when the UI is disabled.
    /// </summary>
    private void OnDisable()
    {
        invalidPriceMessage.SetActive(false);
        changesSavedMessage.SetActive(false);
    }

    /// <summary>
    ///  Displays a message to indicate that changes have been saved.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowSavedMessage()
    {
        changesSavedMessage.SetActive(true);
        yield return new WaitForSeconds(1f);
        changesSavedMessage.SetActive(false);
    }
    
    /// <summary>
    /// Toggles UI elements based on whether the UI is in edit mode or not.
    /// </summary>
    /// <param name="isInEditMode">True to enable edit mode; false to disable it.</param>
    private void InEditMode(bool isInEditMode)
    {
        editButton.SetActive(!isInEditMode);
        saveButton.SetActive(isInEditMode);
        cancelButton.SetActive(isInEditMode);
        nameEdit.gameObject.SetActive(isInEditMode);
        descriptionEdit.gameObject.SetActive(isInEditMode);
        priceEdit.gameObject.SetActive(isInEditMode);
    }

    /// <summary>
    /// Clears all input fields to ensure fresh values when entering edit mode.
    /// </summary>
    private void ClearInputFields()
    {
        nameEdit.text = "";
        descriptionEdit.text = "";
        priceEdit.text = "";
    }
}
