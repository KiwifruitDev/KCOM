# KCOM v4

process of how multiplayer works

## gamemodes
- co-op campaign
- free mode
  - select any map without story progression

## syncing
- options file that supports configuring the following between all players
- different files for gamemodes (co-op campaign, free mode)
- settings cannot be changed during gameplay
- select:
  - players
    - head (headset model that doesn't actually appear for the other player)
    - hands (glove models that float in mid-air, may sync fingers in the future)
  - gameplay
    - ammo
      - if a player picks up ammo, it will be removed and all players will receive the ammo
    - resin
      - select between multiplying by the number of players or having a shared pool
    - weapons
    - weapon upgrades
      - resin is limited if a shared pool is selected, so not all players can upgrade their weapons
      - weapon upgrades can be per-player or shared between all players
    - respawning players
      - if a player dies, they will respawn at the last checkpoint
      - otherwise, they will enter a spectator mode
  - enemies
    - damage and state
      - if an enemy is defeated by a player, it will be ragdolled for all players
      - positions for the enemies will not be synced to avoid issues

## co-op campaign
- new campaign
  - host selects a chapter
  - lobby map is loaded and players can connect
  - when all players have joined, the host can start the chapter
  - chapter map loads and then current resin is multiplied if enabled
  - players can progress through the chapter together
  - when the map ends, a countdown will start and all players are frozen
  - events
    - player death will either respawn the player or enter spectator mode
    - player join/disconnect will be announced to all players
    - if a player gets too far ahead and gets into an area that can't be reached by the other player, all players will be teleported
    - saving
      - host can save the game at any time
      - clients cannot save the game
      - steamid is used to identify all players, their progress is also saved
      - when a new player joins, the host's save is loaded for them
    - checkpoints
      - autosaves from the game itself will count as checkpoints
      - all players are teleported here immediately
- load save
  - host selects a save

## free mode
- host selects a map
- lobby map is loaded and players can connect
- when all players have joined, the host can start the map
