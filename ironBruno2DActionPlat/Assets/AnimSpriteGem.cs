using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpriteGem : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ParticleEffectsTimerSprite());
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ParticleEffectsTimerSprite()
    {
        while (true)
        {

            spriteRenderer.color = new Color(1, 0, 1, 1);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = new Color(0, 0, 1, 1);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = new Color(0, 1, 0, 1);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = new Color(0, 1, 1, 1);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = new Color(1, 1, 0, 1);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }
}
