using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ViewFitter : MonoBehaviour
{
    enum FitVariants
    {
        Vertical,
        Horizontal,
        Both,
        None
    }
    
    [SerializeField] private Renderer objectToTif;
    [SerializeField] private FitVariants fitVariant = FitVariants.None;

    void Start ()
    {
        if (Camera.main == null || fitVariant == FitVariants.None || objectToTif == null)
            return;
        Camera cameraMain = Camera.main;
        Vector3 boundsSize = objectToTif.bounds.size;
        
        if (fitVariant == FitVariants.Horizontal)
            cameraMain.orthographicSize = boundsSize.x * Screen.height / Screen.width * 0.5f;
        else if (fitVariant == FitVariants.Vertical)
            cameraMain.orthographicSize = boundsSize.y / 2;
        else if (fitVariant == FitVariants.Both)
        {
            float screenRatio = (float)Screen.width / Screen.height;
            float targetRatio = boundsSize.x / boundsSize.y;
            
            if(screenRatio >= targetRatio)
                cameraMain.orthographicSize = boundsSize.y / 2;
            else
            {
                float differenceInSize = targetRatio / screenRatio;
                cameraMain.orthographicSize = boundsSize.y / 2 * differenceInSize;
            }
        }
    }
}
