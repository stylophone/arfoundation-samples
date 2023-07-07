using UnityEngine.UI;
using UnityEngine;

public class ARKitFaceAppController : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public GameObject border;
    //public GameObject guyBorder;
    //public GameObject chickBorder;

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    public void HandleOnClickToggle(bool isOn)
    {
        //Debug.Log(toggleGroup.GetFirstActiveToggle());

        border.transform.position = toggleGroup.GetFirstActiveToggle().transform.position;
        int idx = int.Parse(toggleGroup.GetFirstActiveToggle().gameObject.name);
        FindObjectOfType<ARFaceController>().ChangeModel(idx);
    }

    //public void HandleOnSelectGuy(bool isOn)
    //{
    //    guyBorder.SetActive(true);
    //    chickBorder.SetActive(false);

    //    FindObjectOfType<ARFaceController>().SetHuman(ARFaceController.HumanType.Guy);
    //}

    //public void HandleOnSelectChick(bool isOn)
    //{
    //    chickBorder.SetActive(true);
    //    guyBorder.SetActive(false);

    //    FindObjectOfType<ARFaceController>().SetHuman(ARFaceController.HumanType.Chick);
    //}
}