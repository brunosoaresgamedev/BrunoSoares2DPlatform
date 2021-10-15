using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItens : MonoBehaviour
{
    [SerializeField]
    WeaponCostumize weaponCostumize;
    [SerializeField]
    int OreToNextLevelWeaponType0;

    [SerializeField]
    CustomizableCharacter customizableCharacter;
    [SerializeField]
    int OreToNextLevelArmor;

    [SerializeField]
    Text IronText1, IronText2;

    bool CanUpgrade = true;

    private void Awake()
    {

       

        GameObject ButtonGameobject = GameObject.Find("CanvasMenu");

      

        var buttonClava = ButtonGameobject.transform.GetChild(0).GetComponent<Button>();
        buttonClava.onClick.AddListener(delegate () { this.ButtonClavaClicked(); });

        var buttonDagger = ButtonGameobject.transform.GetChild(1).GetComponent<Button>();
        buttonDagger.onClick.AddListener(delegate () { this.ButtonDaggerClicked(); });
    }

    public void ButtonClavaClicked()
    {
        GameManager.instance.WeaponType = 0;

    }
    public void ButtonDaggerClicked()
    {
        GameManager.instance.WeaponType = 1;

    }
    private void Update()
    {
     
        if (GameManager.instance.ArmorLevel >= 9 && IronText2 != null)
        {
            IronText2.text = 0.ToString();
        }
        if (GameManager.instance.ArmorLevel < 9)
        {
            OreToNextLevelArmor = 100 * (GameManager.instance.ArmorLevel * (GameManager.instance.ArmorLevel * 10));
        }

        if (GameManager.instance.WeaponLevel0 >= 50 && IronText1 != null)
        {
            IronText1.text = 0.ToString();
        }
        if (GameManager.instance.WeaponLevel0 < 50)
        {
            OreToNextLevelWeaponType0 = 2 * (GameManager.instance.WeaponLevel0 * (GameManager.instance.WeaponLevel0 * 2));
        }


    }
    public void UpgradeWeaponButton()
    {
        if (GameManager.instance.IronOre >= OreToNextLevelWeaponType0 && GameManager.instance.WeaponLevel0 < 50)
        {
            if (CanUpgrade)
            {
                StartCoroutine(CanUpgradeItem());
                GameManager.instance.WeaponLevel0++;
                GameManager.instance.IronOre -= OreToNextLevelWeaponType0;
                IronText1.text = OreToNextLevelWeaponType0.ToString();             
            }                
        }
       if(GameManager.instance.WeaponLevel0 >= 50)
        {
            IronText1.text = 0.ToString();
        }
    }
    public void UpgradeArmorButton()
    {
        if (GameManager.instance.IronOre >= OreToNextLevelArmor && GameManager.instance.ArmorLevel < 9)
        {
            if (CanUpgrade)
            {
                StartCoroutine(CanUpgradeItem());
                GameManager.instance.ArmorLevel++;
                GameManager.instance.IronOre -= OreToNextLevelArmor;
                IronText2.text = OreToNextLevelArmor.ToString();
            }
        }
      
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Forge")
        {
            GameObject ForgeObject = other.gameObject;

            Transform ArmorButtonGameobject = ForgeObject.transform.Find("ForgeCanvas");

            IronText1 = ArmorButtonGameobject.transform.GetChild(0).transform.GetChild(2).GetComponent<Text>();
            IronText2 = ArmorButtonGameobject.transform.GetChild(0).transform.GetChild(3).GetComponent<Text>();
            IronText1.text = OreToNextLevelWeaponType0.ToString();
            IronText2.text = OreToNextLevelArmor.ToString();
            //IronText2.text = OreToNextLevelWeaponType0.ToString();

            var buttonWeapon = ArmorButtonGameobject.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>();
            buttonWeapon.onClick.AddListener(delegate () { this.UpgradeWeaponButton(); });

            var buttonArmor = ArmorButtonGameobject.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>();
            buttonArmor.onClick.AddListener(delegate () { this.UpgradeArmorButton(); });
        }
    }
   
    IEnumerator CanUpgradeItem()
    {
        CanUpgrade = false;
        yield return new WaitForSeconds(0.1f);
        CanUpgrade = true;
    }
}
