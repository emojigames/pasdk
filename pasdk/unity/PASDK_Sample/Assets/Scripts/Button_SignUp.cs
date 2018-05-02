using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
using UnityEditor;
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
        Debug.Log("*********CreateAccountButton.cs OnMouseDown");
    }
    public void OpenCreateAccount()
    {
        //Debug.Log("OpenCreateAccount!!");
        SignInPanelObj.SetActive(false);
        SignUpPanelObj.SetActive(true);
        
        
        /*if (EditorUtility.DisplayDialog("Title!!", "Message TExt", "Yes", "No"))
            print("Pressed Yes.");
        else
            print("Pressed No.");*/
    }
    
   
}
