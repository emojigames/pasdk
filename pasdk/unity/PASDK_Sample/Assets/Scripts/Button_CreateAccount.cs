using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
using UnityEditor;

public class Button_CreateAccount : MonoBehaviour {

    [Header("SignUpPanel")]
    public InputField NewEmailField;
    public InputField NewPassField;
    public InputField ConfirmPassField;
    // Use this for initialization
	void Start () {
	
	}
    public void OnMouseDown()
    {
        SignUp(NewEmailField.text, NewPassField.text);
    }
    void SignUp(string new_email, string new_pass)
    {
        
        if (Cmn_function.ConfirmPasswordCheck(NewPassField.text, ConfirmPassField.text))
        {
            PASdkLib.PASDK.SignUp(this, new_email, new_pass,
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
                  Debug.Log("Button_CreateAccount : SignUp Success Result " + result.ToJson());
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
                  Debug.Log("Button_CreateAccount : SignUp Fail Result " + result.ToJson());
              });
        }
        else
        {
            Cmn_function.MessageBoxOK("Error","The password is different.", "OK", 
                () =>
                {   //OK Button Click

                });
            
        }
        
    }
    
}
