using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCostumize : MonoBehaviour
{
    [SerializeField]
    BulletSpriteCostumize bulletSpriteCostumize;
    [SerializeField]
    GameObject BulletPrefab;
    public int skinAimNr;
   
    public Skins[] skinsClava,skinsDagger;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {

        if (skinAimNr > skinsClava.Length - 1) skinAimNr = skinsClava.Length - 1;
        else if (skinAimNr < 0) skinAimNr = skinsClava.Length - 1;

        if (skinAimNr > skinsDagger.Length - 1) skinAimNr = skinsDagger.Length - 1;
        else if (skinAimNr < 0) skinAimNr = skinsDagger.Length - 1;

        //   = GameManager.instance.WeaponLevel1;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        skinAimNr = GameManager.instance.WeaponLevel0;
            SkinChoice();
        bulletSpriteCostumize.skinBulletNr = skinAimNr;
       
       
    }

    void SkinChoice()
    {
        if (spriteRenderer.sprite.name.Contains("ClavaT1+0") && GameManager.instance.WeaponType ==0)
        {
            string spriteName = spriteRenderer.sprite.name;
            spriteName = spriteName.Replace("ClavaT1+0_", "");
            int spriteNr = int.Parse(spriteName);

            spriteRenderer.sprite = skinsClava[skinAimNr].sprites[spriteNr];
        }

        if (spriteRenderer.sprite.name.Contains("ClavaT1+0") && GameManager.instance.WeaponType == 1)
        {
            string spriteName = spriteRenderer.sprite.name;
            spriteName = spriteName.Replace("ClavaT1+0_", "");
            int spriteNr = int.Parse(spriteName);

            spriteRenderer.sprite = skinsDagger[skinAimNr].sprites[spriteNr];
        }
    }


    [System.Serializable]
    public struct Skins
    {
        public Sprite[] sprites;
    }

}
