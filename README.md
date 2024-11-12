### WEB BUILD ###
https://play.unity.com/en/games/02c7ed1c-7908-489f-a060-a7ea39cfb40c/adasd

### RUN LOCALLY ###
The windows build exe is in the path
Mocart-home-assignment\Builds -> Home Assignment

### MORE INFO ###
No external libraries were used, except the sprites for the background and the wood texture

### CODE ###

DataLoader.cs: Handles fetching product data from an API and spawns product objects in the scene.
The script contains serialized fields to set the API URL and the prefab used for each product,
making it adaptable to different APIs and prefabs with minor code adjustments.

InteractableObject.cs: Adds interactivity to objects, allowing them to be hovered over,
clicked to open a details panel.
It uses an Action to close previously opened panels, ensuring only one panel remains open,
enhancing user experience.

Product.cs: Represents a product with the rquired properties: name, description, and price.
This class is marked [System.Serializable] for easy serialization, 
particularly for JSON deserialization.

ProductList.cs: Defines a serializable list of Product objects,
used to store and manage multiple products. This class is also designed for compatibility with JSON,
facilitating bulk data operations like loading product arrays from the API.

ProductObject.cs: Represents individual products in the scene with methods for
initialization based on product data.
This script manages color display and interfaces with the UIManager to update
and display product information dynamically.

UI Manager.cs: Manages the UI for displaying and editing product details.
It allows viewing and saving product information in the ProductObject.
This modular structure aids in separating UI logic from data handling, improving maintainability.
