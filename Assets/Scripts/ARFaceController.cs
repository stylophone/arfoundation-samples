using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation.Samples;

public class ARFaceController : MonoBehaviour
{
    public ARKitBlendShapeVisualizer blendShapeVisualizer;
    public GameObject guy;
    public GameObject chick;

    public List<VRMModelData> models;

    private void Awake()
    {
        ChangeModel(0);
    }

    public void ChangeModel(int idx)
    {
        models.ForEach(i => i.gameObject.SetActive(false));
        models[idx].gameObject.SetActive(true);
        blendShapeVisualizer.SkinnedMeshRenderer = models[idx].gameObject.transform.Find("Face").GetComponent<SkinnedMeshRenderer>();
    }
}