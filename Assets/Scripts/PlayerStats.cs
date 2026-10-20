
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int hp = 10;
    [SerializeField] private PlayerManager playerManager;

    private bool deathTriggered;

    private void Start()
    {
        ResetHP();
    }

    public void ResetHP()
    {
        hp = 10;
        deathTriggered = false;

        if (playerManager != null)
            playerManager.UpdateHPUI(hp);
    }

    private void UpdateHP(int change)
    {
        if (deathTriggered)
            return;

        int newHP = Mathf.Clamp(hp + change, 0, 10);

        if (newHP == hp)
            return;

        hp = newHP;

        if (playerManager != null)
            playerManager.UpdateHPUI(hp);

        if (hp <= 0)
        {
            // Set this before calling Die(), which resets HP synchronously.
            deathTriggered = true;

            if (playerManager != null)
                playerManager.Die();
        }
    }

    public void DecrementHP(int decrement)
    {
        if (decrement > 0)
            UpdateHP(-decrement);
    }

    public void IncrementHP(int increment)
    {
        if (increment > 0)
            UpdateHP(increment);
    }
}
