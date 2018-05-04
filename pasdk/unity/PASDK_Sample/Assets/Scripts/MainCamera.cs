using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;

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



        PASdkLib.PASDK.Init(this, initData,
            (JsonData result) =>    //success 
            {
                /*
                 * result :ex) {"USER_NO":"58","STATUS":"1","STATUS_PACOIN":"enabled","EMAIL":"mbiz.neel@gmail.com","NAME":"","COUNTRY":"","BIRTH_DATE":"","GENDER":"none","POC":"50"}
                 * 
                 * string : result["USER_NO"]
                 * string : result["STATUS"]
                 * string : result["STATUS_PACOIN"]
                 * string : result["EMAIL"]
                 * string : result["NAME"]
                 * string : result["COUNTRY"]
                 * string : result["BIRTH_DATE"]
                 * string : result["POC"]
                 * 
                 */


                Debug.Log("MainCamera : Init Success Result " + result.ToJson());
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
