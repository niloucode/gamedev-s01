using UnityEngine;
using UnityEngine.UI;

public class hpBar : MonoBehaviour
{
    [SerializeField] Slider hpSlider;

    public void UpdateHP(int hp)
    {
        hpSlider.value = hp;
    }
}
