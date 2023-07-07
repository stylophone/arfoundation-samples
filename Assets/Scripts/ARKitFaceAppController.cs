using UnityEngine;

public class ARKitFaceAppController : MonoBehaviour
{
    public GameObject guyBorder;
    public GameObject chickBorder;

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    public void HandleOnSelectGuy(bool isOn)
    {
        guyBorder.SetActive(true);
        chickBorder.SetActive(false);

        FindObjectOfType<ARFaceController>().SetHuman(ARFaceController.HumanType.Guy);
    }

    public void HandleOnSelectChick(bool isOn)
    {
        chickBorder.SetActive(true);
        guyBorder.SetActive(false);

        FindObjectOfType<ARFaceController>().SetHuman(ARFaceController.HumanType.Chick);
    }
}