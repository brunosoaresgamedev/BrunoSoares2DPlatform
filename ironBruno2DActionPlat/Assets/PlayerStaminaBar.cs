using Platformer2D.Character;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    [SerializeField] private Image staminaBarFillImage;
    [SerializeField] private PlayerStaminaManager playerStaminaManager;
    public TextMeshProUGUI currentStamText, maxStamText;

    private void LateUpdate()
    {
        float healthPercent = (float)playerStaminaManager.currentStamina / playerStaminaManager.startingStamina;
        staminaBarFillImage.fillAmount = healthPercent;
    }
    private void Update()
    {
        currentStamText.text = playerStaminaManager.currentStamina.ToString();
        maxStamText.text = playerStaminaManager.startingStamina.ToString();
    }
}
