using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private EnemyHealthManager enemyHealthManager;


    private void LateUpdate()
    {
        float healthPercent = (float)enemyHealthManager.currentHealth / enemyHealthManager.startingHealth;
        healthBarFillImage.fillAmount = healthPercent;
    }
}
