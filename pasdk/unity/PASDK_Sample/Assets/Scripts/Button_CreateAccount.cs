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
        Debug.Log("Button_CreateAccount OnClick!!");
        SignUp(NewEmailField.text, NewPassField.text);
    }
    void SignUp(string new_email, string new_pass)
    {
        
        if (ConfirmPasswordCheck(NewPassField.text, ConfirmPassField.text))
        {
            PASdkLib.PASDK.SignUp(this, new_email, new_pass,
              (JsonData result) =>    //success 
              {
                  /*
               * result :ex) {"message":"success"}
               * 
               * string : result["message"]
               */
                  Debug.Log("CUBE : SignUp Success Result " + result.ToString());
              },
              (JsonData result) =>    //fail
              {
                  /*
                 * result :ex) {"returncode":"-1","message":"error"}
                 * 
                 * string : result["returncode"]
                 * string : result["message"]
                 */
                  Debug.Log("CUBE : SignUp Fail Result " + result.ToString());
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
    bool ConfirmPasswordCheck(string p1, string p2)
    {
        if (p1.CompareTo(p2) == 0)
            return true;
        else 
            return false;
    }
}
