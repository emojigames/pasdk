using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;

public class Button_SignIn : MonoBehaviour {

    [Header("SigninPanel")]
    public GameObject SignInPanelObj;
    public InputField emailField;
    public InputField passField;
    [Header("SignUpPanel")]
    public GameObject SignUpPanelObj;
    [Header("MainScenePanel")]
    public GameObject MainScenePanelObj;

    // Use this for initialization
	void Start () {
	
	}
    public void OnMouseDown()
    {
        SignIn(emailField.text, passField.text);
        Debug.Log("*********Button_SignIn.cs OnMouseDown");
    }
    void SignIn(string email, string pass)
    {

        PASdkLib.PASDK.SignIn(this, email, pass,
            (JsonData result) =>    //success 
            {
                /*
                * result :ex) {"message":"success"}
                * 
                * string : result["message"]
                */
                Debug.Log("CUBE : SignIn Success Result " + result.ToJson());
                GoMainScenePanel();
            },
            (JsonData result) =>    //fail
            {
                /*
                 * result :ex) {"returncode":"-1","message":"error"}
                 * 
                 * string : result["returncode"]
                 * string : result["message"]
                 */
                Debug.Log("CUBE : SignIn Fail Result " + result.ToJson());
            });

    }
    void GoMainScenePanel()
    {
        SignInPanelObj.SetActive(false);
        SignUpPanelObj.SetActive(false);
        MainScenePanelObj.SetActive(true);
    }
}
