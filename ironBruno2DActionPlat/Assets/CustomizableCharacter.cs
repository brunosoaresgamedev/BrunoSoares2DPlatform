using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizableCharacter : MonoBehaviour
{

    public int skinNr;
    public Skins[] skins;

    SpriteRenderer spriteRenderer;
       // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (skinNr > skins.Length - 1) skinNr = skins.Length - 1;
        else if (skinNr < 0) skinNr = skins.Length - 1;

        skinNr = GameManager.instance.ArmorLevel;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SkinChoice();
        
    }

    void SkinChoice()
    {
        if (spriteRenderer.sprite.name.Contains("CharacterMain"))
        {
            string spriteName = spriteRenderer.sprite.name;
            spriteName = spriteName.Replace("CharacterMain_", "");
            int spriteNr = int.Parse(spriteName);

            spriteRenderer.sprite = skins[skinNr].sprites[spriteNr];
        }
    }

    [System.Serializable]
    public struct Skins
    {
        public Sprite[] sprites;
    }

  
}
