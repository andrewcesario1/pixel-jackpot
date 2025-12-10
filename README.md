# Pixel Jackpot

A 2D pixel art casino game built with Unity. Players explore a casino environment, play classic casino games including slots, blackjack, and roulette, complete objectives, and manage their money to progress through the game.

## Demo

[![Game Preview](https://img.youtube.com/vi/ulbLJVsZhzo/0.jpg)](https://youtu.be/ulbLJVsZhzo)

### Play Online
**Try the game yourself**: [Play Pixel Jackpot](https://play.unity.com/en/games/1872c98a-c89f-4805-b80f-324ab9f2b133/pixel-jackpot)

*Note: Go full screen for optimal UI experience*

## What It Is

Pixel Jackpot is a top-down 2D casino game where you control a character exploring a multi-room casino. The game features three main casino games (slot machines, blackjack, and roulette), an objective system that guides gameplay, NPC interactions, and a money management system. Players can purchase VIP access, use power-up items, and save their progress.

## Features

### Player Movement and Controls
The player character uses top-down movement with directional animations and sprite flipping based on movement direction. Movement can be disabled during interactions with game objects.

**Files:**
- `Assets/Scripts/PlayerController.cs` - Main player controller handling movement, animations, and player state

### Money System
Players start with $500 and can earn or lose money through various casino games. Money is displayed in the HUD and persists across game sessions through the save system.

**Files:**
- `Assets/Scripts/PlayerController.cs` - Money management (AddMoney, SubtractMoney methods)
- `Assets/Scripts/MoneyDisplay.cs` - UI display for player money

### Casino Games

#### Slot Machines
Three-reel slot machines with multiple symbols and payout multipliers. Features both paid and free play machines. Includes visual reel spinning animation and win detection for matching symbols (2 or 3 of a kind).

**Files:**
- `Assets/Scripts/SlotScripts/SlotMachineInteract.cs` - Interaction handling for slot machines
- `Assets/Scripts/SlotScripts/SlotMachineFreeInteract.cs` - Free play slot machine variant
- `Assets/Scripts/SlotScripts/ReelManager.cs` - Core slot machine logic, reel spinning, symbol matching, and payout calculation

#### Blackjack
Full blackjack implementation with card dealing, ace handling (1 or 11), hit/stand/double down options, and dealer AI. Players can place bets and win/lose based on standard blackjack rules.

**Files:**
- `Assets/Scripts/BlackJackScripts/BJInteract.cs` - Blackjack table interaction
- `Assets/Scripts/BlackJackScripts/GameManager.cs` - Main blackjack game logic, betting, and win/loss conditions
- `Assets/Scripts/BlackJackScripts/GameScript.cs` - Hand management and card value calculation
- `Assets/Scripts/BlackJackScripts/CardScript.cs` - Individual card behavior and value
- `Assets/Scripts/BlackJackScripts/deck.cs` - Card deck management and shuffling

#### Roulette
European roulette (0-36) with betting options for colors (red/black/green), number ranges (1st/2nd/3rd dozen), and payout calculations. Features spinning animation and visual feedback for winning bets.

**Files:**
- `Assets/Scripts/RouletteScripts/RouletteInteract.cs` - Roulette table interaction
- `Assets/Scripts/RouletteScripts/RouletteLogic.cs` - Roulette wheel logic, betting system, and payout calculations

### Objective System
Progressive objective system that guides players through the casino. Objectives include visiting specific locations, playing games, and purchasing items. Completing objectives rewards money and unlocks new objectives.

**Files:**
- `Assets/Scripts/Objectives.cs` - Objective data structure
- `Assets/Scripts/PlayerController.cs` - Objective tracking, completion, and UI updates

### NPC Interactions
NPCs throughout the casino provide information and quests. Players can interact with NPCs to receive objectives and dialogue.

**Files:**
- `Assets/Scripts/NPCGame/NPCInteract.cs` - NPC interaction handling
- `Assets/Scripts/NPCGame/CoinToss.cs` - Mini-game interaction with NPCs

### VIP Access System
Players can purchase VIP access to unlock exclusive areas of the casino. VIP status is saved and persists across sessions.

**Files:**
- `Assets/Scripts/VIPGateInteract.cs` - VIP gate interaction and access control
- `Assets/Scripts/PlayerController.cs` - VIP access state management

### Item System
Power-up items that provide temporary bonuses:
- Cost Reduction: Reduces game costs for 2 minutes
- Speed Boost: Increases player movement speed for 2 minutes
- Boosted Winnings: Increases payout multipliers for 2 minutes

Items display active timers in the UI and persist across game sessions.

**Files:**
- `Assets/Scripts/PlayerController.cs` - Item activation, timer management, and UI display

### Save System
Game progress is saved including player money, VIP access status, objective progress, active items, and player position. Data persists between game sessions using Unity's PlayerPrefs system.

**Files:**
- `Assets/Scripts/PlayerController.cs` - SaveData() and LoadData() methods
- `Assets/Scripts/Helper/DataSaving.cs` - Data serialization helpers
- `Assets/Scripts/MenuScripts/PauseManager.cs` - Save game functionality

### Menu System
Main menu with options to start new game, load saved game, adjust settings, and view game information. Pause menu accessible during gameplay.

**Files:**
- `Assets/Scripts/MenuScripts/MenuManager.cs` - Main menu navigation
- `Assets/Scripts/MenuScripts/PauseManager.cs` - Pause menu, save/load, and scene management
- `Assets/Scripts/MenuScripts/SettingsManager.cs` - Game settings management
- `Assets/Scripts/MenuScripts/GameInfoManager.cs` - Game information display

### Audio System
Separate audio managers for menu and gameplay with music and sound effect controls. Supports volume adjustment and sound effect playback for game events.

**Files:**
- `Assets/Scripts/MenuScripts/MenuAudioManager.cs` - Main menu audio
- `Assets/Scripts/MenuScripts/GameAudioManager.cs` - In-game audio and sound effects

### Cashier System
Cashier NPC allows players to manage their money, potentially exchange currency or view transaction history.

**Files:**
- `Assets/Scripts/CashierInteract.cs` - Cashier interaction system

### Challenge System
Framework for timed challenges that players can complete for rewards. Currently implemented but can be expanded with additional challenge types.

**Files:**
- `Assets/Scripts/Challenges/Challenge.cs` - Challenge data structure
- `Assets/Scripts/Challenges/ChallengeManager.cs` - Challenge management and completion tracking
- `Assets/Scripts/Challenges/ChallengeInteract.cs` - Challenge interaction points

### Interaction System
Base interactable system that all interactive objects inherit from, providing consistent interaction behavior across the game.

**Files:**
- `Assets/Scripts/Interactable.cs` - Base class for all interactable objects

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

## Assets Used

### Casino Tilemap
- [2D Top Down Pixel Art Tileset Casino](https://gamebetweenthelines.itch.io/2d-top-down-pixel-art-tileset-casino)

### Pixel Art Characters
- [Ninja Adventure Asset Pack](https://pixel-boy.itch.io/ninja-adventure-asset-pack)

### Music & Sound Effects
- Local Forecast - Elevator by Kevin MacLeod: [Local Forecast - Elevator](https://incompetech.com/music/royalty-free/music.html)
- Slot Machine Reel Spin: [Fruit Machine Sound Win or Spin Tone](https://www.zapsplat.com/music/fruit-machine-sound-win-or-spin-tone-2/)
- Deal Card: [Large Thick Playing Card Set Down Single Deal](https://www.zapsplat.com/music/large-thick-playing-card-set-down-single-deal-2/)
- Complete Objective: [Game Sound Award Hit Win Coins](https://www.zapsplat.com/music/game-sound-award-hit-win-coins/)
- Roulette Sound: Sounds Effects - Roulette game (Roleta jogo)
- Coin Toss Sound: Fear and Hunger Coin Flip/Toss Sound effect

## References

- [Unity Documentation](https://docs.unity3d.com/)
- [TextMeshPro Documentation](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html)
- [Cinemachine Documentation](https://docs.unity3d.com/Packages/com.unity.cinemachine@2.10/manual/index.html)
- [Unity 2D Tilemap](https://docs.unity3d.com/Manual/class-Tilemap.html)
- [Unity UI System](https://docs.unity3d.com/Manual/UISystem.html)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

Note: This project was created as an educational assignment. See `Project Documentation.pdf` for detailed project documentation and goals.
