# 🎮 RogueHH
![MainMenu](https://github.com/user-attachments/assets/237c9bf4-3e3e-4938-8478-ce9c489c5092)

A self-make **roguelike turn-based strategy** game with an **isometric perspective**. Battle through **infinite floors** filled with increasingly powerful enemies, strategize your movements, and upgrade your character to survive the depths!


⚔️ Core Gameplay
- **Dynamic Leveling System:** Gain experience, level up your character, and grow stronger during your run to keep up with the dungeon's dangers.
- **Infinite Scaling Difficulty:** Survive an endless descent. The higher the floor you reach, the tougher and more unforgiving the enemies become. 
- **Skill System:**
  - **Attack Skills:** Unleash active abilities to attack enemies.
  - **Tactical Buffs:** Activate buffs featuring managed **active durations and cooldowns**.
- **Pre-Game Upgrade System:** Use your hard-earned resources to upgrade your base character stats before starting a new run.

### 🎲 World & Mechanics
- **Procedural Floor Generation:** Explore endlessly generated dungeon floors. No two runs are ever the exact same.
- **Advanced Tile-Based Movement:**
  - Click-based navigation utilizing **A\* pathfinding** for precise and optimal player movement.  
  - Smart AI: Enemies use a **custom A\* algorithm** to dynamically find alternative paths when their primary route is blocked.
- **Isometric Perspective:** A clean, classic isometric view optimized with visual and performance enhancements.

## 🎭 Game Architecture & Design Patterns
This game heavily utilizes Scriptable Objects and the Event Bus Pattern to create a modular and scalable system:
- **Scriptable Objects** for flexible game architecture.
- **Data Containers** to manage game data efficiently.
- **Event Handling** with **Channels** for cleaner communication between systems.
- **State Machine** to manage player and enemy behaviors.
- **Observer Pattern** integrated into event channels.


## 🎥 Screenshots / Gameplay Demo
### Main Menu
![image](https://github.com/user-attachments/assets/f0676aa2-5cc2-4b81-8b5a-0e232b63ace2)

### Upgrade Menu
![UpgradeMenu](https://github.com/user-attachments/assets/4ca13f5a-2f44-48a6-ba8a-39aa4f926dfb)

### Gameplay
![Gameplay1](https://github.com/user-attachments/assets/9e459e9c-d768-4e00-9b0b-9b40b622c529)
![Gameplay2](https://github.com/user-attachments/assets/d1ec2813-64e3-4bf4-9f3a-08167eb5bb9c)
![Gameplay3](https://github.com/user-attachments/assets/5f9e9585-9741-464e-bd9f-70890d097579)

