using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    CanvasGroup canvasGroup;


    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
      //  StartCoroutine(FadeOutIn());
    }

      public IEnumerator FadeOutIn()
    {
        
        yield return FadeOut(3f);
        print("fadou out");
        yield return FadeIn(3f);
        print("fadou in");

    }

    public IEnumerator FadeOut(float time)
    {
        while(canvasGroup.alpha < 1) // alpha is not 1
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha += Time.deltaTime / time;
            yield return null;

            //moving alpha toward 1
        }
    }
     public IEnumerator FadeIn(float time)
    {
        while (canvasGroup.alpha > 0) // alpha is not 1
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
            canvasGroup.blocksRaycasts = false;
            //moving alpha toward 1
        }

    }
}
