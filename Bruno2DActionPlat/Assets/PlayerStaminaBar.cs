using Platformer2D.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    [SerializeField] private Image staminaBarFillImage;
    [SerializeField] private PlayerAimWeapon playerStaminaManager;


    private void LateUpdate()
    {
        float healthPercent = (float)playerStaminaManager.currentStamina / playerStaminaManager.StartingStamina;
        staminaBarFillImage.fillAmount = healthPercent;
    }
}
