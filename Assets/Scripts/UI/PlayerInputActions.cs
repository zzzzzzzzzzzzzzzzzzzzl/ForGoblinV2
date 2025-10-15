using UnityEngine;
using UnityEngine.InputSystem;
class PlayerInputActions : MonoBehaviour
{
    public InputAction keydown1;
    private PlayerControls playerControls;
    public playerManager playerManager;
    void Start()
    {
        playerControls = new PlayerControls();
        playerControls.GamePlay.Enable();
        playerControls.GamePlay.SpawnSwordsMan.performed += c => playerManager.spawner.spawnUnit("Swordsman");
        playerControls.GamePlay.SpawnArcher.performed += c => playerManager.spawner.spawnUnit("Archer");
        playerControls.GamePlay.SpawnHealer.performed += c => playerManager.spawner.spawnUnit("Healer");
        playerControls.GamePlay.SpawnMacer.performed += c => playerManager.spawner.spawnUnit("Macer");
        playerControls.GamePlay.SpawnMagi.performed += c => playerManager.spawner.spawnUnit("Magi");
        playerControls.GamePlay.SpawnSpearer.performed += c => playerManager.spawner.spawnUnit("Spearer");
        playerControls.GamePlay.SpawnShielder.performed += c => playerManager.spawner.spawnUnit("Shielder");
        playerControls.GamePlay.SpawnSummoner.performed += c => playerManager.spawner.spawnUnit("Summoner");
        playerControls.GamePlay.SpawnMushroomer.performed += c => playerManager.spawner.spawnUnit("Sporager");
    }
}