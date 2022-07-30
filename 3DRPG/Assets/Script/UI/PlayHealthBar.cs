using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayHealthBar : MonoBehaviour
{
    Text level;
    Image healthSlider;
    Image expSlider;

    private void Awake()
    {
        level = transform.GetChild(2).GetComponent<Text>();
        healthSlider = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        expSlider = transform.GetChild(1).GetChild(0).GetComponent<Image>();
    }

    private void LateUpdate()
    {
        level.text = "Level  " + GameManager.Instance.playerStats.character.currentLevel.ToString("00");

        UpdateHealth();

        UpdateExp();
    }

    void UpdateHealth()
    {
        float sliderPercent = (float)GameManager.Instance.playerStats.CurrentHealth / GameManager.Instance.playerStats.MaxHealth;
        healthSlider.fillAmount = sliderPercent;
    }
    void UpdateExp()
    {
        float sliderPercent = (float)GameManager.Instance.playerStats.character.currentExp / GameManager.Instance.playerStats.character.baseExp;
        expSlider.fillAmount = sliderPercent;
    }
}
