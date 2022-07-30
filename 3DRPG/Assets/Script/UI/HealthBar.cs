using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public GameObject healthBarUiPerfab;

    public Transform barPoint;
    //�Ƿ�һֱ��ʾ ��ʾ��ʱ��
    public bool alwaysVisible;
    public float visibleTime;
    float timeLeft;

    Image healthSlider;
    Transform UIBar;
    Transform cam;
    CharacterStats currentStats;

    private void Awake()
    {
        //��ȡ���
        currentStats = GetComponent<CharacterStats>();
        currentStats.UpdateHealthBarOnAttack += UpdateHealthBar;
    }
    private void OnEnable()
    {
        cam = Camera.main.transform;
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                UIBar = Instantiate(healthBarUiPerfab, canvas.transform).transform;
                healthSlider = UIBar.GetChild(0).GetComponent<Image>();
                UIBar.gameObject.SetActive(alwaysVisible);
            }
        }

    }

    private void UpdateHealthBar(int currentHealth, int Maxhealth)
    {
        if (currentHealth <= 0)
            Destroy(UIBar.gameObject);

        UIBar.gameObject.SetActive(true);
        //����ʱ������ʾʣ��ʱ��
        timeLeft = visibleTime;
        float sliderPercent = (float)currentHealth / Maxhealth;
        healthSlider.fillAmount = sliderPercent;

    }
    private void LateUpdate()
    {
        if (UIBar != null)
        {
            //λ��
            UIBar.position = barPoint.position;
            //ui�ķ�����������ķ����෴
            UIBar.forward = -cam.forward;

            if (timeLeft <= 0f&&!alwaysVisible)
            {
                UIBar.gameObject.SetActive(false);
            }
            else
            {
                timeLeft -= Time.deltaTime;

            }
        }


    }
}
