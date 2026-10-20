
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private enum RoundState
    {
        WaitingForDetection,
        Running,
        Dying,
        Resetting
    }

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private MovementController movementController;
    [SerializeField] private Timer timer;
    [SerializeField] private hpBar hpbar;
    [SerializeField] private LaserSpawnController laserSpawnController;
    [SerializeField] private StealthManager stealthManager;

    private RoundState roundState = RoundState.WaitingForDetection;

    public bool gameRunning => roundState == RoundState.Running;
    public bool IsDead => roundState == RoundState.Dying ||
                          roundState == RoundState.Resetting;

    private void Start()
    {
        roundState = RoundState.WaitingForDetection;

        timer.StopTimer();
        laserSpawnController.StopLaserSpawn();
        stealthManager.ResetStealthVisuals();
    }

    public void UpdateHPUI(int hp)
    {
        if (hpbar != null)
            hpbar.UpdateHP(hp);
    }

    // Called when a valid detection disables stealth.
    public void Unstealth()
    {
        if (roundState != RoundState.WaitingForDetection)
            return;

        roundState = RoundState.Running;

        stealthManager.LeaveStealth();
        timer.StartTimer();
        laserSpawnController.StartLaserSpawn();

        Debug.Log("Round started.");
    }

    // Called when the player's HP reaches zero.
    public void Die()
    {
        // Set the state FIRST. All subsequent callbacks are rejected.
        if (roundState == RoundState.Dying ||
            roundState == RoundState.Resetting)
            return;

        roundState = RoundState.Dying;

        timer.StopTimer();
        laserSpawnController.StopLaserSpawn();

        // Reset visuals without allowing detection to start another round.
        stealthManager.ResetStealthVisuals();

        roundState = RoundState.Resetting;

        movementController.ResetPosition();
        playerStats.ResetHP();

        // Reset is complete. Wait for a fresh detection.
        roundState = RoundState.WaitingForDetection;

        Debug.Log("You died. Waiting for new detection.");
    }

    public bool CanReceiveDetection()
    {
        return roundState == RoundState.WaitingForDetection ||
               roundState == RoundState.Running;
    }
}
