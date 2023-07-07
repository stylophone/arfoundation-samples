using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARKit;
using UnityEngine.XR.ARSubsystems;
using Unity.Collections;

namespace G13Kit
{
    /// <summary>
    /// Populates the action unit coefficients for an <see cref="ARFace"/>.
    /// </summary>
    /// <remarks>
    /// If this <c>GameObject</c> has a <c>SkinnedMeshRenderer</c>,
    /// this component will generate the blend shape coefficients from the underlying <c>ARFace</c>.
    ///
    /// </remarks>
    [RequireComponent(typeof(ARFace))]
    public class MyARKitBlendShapeVisualizer : MonoBehaviour
    {
        public ARFaceManager faceManager;

        [SerializeField]
        private float m_CoefficientScale = 100.0f;

        public float coefficientScale
        {
            get { return m_CoefficientScale; }
            set { m_CoefficientScale = value; }
        }

        [SerializeField]
        private SkinnedMeshRenderer m_SkinnedMeshRenderer;

        public SkinnedMeshRenderer SkinnedMeshRenderer
        {
            get
            {
                return m_SkinnedMeshRenderer;
            }
            set
            {
                m_SkinnedMeshRenderer = value;
            }
        }

        private ARKitFaceSubsystem m_ARKitFaceSubsystem;
        private Dictionary<ARKitBlendShapeLocation, int> m_FaceArkitBlendShapeIndexMap;
        private ARFace m_Face;

        void Awake()
        {
            faceManager.facesChanged += FaceManager_facesChanged;
            CreateFeatureBlendMapping();
        }

        private void FaceManager_facesChanged(ARFacesChangedEventArgs obj)
        {
            if (obj.added.Count != 0)
            {
                m_Face = obj.added[0].GetComponent<ARFace>();
                m_Face.updated += OnUpdated;

                Debug.Log("😀" + obj.added.Count);
            }
        }

        private void CreateFeatureBlendMapping()
        {
            if (SkinnedMeshRenderer == null || SkinnedMeshRenderer.sharedMesh == null)
            {
                return;
            }

            const string strPrefix = "";
            m_FaceArkitBlendShapeIndexMap = new Dictionary<ARKitBlendShapeLocation, int>();

            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowDownLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "browDownLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowDownRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "browDownRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowInnerUp] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "browInnerUp");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowOuterUpLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "browOuterUpLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowOuterUpRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "browOuterUpRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.CheekPuff] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "cheekPuff");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.CheekSquintLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "cheekSquintLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.CheekSquintRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "cheekSquintRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeBlinkLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeBlinkLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeBlinkRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeBlinkRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookDownLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookDown_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookDownRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookDown_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookInLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookInLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookInRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookInRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookOutLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookOutLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookOutRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookOutRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookUpLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookUpLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookUpRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookUpRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeSquintLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeSquintLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeSquintRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeSquintRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeWideLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeWideLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeWideRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeWideRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawForward] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "jawForward");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "jawLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawOpen] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "jawOpen");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "jawRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthClose] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthClose");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthDimpleLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthDimpleLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthDimpleRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthDimpleRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthFrownLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthFrownLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthFrownRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthFrownRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthFunnel] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthFunnel");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthLowerDownLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthLowerDownLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthLowerDownRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthLowerDownRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthPressLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthPressLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthPressRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthPressRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthPucker] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthPucker");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthRollLower] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthRollLower");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthRollUpper] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthRollUpper");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthShrugLower] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthShrugLower");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthShrugUpper] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthShrugUpper");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthSmileLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthSmileLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthSmileRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthSmileRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthStretchLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthStretchLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthStretchRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthStretchRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthUpperUpLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthUpperUpLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthUpperUpRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthUpperUpRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.NoseSneerLeft] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "noseSneerLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.NoseSneerRight] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "noseSneerRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.TongueOut] = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "tongueOut");
        }

        void SetVisible(bool visible)
        {
            if (SkinnedMeshRenderer == null) return;

            SkinnedMeshRenderer.enabled = visible;
        }

        void UpdateVisibility()
        {
            //if (m_Face == null)
            //{
            //    return;
            //}

            //var visible = enabled && (m_Face.trackingState == TrackingState.Tracking) && (ARSession.state > ARSessionState.Ready);
            //SetVisible(visible);
        }

        void OnEnable()
        {
            if (faceManager != null)
            {
                m_ARKitFaceSubsystem = (ARKitFaceSubsystem)faceManager.subsystem;
            }

            //UpdateVisibility();
            //m_Face.updated += OnUpdated;
            //ARSession.stateChanged += OnSystemStateChanged;
        }

        void OnDisable()
        {
            //m_Face.updated -= OnUpdated;
            //ARSession.stateChanged -= OnSystemStateChanged;
        }

        void OnSystemStateChanged(ARSessionStateChangedEventArgs eventArgs)
        {
            //UpdateVisibility();
        }

        void OnUpdated(ARFaceUpdatedEventArgs eventArgs)
        {
            UpdateVisibility();
            UpdateFaceFeatures();
        }

        void UpdateFaceFeatures()
        {
            if (SkinnedMeshRenderer == null || !SkinnedMeshRenderer.enabled || SkinnedMeshRenderer.sharedMesh == null)
            {
                return;
            }

            using (var blendShapes = m_ARKitFaceSubsystem.GetBlendShapeCoefficients(m_Face.trackableId, Allocator.Temp))
            {
                foreach (var featureCoefficient in blendShapes)
                {
                    int mappedBlendShapeIndex;
                    if (m_FaceArkitBlendShapeIndexMap.TryGetValue(featureCoefficient.blendShapeLocation, out mappedBlendShapeIndex))
                    {
                        if (mappedBlendShapeIndex >= 0)
                        {
                            Debug.Log(SkinnedMeshRenderer.gameObject);
                            SkinnedMeshRenderer.SetBlendShapeWeight(mappedBlendShapeIndex, featureCoefficient.coefficient * coefficientScale);
                        }
                    }
                }
            }
        }
    }
}
