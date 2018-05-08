using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;

public class Button_SignUp : MonoBehaviour {
    [Header("SignInPanel")]
    public GameObject SignInPanelObj;
    [Header("SignUpPanel")]
    public GameObject SignUpPanelObj;

   // Use this for initialization
	void Start () {
	
	}

    public void OnMouseDown()
    {
        OpenCreateAccount();
        
    }
    public void OpenCreateAccount()
    {
        //Debug.Log("OpenCreateAccount!!");
        SignInPanelObj.SetActive(false);
        SignUpPanelObj.SetActive(true);
        
     
    }
    
   
}
