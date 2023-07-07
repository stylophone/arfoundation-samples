using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IPadAdapter : MonoBehaviour
{
    public float matchWidthOrHeight;

    void Start()
    {
        if (IsIPad())
        {
            bool isCanvasExists = TryGetComponent<CanvasScaler>(out CanvasScaler canvasScaler);
            if (isCanvasExists)
            {
                canvasScaler.matchWidthOrHeight = matchWidthOrHeight;
            }
        }
    }

    public static bool IsIPad()
    {
        if (Application.isEditor)
        {
            return UnityEngine.Device.SystemInfo.deviceModel.Contains("iPad");
        }
        else
        {
            return SystemInfo.deviceModel.Contains("iPad");
        }
    }
}