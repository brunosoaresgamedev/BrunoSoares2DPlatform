using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class TransparencyWall : MonoBehaviour
{
    [Range(0, 1)]
    public float transparancy;
    public Renderer targetRenderer;
    
    public float fadeDuration;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetTransparency(transparancy);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetTransparency(1);
        }
    }
    private void SetTransparency(float alpha)
    {
        StopCoroutine("FadeCoroutine");
        StartCoroutine("FadeCoroutine",alpha);
    }

    private IEnumerator FadeCoroutine(float fadeTo)
    {
        float timer = 0;
        Color currentColor = targetRenderer.material.color;
        float startAlpha = targetRenderer.material.color.a;

        while(timer < 1)
        {
            yield return new WaitForEndOfFrame();

            timer += Time.deltaTime / fadeDuration;
            currentColor.a = Mathf.Lerp(startAlpha, fadeTo, timer);
            targetRenderer.material.color = currentColor;
        }
    }

}
