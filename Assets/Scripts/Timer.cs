using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime;
    [SerializeField] private PlayerManager playerManager;

    private bool running = false;

    private float currentTime;

    public void StartTimer()
    {
        currentTime = remainingTime;
        running = true;
    }

    public void StopTimer()
    {
        running = false;
        currentTime = remainingTime;
    }

    void Update()
    {
        if (running)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else if (currentTime < 0)
            {
                currentTime = 0;
                playerManager.Die();
            }
        }
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
}
