using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItens : MonoBehaviour
{
    [SerializeField]
    WeaponCostumize weaponCostumize;

    int OreToNextLevel;

    bool GreatSwordT1Plus0;
    private void Awake()
    {

        
    }
    void ButtonClicked()
    {
        

    }
    private void Update()
    {
        OreToNextLevel = 100 * GameManager.instance.WeaponLevel;
    }
    public void UpgradeWeaponButton()
    {
        if (GameManager.instance.IronOre >= OreToNextLevel && GameManager.instance.WeaponType == 0 && GameManager.instance.WeaponTier == 1)
        {

            GameManager.instance.WeaponLevel++;
            GameManager.instance.IronOre -= OreToNextLevel;
        }
        
    }
}
