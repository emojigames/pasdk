using UnityEngine;
using System.Collections;

public class Button_Back : MonoBehaviour {

    [Header("SignInPanel")]
    public GameObject SignInPanelObj;
    [Header("SignUpPanel")]
    public GameObject SignUpPanelObj;
    [Header("SetMyInfoPanel")]
    public GameObject SetMyInfoPanelObj;
    // Use this for initialization
	void Start () {
	
	}
   
    public void Back_SignUp()
    {
        //Debug.Log("BackButtonClick!!");
        SignInPanelObj.SetActive(true);
        SignUpPanelObj.SetActive(false);
        SetMyInfoPanelObj.SetActive(false);
    }
    public void Back_SetMyInfo()
    {
        SetMyInfoPanelObj.SetActive(false);
    }
}
