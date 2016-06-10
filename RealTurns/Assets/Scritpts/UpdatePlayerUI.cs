using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdatePlayerUI : MonoBehaviour
{
    public Slider healthSlider;

    public void SetMaxHealth(float max)
    {
        healthSlider.maxValue = max;
    }
    public void SetHealth(float value)
    {
        healthSlider.value = value;
    }


}
