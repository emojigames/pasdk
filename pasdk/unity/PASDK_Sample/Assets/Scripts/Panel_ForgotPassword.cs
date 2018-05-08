using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using LitJson;
public class Panel_ForgotPassword : MonoBehaviour {

    [Header("ForgotPasswordPanel")]
    public GameObject ForgotPasswordPanelObj;
    public InputField EmailObj;
    [Header("MessageBoxPanel")]
    public Text MessageTextTitle;
    public Text MessageTextMessage;
  
	// Use this for initialization
	void Start () {
	    
	}
    public void SubmitButtonClick()
    {
        String email = EmailObj.text;
        resetPassword(email);

    }
    public void resetPassword(String email)
    {

        PASdkLib.PASDK.resetPassword(this, email,
           (JsonData result) =>    //success 
           {
               /*
              * result :ex) {"message":"email is sent"}
              * 
              * string : result["message"]
              */
               Debug.Log("Panel_ForgotPassword : SignOut Success Result " + result.ToJson());
               Cmn_function.MessageBoxOK("Reset Password", result["message"].ToString());
           },
           (JsonData result) =>    //fail
           {
               /*
                * result :ex) {"returncode":"-1","message":"error"}
                * 
                * string : result["returncode"]
                * string : result["message"]
                */
               Debug.Log("Panel_ForgotPassword : SignOut Fail Result " + result.ToJson());
           });
            
        

    }
    public void BackButtonClick()
    {
        ForgotPasswordPanelObj.SetActive(false);
    }
   

}
