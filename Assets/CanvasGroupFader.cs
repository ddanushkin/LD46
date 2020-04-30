using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CanvasGroupFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, true, canvasGroup.alpha, 1f));
    }
 
    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, false, canvasGroup.alpha, 0f));
    }
    
    public IEnumerator FadeCanvasGroup(CanvasGroup cg, bool interactable, float start, float end, float lerpTime = 0.5f)
    {
        float timeStarted = Time.time;
        float timeSinceStarted;
        float completionPercentage;

        cg.interactable = false;
        while (true)
        {
            timeSinceStarted = Time.time - timeStarted;
            completionPercentage = timeSinceStarted / lerpTime;
            
            cg.alpha = Mathf.Lerp(start, end, completionPercentage);;
            if (completionPercentage >= 1) break;
            yield return new WaitForEndOfFrame();
        }
        cg.interactable = interactable;
    }

}
