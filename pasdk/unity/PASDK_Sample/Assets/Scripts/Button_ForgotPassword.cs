using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Button_ForgotPassword : MonoBehaviour {
    [Header("ForgotPasswordPanel")]
    public GameObject ForgotPasswordPanelObj;
    public InputField ForgotEmailObj;
    
	// Use this for initialization
	void Start () {
	
	}

    public void OnMouseDown()
    {
        
        OpenForgotPasswordScene();
       
    }
    void OpenForgotPasswordScene()
    {
        ForgotEmailObj.text = "";
        ForgotPasswordPanelObj.SetActive(true);
    }
}
