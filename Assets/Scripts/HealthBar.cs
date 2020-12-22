using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider _slider;

    public void SetMaxHealth(int _currentHealth)
    {
        _slider.maxValue = _currentHealth;
        _slider.value = _currentHealth;
    }

    public void SetHealth(int _currentHealth)
    {
        _slider.value = _currentHealth;
    }
}
