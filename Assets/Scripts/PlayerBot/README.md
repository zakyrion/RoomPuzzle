# PlayerBot System

A complete AI bot controller for your RoomPuzzle player character.

## Features
- Automated movement with random patterns
- Target seeking behavior
- Stuck detection and recovery
- Configurable behavior parameters
- Easy toggle between manual and bot control
- Optional UI for runtime control

## Quick Setup

1. Create PlayerBotConfig asset:
   - Right-click in Project > Create > RoomPuzzle > PlayerBot > Config
   - Configure behavior parameters

2. Add PlayerBotInstaller to scene:
   - Create empty GameObject
   - Add PlayerBotInstaller component
   - Assign the config asset

3. Test with PlayerBotDemo:
   - Add PlayerBotDemo script to any GameObject
   - Press B to toggle bot, T for target seeking

## Architecture

Follows MVP pattern with:
- **Model**: AI logic and behavior state
- **Presenter**: Orchestrates bot with player input
- **Config**: ScriptableObject for settings
- **UI**: Optional runtime controls

## Integration

The bot works by:
1. Generating movement decisions in PlayerBotModel
2. Sending input commands through PlayerInputModel
3. Using existing movement and physics systems

## Customization

Extend FindNearestTarget() for pickup item integration.
Modify behavior states for game-specific AI patterns.
Adjust configuration parameters for different bot personalities.

## Controls (Demo)
- B: Toggle bot on/off
- T: Toggle target seeking
- R: Reset behavior
- G: Set random target