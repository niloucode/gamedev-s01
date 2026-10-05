using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int hp = 10;
    [SerializeField] private PlayerManager playerManager;

    private void Start()
    {
        ResetHP();
    }

    public void ResetHP()
    {
        hp = 10;
        playerManager.UpdateHPUI(hp);
    }

    private void UpdateHP(int change)
    {
        if ((hp+change)<=10)
        {
            hp += change;
            playerManager.UpdateHPUI(hp);

            if (hp <= 0)
            {
                playerManager.Die();
            }
        }
    }

    public void DecrementHP(int decrement)
    {
        UpdateHP(-decrement);
    }

    public void IncrementHP(int increment)
    {
        UpdateHP(increment);
    }
}