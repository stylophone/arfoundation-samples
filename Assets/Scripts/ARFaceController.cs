using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation.Samples;

public class ARFaceController : MonoBehaviour
{
    public ARKitBlendShapeVisualizer blendShapeVisualizer;
    public GameObject guy;
    public GameObject chick;

    public enum HumanType
    {
        Guy,
        Chick
    }

    public void SetHuman(HumanType type)
    {
        if (type == HumanType.Guy)
        {
            guy.SetActive(true);
            var smr = guy.transform.Find("Face").GetComponent<SkinnedMeshRenderer>();
            blendShapeVisualizer.SkinnedMeshRenderer = smr;

            chick.SetActive(false);
        }
        else
        {
            chick.SetActive(true);
            var smr = chick.transform.Find("Face").GetComponent<SkinnedMeshRenderer>();
            blendShapeVisualizer.SkinnedMeshRenderer = smr;

            guy.SetActive(false);
        }
    }
}
