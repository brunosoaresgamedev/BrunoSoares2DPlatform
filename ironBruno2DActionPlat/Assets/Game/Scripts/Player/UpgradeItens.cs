using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItens : MonoBehaviour
{
    [SerializeField]
    WeaponCostumize weaponCostumize;
    [SerializeField]
    int OreToNextLevelWeaponType0, OreToNextLevelWeaponType1, OreToNextLevelWeaponType2;

    private void Awake()
    {

    }

    private void Update()
    {
        OreToNextLevelWeaponType0 = 100 * GameManager.instance.WeaponLevel0;
        OreToNextLevelWeaponType1 = 100 * GameManager.instance.WeaponLevel1;
        OreToNextLevelWeaponType2 = 100 * GameManager.instance.WeaponLevel2;
    }
    public void UpgradeWeaponButton()
    {
        if (GameManager.instance.IronOre >= OreToNextLevelWeaponType0 && GameManager.instance.WeaponType == 0)
        {

            GameManager.instance.WeaponLevel0++;
            GameManager.instance.IronOre -= OreToNextLevelWeaponType0;
            weaponCostumize.skinAimNr++;
        }
        
    }
}
