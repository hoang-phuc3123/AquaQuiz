# Aqua Quiz - Unity learning project at FPT University

A relaxing fishing game prototype inspired by the charming world of *Stardew Valley*, with a unique twist: instead of a test of reflexes, catching a fish is a test of your knowledge!

---

### **IMPORTANT ASSET INFORMATION**

The visual assets used in this project (including sprites for the player character, tools, fish, UI elements, etc.) are from the game **Stardew Valley**, created by **ConcernedApe (Eric Barone)**.

This project is a **non-commercial, educational prototype** created for learning and demonstration purposes. The assets are the property of their respective owner and are not intended for use in any commercial product. If you enjoy the art style, please support the original creator by purchasing *Stardew Valley*.

---

## Gameplay

Explore the map as a pixelated angler. When you find a suitable spot, cast your line and wait for a bite. But be ready! Once a fish is on the line, a quiz-based minigame will pop up.

A progress bar will appear, which slowly ticks down over time. You'll be presented with a general knowledge question about fish, fishing, or aquatic life.

*   **Answer Correctly:** The progress bar gets a significant boost!
*   **Answer Incorrectly:** The progress bar takes a hit.
*   **Fill the Bar:** Get the progress bar to 100% to successfully catch the fish.
*   **Let it Deplete:** If the bar drops to 0%, the fish gets away!

Every fish you catch is added to your persistent **Fish Collection**, where you can view your prized catches and track your progress.

## Key Features

*   **Quiz-Based Fishing:** A unique spin on the classic fishing minigame. Instead of chasing a bar, you answer trivia questions to reel in your catch.
*   **Dynamic Progress Bar:** A race against time and your own knowledge. The constantly decreasing bar adds a fun layer of pressure to each quiz.
*   **Persistent Fish Collection:** All caught fish are saved to a collection that persists across scenes and game sessions. Initially, all fish appear as black silhouettes, revealing their colorful sprites only after being caught for the first time.
*   **Interactive UI:**
    *   Press 'I' to open and close the collection from any scene.
    *   Click on a caught fish in the grid to view detailed information about it.
    *   The player's input is disabled during the minigame and while viewing the collection for a seamless experience.
*   **Expandable System:** The game is built using Scriptable Objects (`FishData`), making it incredibly easy for developers to add new fish, sprites, names, descriptions, and trivia questions without changing any core code.

## Technical Implementation

This prototype is built in Unity and demonstrates several key concepts:

*   **Singleton Pattern (`FishCollection.cs`):** The fish collection manager is implemented as a persistent Singleton (`DontDestroyOnLoad`) to ensure that player catch data is maintained across all scenes.
*   **Scriptable Objects (`FishData.cs`):** Each fish is a `FishData` Scriptable Object, containing all its unique information (name, sprite, description). This decouples the game's data from its logic.
*   **UI Management:** The game features multiple UI canvases that are enabled and disabled dynamically, including the fishing minigame, the fish collection grid, and the detailed fish information panel.
*   **Unity's Input System:** Player input for movement and interacting with UI elements is handled through Unity's modern Input System.
*   **Dynamic UI Generation:** The fish collection grid is populated dynamically at runtime by instantiating prefabs for each fish defined in the `FishData` list.
*   **Event-Driven UI:** Clicks on the fish grid items are handled via `EventTrigger` components, which are added and configured at runtime to ensure proper references.

## How to Set Up in Unity

1.  Open the project in the Unity Hub.
2.  Open the main game scene (e.g., `SceneA`).
3.  **Key GameObjects:**
    *   **Player:** Contains the `PlayerController.cs` script for movement and input.
    *   **FishCollection:** A persistent GameObject with the `FishCollection.cs` script. This is where you assign the list of all `FishData` assets and the UI prefabs.
    *   **FishingManager:** A persistent GameObject that controls the logic of the fishing minigame itself.
    *   **UI Canvases:** The scene contains canvases for the Collection UI and the Fish Info Display.
4.  **Adding a New Fish:**
    *   In the Project window, right-click -> `Create` -> `Fish Data`.
    *   Fill in the new asset with the fish's name, sprite, and description.
    *   Drag this new `FishData` asset into the `Fish Data List` on the `FishCollection` GameObject's Inspector.
    *   Add a new question to the `QuestionGenerator` for this fish. The collection will update automatically
