using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
using System;
using UnityEngine.Events;

public class MainCamera : MonoBehaviour {

    [Header("SigninPanel")]
    public GameObject SignInPanelObj;
    [Header("SignUpPanel")]
    public GameObject SignUpPanelObj;
    [Header("MainScenePanel")]
    public GameObject MainScenePanelObj;
   
      // Use this for initialization
	void Start () {

        JsonData initData = new JsonData();

        //initData["gameId"] = Cmn_Var.GAME_ID_REAL;
        initData["gameId"] = Cmn_Var.GameID;

        
        //Debug.Log("Start!!!");
        PASdkLib.PASDK.Init(this, initData,
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


                Debug.Log("MainCamera : Init Success Result " + result.ToJson());
                GoMainScenePanel();
               // displayManager.DisplayMessage("success testmessa");
                //msgPanel.ShowMessageBoxOK("testTitle", "testMessage", YesAction);
               
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
                
                Debug.Log("MainCamera : Init Fail Result " + result.ToJson());
                GoSignInPanel();
            });
	}
    void GoMainScenePanel()
    {
        SignInPanelObj.SetActive(false);
        SignUpPanelObj.SetActive(false);
        MainScenePanelObj.SetActive(true);
    }
    void GoSignInPanel()
    {
        SignInPanelObj.SetActive(true);
        SignUpPanelObj.SetActive(false);
        MainScenePanelObj.SetActive(false);
    }
   
}
