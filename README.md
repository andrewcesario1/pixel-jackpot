# Pixel Jackpot

[![Game Preview](https://img.youtube.com/vi/ulbLJVsZhzo/maxresdefault.jpg)](https://youtu.be/ulbLJVsZhzo)

A 2D pixel art casino game built with Unity. Players explore a casino environment, play classic casino games including slots, blackjack, and roulette, complete objectives, and manage their money to progress through the game.

## Demo

[![Game Preview](https://img.youtube.com/vi/ulbLJVsZhzo/maxresdefault.jpg)](https://youtu.be/ulbLJVsZhzo)

## What It Is

Pixel Jackpot is a top-down 2D casino game where you control a character exploring a multi-room casino. The game features three main casino games (slot machines, blackjack, and roulette), an objective system that guides gameplay, NPC interactions, and a money management system. Players can purchase VIP access, use power-up items, and save their progress.

## Tech Stack

- Unity 2022.3.45f1 (LTS)
- C# for scripting
- TextMeshPro 3.0.6 for UI text
- Cinemachine 2.10.1 for camera control
- Unity 2D Tilemap for level design
- Unity UI (uGUI) for interface elements

## Requirements

To run this project, you need:
- Unity Editor 2022.3.45f1 (LTS)
- Unity Hub (latest version)

## How to Run

1. Clone the repository
2. Open Unity Hub and add the `32BitCasino` folder as a project
3. Open the project in Unity Editor
4. Open a scene from `Assets/Scenes/` (Main Menu or gameScene)
5. Press Play in the Unity Editor

Controls:
- WASD or Arrow Keys: Move player
- Space: Interact with objects/NPCs
- ESC: Pause menu

## Project Structure

```
32BitCasino/
├── Assets/
│   ├── Scenes/              # Main Menu, gameScene, Black Jack
│   ├── Scripts/             # C# game scripts
│   │   ├── BlackJackScripts/
│   │   ├── RouletteScripts/
│   │   ├── SlotScripts/
│   │   ├── MenuScripts/
│   │   ├── NPCGame/
│   │   └── Challenges/
│   ├── Sprites/              # 2D sprites and textures
│   ├── Tiles/                # Tilemap assets
│   ├── UI/                   # UI elements and animations
│   ├── Music/                # Background music
│   ├── Sound Effects/        # Audio effects
│   └── TextMesh Pro/         # TextMeshPro assets
├── ProjectSettings/          # Unity project settings
└── Packages/                 # Package dependencies
```

## References

- [Unity Documentation](https://docs.unity3d.com/)
- [TextMeshPro Documentation](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html)
- [Cinemachine Documentation](https://docs.unity3d.com/Packages/com.unity.cinemachine@2.10/manual/index.html)
- [Unity 2D Tilemap](https://docs.unity3d.com/Manual/class-Tilemap.html)
- [Unity UI System](https://docs.unity3d.com/Manual/UISystem.html)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

Note: This project was created as an educational assignment. See `Project Documentation.pdf` for detailed project documentation and goals.
