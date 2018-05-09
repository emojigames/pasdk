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
      
    }
    void SignIn(string email, string pass)
    {

        PASdkLib.PASDK.SignIn(this, email, pass,
            (JsonData result) =>    //success 
            {
                /*
                 * result :ex) {"EMAIL":"pasdk@email.com","NAME":"","COUNTRY":"","POC":"50"}
                 * 
                 * string : result["EMAIL"]
                 * string : result["NAME"]
                 * string : result["COUNTRY"]
                 * string : result["POC"]
                 * 
                 */
                Debug.Log("CUBE : SignIn Success Result " + result.ToJson());
                GoMainScenePanel();
                Cmn_function.EmailAgreementCheck(result["POC"].ToString());
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
                Cmn_function.MessageBoxOK("Error", result.ToJson());
            });

    }
    void GoMainScenePanel()
    {
        SignInPanelObj.SetActive(false);
        SignUpPanelObj.SetActive(false);
        MainScenePanelObj.SetActive(true);
    }
}
