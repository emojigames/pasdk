using UnityEngine;
using System.Collections;
using LitJson;
using System;
using UnityEngine.UI;

public class Panel_MainScene : MonoBehaviour {
    [Header("SigninPanel")]
    public GameObject SignInPanelObj;
    [Header("SignUpPanel")]
    public GameObject SignUpPanelObj;
    [Header("MainScenePanel")]
    public GameObject MainScenePanelObj;
    public Text gameIDObj;
    [Header("SetMyInfoPanel")]
    public GameObject SetMyInfoPanelObj;
    public InputField NameObj;
    public InputField CurrentPasswordObj;
    public InputField NewPasswordObj;
    public InputField NewPasswordConfirmObj;
     
    
    
	// Use this for initialization
	void Start () {
        gameIDObj.text = "gameId : " + Cmn_Var.GameID;
	}
    bool IsInitCheck()
    {
        bool inInited = PASdkLib.PASDK.isInited();
        return inInited;
    }
    void GoSignInPanel()
    {
        SignInPanelObj.SetActive(true);
        SignUpPanelObj.SetActive(false);
        MainScenePanelObj.SetActive(false);
    }
    public void ButtonClick_SignOut()
    {
        if (IsInitCheck())
        {
            PASdkLib.PASDK.SignOut(this,
            (JsonData result) =>    //success 
            {
                /*
               * result :ex) {"message":"success"}
               * 
               * string : result["message"]
               */
                Debug.Log("Panel_MainScene : SignOut Success Result " + result.ToJson());
                GoSignInPanel();
            },
           (JsonData result) =>    //fail
           {
               /*
                * result :ex) {"returncode":"-1","message":"error"}
                * 
                * string : result["returncode"]
                * string : result["message"]
                */
               Debug.Log("Panel_MainScene : SignOut Fail Result " + result.ToJson());
           });
        }
        else { Cmn_function.MessageWrongApproach(); }
       
    }

   
    public void ButtonClick_gameStart()
    {
        if (IsInitCheck())
        {
            PASdkLib.PASDK.startGame(this,
             (JsonData result) =>    //success 
             {
                 /*
               * result :ex) {"message":"success"}
               * 
               * string : result["message"]
               */

                 Debug.Log("Panel_MainScene : gameStart Success Result " + result.ToJson());
                 //setUserPlay();
                 Cmn_function.MessageBoxOK("Success", result.ToJson());

             },
            (JsonData result) =>    //fail
            {
                /*
                 * result :ex) {"returncode":"-1","message":"error"}
                 * 
                 * string : result["returncode"]
                 * string : result["message"]
                 */
                Debug.Log("Panel_MainScene : gameStart Fail Result " + result.ToJson());
                Cmn_function.MessageBoxOK("Error", result.ToJson());
            });
        }
        else { Cmn_function.MessageWrongApproach(); }
        
    }
    public void ButtonClick_setUserPlay()
    {
        
        if (IsInitCheck())
        {
            /// <param name="gametype">0 : SINGLE PLAY | 1 : MULTI PLAY</param>
            /// <param name="gameresult">0 : SINGLE | 1 : WIN | 2 : LOSE | 3 : DRAW</param>
            int gametype = 1;
            int gameresult = 1;
            PASdkLib.PASDK.setUserPlay(this, gametype, gameresult,
                 (JsonData result) =>    //success 
                 {
                     /*
                     * result :ex) {"message":"success","rewardpoc":""}
                     * 
                     * string : result["message"]
                     * string : result["rewardpoc"]
                     */
                     Debug.Log("Panel_MainScene : setUserPlay Success Result " + result.ToJson());
                     Cmn_function.MessageBoxOK("Success", result.ToJson());
                 },
                (JsonData result) =>    //fail
                {
                    /*
                     * result :ex) {"returncode":"-1","message":"error"}
                     * 
                     * string : result["returncode"]
                     * string : result["message"]
                     */
                    Debug.Log("Panel_MainScene : setUserPlay Fail Result " + result.ToJson());
                    Cmn_function.MessageBoxOK("Error", result.ToJson());
                });
        }
        else { Cmn_function.MessageWrongApproach(); }

        
    }
    public void ButtonClick_getShopInfo()
    {
        if (IsInitCheck())
        {
            PASdkLib.PASDK.getShopInfo(this,
            (JsonData result) =>    //success 
            {
                /*
                * result :ex)  {"message":"success","coinamount":50,"gameshop":[{"shopno":"2","title":"item 1","itemimage":"http://ipas.pocketarena.com/pa/ruby01.png","itemamount":"1000","pocamount":"10"},{"shopno":"3","title":"item 2","itemimage":"http://ipas.pocketarena.com/pa/ruby02.png","itemamount":"5500","pocamount":"50"},{"shopno":"4","title":"item 3","itemimage":"http://ipas.pocketarena.com/pa/ruby03.png","itemamount":"12000","pocamount":"100"},{"shopno":"5","title":"item 4","itemimage":"http://ipas.pocketarena.com/pa/ruby04.png","itemamount":"70000","pocamount":"500"}]}
                * 
                * string : result["message"]
                * string : result["coinamount"]
                * array : result["gameshop"]
                 *              ....
                */
                Debug.Log("Panel_MainScene : getShopInfo Success Result " + result.ToJson());
                Cmn_function.MessageBoxOK("Success", result.ToJson());
            },
            (JsonData result) =>    //fail
            {
                /*
                 * result :ex) {"returncode":"-1","message":"error"}
                 * 
                 * string : result["returncode"]
                 * string : result["message"]
                 */
                Debug.Log("Panel_MainScene : getShopInfo Fail Result " + result.ToJson());
                Cmn_function.MessageBoxOK("Error", result.ToJson());
            });
        }
        else { Cmn_function.MessageWrongApproach(); }
        
    }

    public void ButtonClick_pocPurchase()
    {
        if (IsInitCheck())
        {
            int gameShopNo = 3; //test
            PASdkLib.PASDK.pocPurchase(this, gameShopNo,
                (JsonData result) =>    //success 
                {
                    /*
                   * result :ex) {"message":"buying successfully"}
                   * 
                   * string : result["message"]
                   */
                    Debug.Log("Panel_MainScene : pocPurchase Success Result " + result.ToJson());
                    Cmn_function.MessageBoxOK("Success", result.ToJson());
                },
                (JsonData result) =>    //fail
                {
                    /*
                     * result :ex) {"returncode":"-1","message":"error"}
                     * 
                     * string : result["returncode"]
                     * string : result["message"]
                     */
                    Debug.Log("Panel_MainScene : pocPurchase Fail Result " + result.ToJson());
                    Cmn_function.MessageBoxOK("Error", result.ToJson());
                });
        }
        else { Cmn_function.MessageWrongApproach(); }
        
    }
    public void ButtonClick_getMessage()
    {
        if (IsInitCheck())
        {
            int limit = 10; //test  list count 10 ~ 100
            int page = 1;  // now page 1
            PASdkLib.PASDK.getMessage(this, limit, page,
                (JsonData result) =>    //success 
                {
                    /*
                    * result :ex)  {"message":"success","totalcount":"1","hasnext":"0","currentpage":1,"notification":[{"notificationno":"1","title":"Notice Title Test","description":"Notice Description Test","notificationtype":"1","pocamount":"","registdate":1523976927}]}
                    * 
                    * string : result["message"]
                    * int : result["totalcount"]
                    * int : result["hasnext"]
                    * int : result["currentpage"]
                    * array : result["notification"]
                     *              ....
                    */
                    Debug.Log("Panel_MainScene : getMessage Success Result " + result.ToJson());
                    Cmn_function.MessageBoxOK("Success", result.ToJson());
                },
                (JsonData result) =>    //fail
                {
                    /*
                     * result :ex) {"returncode":"-1","message":"error"}
                     * 
                     * string : result["returncode"]
                     * string : result["message"]
                     */
                    Debug.Log("Panel_MainScene : getMessage Fail Result " + result.ToJson());
                    Cmn_function.MessageBoxOK("Error", result.ToJson());
                });
        }
        else { Cmn_function.MessageWrongApproach(); }
       
    }
    public void ButtonClick_getMyInfo()
    {
        if (IsInitCheck())
        {
            PASdkLib.PASDK.getMyInfo(this,
           (JsonData result) =>    //success 
           {
               /*
               * result :ex)  {"USER_NO":58,"STATUS":1,"STATUS_PACOIN":"enabled","EMAIL":"pasdk@email.com","NAME":"","COUNTRY":""}
               * 
               * string : result["EMAIL"]
               * string : result["NAME"]
               * string : result["COUNTRY"]
                *              ....
               */
               Debug.Log("Panel_MainScene : getMyInfo Success Result " + result.ToJson());
               Cmn_function.MessageBoxOK("Success", result.ToJson());
           },
           (JsonData result) =>    //fail
           {
               /*
                * result :ex) {"returncode":"-1","message":"error"}
                * 
                * string : result["returncode"]
                * string : result["message"]
                */
               Debug.Log("Panel_MainScene : getMyInfo Fail Result " + result.ToJson());
               Cmn_function.MessageBoxOK("Error", result.ToJson());
           });
        }
        else { Cmn_function.MessageWrongApproach(); }
        
    }
    public void ButtonClick_GoSetMyInfoPage()
    {
        NameObj.text = "";
        CurrentPasswordObj.text = "";
        NewPasswordObj.text = "";
        NewPasswordConfirmObj.text = "";

        SetMyInfoPanelObj.SetActive(true);
    }
    public void ButtonClick_setMyInfo()
    {
        if (IsInitCheck())
        {
            /////////////////////////////////////
            //수정할 부분만 보낸다. 수정하지 않을 부분은 빈 값으로 보낸다.
            //패스워드는 현재 패스워드와 수정할 패스워드를 같이 보내야 한다.
            /////////////////////////////////////
            String name = NameObj.text;

            String current_password = CurrentPasswordObj.text;

            String new_password = NewPasswordObj.text;
            String new_password_confirm = NewPasswordConfirmObj.text;

            if (Cmn_function.ConfirmPasswordCheck(new_password, new_password_confirm))
            {
                PASdkLib.PASDK.setMyInfo(this, name, current_password, new_password,
                  (JsonData result) =>    //success 
                  {
                      /*
                      * result :ex) {"message":"edited successfully"}
                      * 
                      * string : result["message"]
                      */
                      Debug.Log("Panel_MainScene : setMyInfo Success Result " + result.ToJson());
                      Cmn_function.MessageBoxOK("Success", result.ToJson());
                  },
                  (JsonData result) =>    //fail
                  {
                      /*
                       * result :ex) {"returncode":"-1","message":"error"}
                       * 
                       * string : result["returncode"]
                       * string : result["message"]
                       */
                      Debug.Log("Panel_MainScene : setMyInfo Fail Result " + result.ToJson());
                      Cmn_function.MessageBoxOK("Error", result.ToJson());
                  });
            }
            else
            {
                Cmn_function.MessageBoxOK("Error", "The new password is different.");
            }
        }
        else { Cmn_function.MessageWrongApproach(); }
        
    }
    public void ButtonClick_resendEmail()
    {
        if (IsInitCheck())
        {
            PASdkLib.PASDK.resendEmail(this,
          (JsonData result) =>    //success 
          {
              /*
              * result :ex) {"message":"sent successfully"}
              * 
              * string : result["message"]
              */
              Debug.Log("Panel_MainScene : resendEmail Success Result " + result.ToJson());
              Cmn_function.MessageBoxOK("ReSend Email", result["message"].ToString());
          },
          (JsonData result) =>    //fail
          {
              /*
               * result :ex) {"returncode":"-1","message":"error"}
               * 
               * string : result["returncode"]
               * string : result["message"]
               */
              Debug.Log("Panel_MainScene : resendEmail Fail Result " + result.ToJson());
              Cmn_function.MessageBoxOK("Error", result.ToJson());
          });
        }
        else { Cmn_function.MessageWrongApproach(); }
        
    }
    public void ButtonClick_GetMyPOC()
    {
        long mypoc = PASdkLib.PASDK.getMyPoc();
        Cmn_function.MessageBoxOK("Get My POC", ""+mypoc);
    }
}
