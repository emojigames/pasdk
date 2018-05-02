using UnityEngine;
using System.Collections;

public class Button_Back : MonoBehaviour {

    [Header("SignInPanel")]
    public GameObject SignInPanelObj;
    [Header("SignUpPanel")]
    public GameObject SignUpPanelObj;
	// Use this for initialization
	void Start () {
	
	}
    public void OnMouseDown()
    {
        BackButtonClick();
    }
    void BackButtonClick()
    {
        Debug.Log("BackButtonClick!!");
        SignInPanelObj.SetActive(true);
        SignUpPanelObj.SetActive(false);
    }
}
