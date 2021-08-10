using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private PlayerHealthManager playerHealthManager;


    private void LateUpdate()
    {
        float healthPercent = (float)playerHealthManager.currentHealth / playerHealthManager.startingHealth;
        healthBarFillImage.fillAmount = healthPercent;
    }
}
