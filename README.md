# Pixel Jackpot

<video src="GamePreview.mp4" width="800" controls></video>

A 2D pixel art casino game built with Unity, featuring multiple classic casino games, an objective system, and an interactive casino environment.

## 🎮 Demo

<video src="GamePreview.mp4" width="800" controls></video>

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technologies](#technologies)
- [Requirements](#requirements)
- [Installation](#installation)
- [How to Run](#how-to-run)
- [Game Features](#game-features)
- [Project Structure](#project-structure)
- [References](#references)
- [License](#license)

## 🎯 Overview

Pixel Jackpot is a 2D top-down casino game where players explore a casino, play various games, complete objectives, and manage their money. The game features a pixel art aesthetic with smooth animations and an immersive casino atmosphere.

## ✨ Features

### Casino Games
- **Slot Machines**: Multiple slot machines with different betting options, including a free play machine
- **Blackjack**: Full blackjack implementation with proper card dealing, ace handling, and betting system
- **Roulette**: European roulette with betting on colors (red/black/green), number ranges, and payout calculations

### Game Systems
- **Money Management**: Earn and spend money across different games
- **Objective System**: Complete objectives to earn rewards and progress through the game
- **VIP Access**: Purchase VIP access to unlock exclusive areas
- **NPC Interactions**: Talk to NPCs for information and quests
- **Item System**: Power-up items including:
  - Cost Reduction: Reduces game costs for a limited time
  - Speed Boost: Increases player movement speed
  - Boosted Winnings: Increases payout multipliers
- **Save System**: Save and load game progress
- **Tutorial System**: Interactive tutorial for new players

### Gameplay
- **Top-Down Movement**: Smooth 2D character movement with directional animations
- **Interactive Environment**: Explore the casino and interact with various objects
- **Pause Menu**: Access settings, game info, and save functionality
- **Audio System**: Background music and sound effects for immersive gameplay

## 🛠 Technologies

- **Unity Engine**: 2022.3.45f1
- **C#**: Primary scripting language
- **TextMeshPro**: Advanced text rendering and UI
- **Cinemachine**: Camera system for smooth following
- **Unity 2D**: Tilemap system for level design
- **Unity UI (uGUI)**: User interface system

### Key Packages
- TextMeshPro 3.0.6
- Cinemachine 2.10.1
- Unity 2D Feature Set 2.0.1
- Unity Timeline 1.7.6

## 📦 Requirements

### Minimum System Requirements
- **OS**: Windows 10/11, macOS 10.14+, or Linux
- **CPU**: 2.0 GHz dual-core processor
- **RAM**: 4 GB
- **Graphics**: DirectX 11 compatible graphics card
- **Storage**: 500 MB available space

### Development Requirements
- **Unity Hub**: Latest version
- **Unity Editor**: 2022.3.45f1 (LTS)
- **Git**: For version control (optional)

## 🚀 Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/pixel-jackpot.git
   cd pixel-jackpot
   ```

2. **Open in Unity**
   - Open Unity Hub
   - Click "Add" and select the `32BitCasino` folder
   - Ensure Unity 2022.3.45f1 (LTS) is installed
   - Open the project

3. **Wait for Unity to import assets**
   - Unity will automatically import all assets and generate necessary files
   - This may take a few minutes on first launch

## 🎮 How to Run

### Running in Unity Editor

1. Open the project in Unity Editor
2. Navigate to `Assets/Scenes/` in the Project window
3. Open `Main Menu.unity` or `gameScene.unity`
4. Click the Play button in the Unity Editor
5. Use the following controls:
   - **WASD** or **Arrow Keys**: Move player
   - **Space**: Interact with objects/NPCs
   - **ESC**: Pause menu

### Building the Game

1. Go to **File > Build Settings**
2. Select your target platform (Windows, Mac, Linux)
3. Click **Build** and choose a destination folder
4. Run the executable from the build folder

## 🎲 Game Features

### Main Menu
- Start new game
- Load saved game
- Settings (audio, graphics)
- Game information

### Casino Environment
- Multiple floors and rooms to explore
- Interactive slot machines
- Blackjack tables
- Roulette wheel
- VIP area (requires purchase)
- NPCs to interact with
- Cashier for money management

### Objectives
- Complete objectives to earn money
- Progressive objective system
- Visual feedback for completed objectives

### Items & Power-ups
- Purchase items from the shop
- Temporary boosts to enhance gameplay
- Visual indicators for active items

## 📁 Project Structure

```
32BitCasino/
├── Assets/
│   ├── Scenes/           # Game scenes (Main Menu, Game Scene, Blackjack)
│   ├── Scripts/          # C# scripts
│   │   ├── BlackJackScripts/
│   │   ├── RouletteScripts/
│   │   ├── SlotScripts/
│   │   ├── MenuScripts/
│   │   ├── NPCGame/
│   │   └── Challenges/
│   ├── Sprites/          # 2D sprites and textures
│   ├── Tiles/            # Tilemap assets
│   ├── UI/               # UI elements and animations
│   ├── Music/            # Background music
│   ├── Sound Effects/    # Audio effects
│   └── TextMesh Pro/     # TextMeshPro assets
├── ProjectSettings/      # Unity project settings
└── Packages/            # Package dependencies
```

## 📚 References

- [Unity Documentation](https://docs.unity3d.com/)
- [TextMeshPro Documentation](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html)
- [Cinemachine Documentation](https://docs.unity3d.com/Packages/com.unity.cinemachine@2.10/manual/index.html)
- [Unity 2D Tilemap](https://docs.unity3d.com/Manual/class-Tilemap.html)
- [Unity UI System](https://docs.unity3d.com/Manual/UISystem.html)

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

**Note**: This project was created as an educational assignment. See `Assignment 4 Documentation.pdf` for detailed project documentation and goals.

