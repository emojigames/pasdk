using UnityEngine;
using System.Collections;
using System;

using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using LitJson;

public class Cube : MonoBehaviour
{


    void Start()
    {
        //StartCoroutine(PASdkLib.PASDK.GetStr());
        JsonData initData = new JsonData();
        //initData["gameId"] = "x1zqwjpQ";
        //initData["gameId"] = "default";
        initData["gameId"] = "default_trial";



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


                Debug.Log("CUBE : Init Success Result " + result.ToJson());

            },
            (JsonData result) =>    //fail
            {
                /*
                 * result :ex) {"returncode":"-1","message":"error"}
                 * 
                 * string : result["returncode"]
                 * string : result["message"]
                 */
                Debug.Log("CUBE : Init Fail Result " + result.ToJson());
            });

    }

    void OnMouseDown()
    {
        //////////////////////////////////////////////////////
        //Test Call Api
        //////////////////////////////////////////////////////

        //SignUp();
        //SignIn();
        if (IsInitCheck())
        {

            //SignOut();
            //resetPassword();
            //gameStart();
            //setUserPlay();
            //getShopInfo();
            //pocPurchase();
            //getMessage();
            //getMyInfo();
            //setMyInfo();
            //resendEmail();

        }

    }

    bool IsInitCheck()
    {
        bool inInited = PASdkLib.PASDK.isInited();
        return inInited;
    }
    void SignUp()
    {

        PASdkLib.PASDK.SignUp(this, "mbiz.neel@gmail.com", "hky1945!",
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

    void SignIn()
    {

        PASdkLib.PASDK.SignIn(this, "mbiz.neel@gmail.com", "hky1945!",
            (JsonData result) =>    //success 
            {
                /*
                * result :ex) {"message":"success"}
                * 
                * string : result["message"]
                */
                Debug.Log("CUBE : SignIn Success Result " + result.ToJson());
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

    void SignOut()
    {
        PASdkLib.PASDK.SignOut(this,
             (JsonData result) =>    //success 
             {
                 /*
                * result :ex) {"message":"success"}
                * 
                * string : result["message"]
                */
                 Debug.Log("CUBE : SignOut Success Result " + result.ToJson());
             },
            (JsonData result) =>    //fail
            {
                /*
                 * result :ex) {"returncode":"-1","message":"error"}
                 * 
                 * string : result["returncode"]
                 * string : result["message"]
                 */
                Debug.Log("CUBE : SignOut Fail Result " + result.ToJson());
            });
    }
    void resetPassword()
    {
        PASdkLib.PASDK.resetPassword(this, "mbiz.neel@gmail.com",
            (JsonData result) =>    //success 
            {
                /*
               * result :ex) {"message":"email is sent"}
               * 
               * string : result["message"]
               */
                Debug.Log("CUBE : SignOut Success Result " + result.ToJson());
            },
            (JsonData result) =>    //fail
            {
                /*
                 * result :ex) {"returncode":"-1","message":"error"}
                 * 
                 * string : result["returncode"]
                 * string : result["message"]
                 */
                Debug.Log("CUBE : SignOut Fail Result " + result.ToJson());
            });
    }
    void gameStart()
    {
        PASdkLib.PASDK.startGame(this,
             (JsonData result) =>    //success 
             {
                 /*
               * result :ex) {"message":"success"}
               * 
               * string : result["message"]
               */

                 Debug.Log("CUBE : gameStart Success Result " + result.ToJson());
                 //setUserPlay();

             },
            (JsonData result) =>    //fail
            {
                /*
                 * result :ex) {"returncode":"-1","message":"error"}
                 * 
                 * string : result["returncode"]
                 * string : result["message"]
                 */
                Debug.Log("CUBE : gameStart Fail Result " + result.ToJson());
            });
    }
    void setUserPlay()
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
                 Debug.Log("CUBE : setUserPlay Success Result " + result.ToJson());
             },
            (JsonData result) =>    //fail
            {
                /*
                 * result :ex) {"returncode":"-1","message":"error"}
                 * 
                 * string : result["returncode"]
                 * string : result["message"]
                 */
                Debug.Log("CUBE : setUserPlay Fail Result " + result.ToJson());
            });
    }
    void getShopInfo()
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
                Debug.Log("CUBE : getShopInfo Success Result " + result.ToJson());
            },
            (JsonData result) =>    //fail
            {
                /*
                 * result :ex) {"returncode":"-1","message":"error"}
                 * 
                 * string : result["returncode"]
                 * string : result["message"]
                 */
                Debug.Log("CUBE : getShopInfo Fail Result " + result.ToJson());
            });
    }

    void pocPurchase()
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
                Debug.Log("CUBE : pocPurchase Success Result " + result.ToJson());
            },
            (JsonData result) =>    //fail
            {
                /*
                 * result :ex) {"returncode":"-1","message":"error"}
                 * 
                 * string : result["returncode"]
                 * string : result["message"]
                 */
                Debug.Log("CUBE : pocPurchase Fail Result " + result.ToJson());
            });
    }
    void getMessage()
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
                Debug.Log("CUBE : getMessage Success Result " + result.ToJson());
            },
            (JsonData result) =>    //fail
            {
                /*
                 * result :ex) {"returncode":"-1","message":"error"}
                 * 
                 * string : result["returncode"]
                 * string : result["message"]
                 */
                Debug.Log("CUBE : getMessage Fail Result " + result.ToJson());
            });
    }
    void getMyInfo()
    {
        PASdkLib.PASDK.getMyInfo(this,
           (JsonData result) =>    //success 
           {
               /*
               * result :ex)  {"USER_NO":58,"STATUS":1,"STATUS_PACOIN":"enabled","EMAIL":"mbiz.neel@gmail.com","NAME":"","COUNTRY":"","BIRTH_DATE":"","GENDER":"none"}
               * 
               * string : result["USER_NO"]
               * string : result["STATUS"]
               * string : result["STATUS_PACOIN"]
               * string : result["EMAIL"]
               * string : result["NAME"]
               * string : result["COUNTRY"]
               * string : result["BIRTH_DATE"]
               * string : result["GENDER"]
                *              ....
               */
               Debug.Log("CUBE : getMyInfo Success Result " + result.ToJson());
           },
           (JsonData result) =>    //fail
           {
               /*
                * result :ex) {"returncode":"-1","message":"error"}
                * 
                * string : result["returncode"]
                * string : result["message"]
                */
               Debug.Log("CUBE : getMyInfo Fail Result " + result.ToJson());
           });
    }
    void setMyInfo()
    {
        String name = "neel111";
        String birthday = "2000-09-09"; //YYYY-MM-DD
        String gender = "none";
        String password = "hky1945!";

        PASdkLib.PASDK.setMyInfo(this, name, birthday, gender, password,
          (JsonData result) =>    //success 
          {
              /*
              * result :ex) {"message":"edited successfully"}
              * 
              * string : result["message"]
              */
              Debug.Log("CUBE : setMyInfo Success Result " + result.ToJson());
          },
          (JsonData result) =>    //fail
          {
              /*
               * result :ex) {"returncode":"-1","message":"error"}
               * 
               * string : result["returncode"]
               * string : result["message"]
               */
              Debug.Log("CUBE : setMyInfo Fail Result " + result.ToJson());
          });
    }
    void resendEmail()
    {
        PASdkLib.PASDK.resendEmail(this,
          (JsonData result) =>    //success 
          {
              /*
              * result :ex) {"message":"sent successfully"}
              * 
              * string : result["message"]
              */
              Debug.Log("CUBE : resendEmail Success Result " + result.ToJson());
          },
          (JsonData result) =>    //fail
          {
              /*
               * result :ex) {"returncode":"-1","message":"error"}
               * 
               * string : result["returncode"]
               * string : result["message"]
               */
              Debug.Log("CUBE : resendEmail Fail Result " + result.ToJson());
          });
    }
}
