using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private PlayerHealthManager playerHealthManager;
    public TextMeshProUGUI currentHPText,maxHPText;

    private void LateUpdate()
    {
        float healthPercent = (float)playerHealthManager.currentHealth / playerHealthManager.startingHealth;
        healthBarFillImage.fillAmount = healthPercent;
    }

    private void Update()
    {
        currentHPText.text = playerHealthManager.currentHealth.ToString();
        maxHPText.text = playerHealthManager.startingHealth.ToString();
    }
}
