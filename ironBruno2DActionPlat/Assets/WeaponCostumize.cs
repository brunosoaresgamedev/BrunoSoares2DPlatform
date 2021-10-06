using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCostumize : MonoBehaviour
{
    public int skinAimNr;
    public Skins[] skins;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {

        if (skinAimNr > skins.Length - 1) skinAimNr = 0;
        else if (skinAimNr < 0) skinAimNr = skins.Length - 1;

        skinAimNr = GameManager.instance.WeaponLevel;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SkinChoice();
    }

    void SkinChoice()
    {
        if (spriteRenderer.sprite.name.Contains("ClavaT1+0"))
        {
            string spriteName = spriteRenderer.sprite.name;
            spriteName = spriteName.Replace("ClavaT1+0_", "");
            int spriteNr = int.Parse(spriteName);

            spriteRenderer.sprite = skins[skinAimNr].sprites[spriteNr];
        }
    }

    [System.Serializable]
    public struct Skins
    {
        public Sprite[] sprites;
    }

}
