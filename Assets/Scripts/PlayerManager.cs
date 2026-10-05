using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private MovementController movementController;
    [SerializeField] private Timer timer;
    [SerializeField] private hpBar hpbar;
    [SerializeField] private LaserSpawnController laserSpawnController;
    
    private bool gameRunning = false;

    public void UpdateHPUI(int hp)
    {
        hpbar.UpdateHP(hp);
    }

    public void Start()
    {
        Pause();
    }

    public void Pause()
    {
        timer.StopTimer();
        laserSpawnController.StopLaserSpawn();
        gameRunning = false;
        Debug.Log("Game Status Paused");
    }

    public void Die()
    {
        movementController.ResetPosition();
        playerStats.ResetHP();
        Pause();
        Debug.Log("You died. :(");
    }

    public void PlayerMovedAction()
    {
        if (!gameRunning)
        {
            gameRunning=true;
            timer.StartTimer();
            laserSpawnController.StartLaserSpawn();
        }
    }
}