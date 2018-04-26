using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using PASdkLib.src;
using System.Net;
using System.IO;
using UnityEngine;
using System.Collections;
using LitJson;

namespace PASdkLib
{
    public delegate void CBFunc_Success(JsonData message);
    public delegate void CBFunc_Fail(JsonData message);
    
    public class PASDK
    {

        /// <summary>
        ///  PASDK Initial Called only once at first
        /// </summary>
        /// <param name="method">MonoBehaviour</param>
        /// <param name="initdata">Initial JsonData</param> gameId
        /// <param name="cb_success">Success Callback Function</param>LitJson JsonData
        /// <param name="cb_fail">Fail Callback Function</param>LitJson JsonData
        public static void Init(MonoBehaviour method, JsonData initdata, CBFunc_Success cb_success, CBFunc_Fail cb_fail)
        {
            String gameid = initdata["gameId"].ToString();
            Cmn_Func.Init(initdata);
            
            getGameInfo(method, gameid, (JsonData result) =>
            {

                //JsonData msgs = new JsonData();
                int c = 0;
                //Dictionary<string, string> list = new Dictionary<string, string>();
                String[] msgs = { "", "", "", "", "", "", "", "", ""};
                getMyInfo(method, (JsonData r1) => {
                    c++;

//                    Debug.Log("******************* getMyInfo Success " + r1.ToJson());

                    //msgs[0] = r1["USER_NO"].ToString();
                    //msgs[1] = r1["STATUS"].ToString();
                    //msgs[2] = r1["STATUS_PACOIN"].ToString();
                    msgs[3] = r1["EMAIL"].ToString();
                    msgs[4] = r1["NAME"].ToString();
                    msgs[5] = r1["COUNTRY"].ToString();
                    msgs[6] = r1["BIRTH_DATE"].ToString();
                    msgs[7] = r1["GENDER"].ToString();
                    
                    if (c == 2)
                    {
                        JsonData msg = new JsonData();
                        //msg["USER_NO"] = msgs[0];
                        //msg["STATUS"] = msgs[1];
                        //msg["STATUS_PACOIN"] = msgs[2];
                        msg["EMAIL"] = msgs[3];
                        msg["NAME"] = msgs[4];
                        msg["COUNTRY"] = msgs[5];
                        msg["BIRTH_DATE"] = msgs[6];
                        msg["GENDER"] = msgs[7];
                        msg["POC"] = msgs[8];
                        Cmn_Var.isInit = true;
                        cb_success(msg);
                    }
                }, (JsonData r2) => {
                    //Debug.Log("******************** getMyInfo Fail "+r2.ToJson());
                    cb_fail(r2); });

                pocView(method, (JsonData r1) => {
                    c++;
                    //JsonData msgs = new JsonData();
                    //Debug.Log("+++++++++++++++++pocView Success " + r1.ToJson());
                    //list.Add("POC", r1["POC"].ToString());
                    msgs[8] = r1["POC"].ToString();
                    if (c == 2)
                    {
                        JsonData msg = new JsonData();
                        //msg["USER_NO"] = msgs[0];
                        //msg["STATUS"] = msgs[1];
                        //msg["STATUS_PACOIN"] = msgs[2];
                        msg["EMAIL"] = msgs[3];
                        msg["NAME"] = msgs[4];
                        msg["COUNTRY"] = msgs[5];
                        msg["BIRTH_DATE"] = msgs[6];
                        msg["GENDER"] = msgs[7];
                        msg["POC"] = msgs[8];
                        Cmn_Var.isInit = true;
                        cb_success(msg);
                    }
                }, (JsonData r2) => {
                    //Debug.Log("+++++++++++++++++pocView Fail " + r2.ToJson());
                    cb_fail(r2); });
               
                
            },
            (JsonData result) =>
            {
                cb_fail(result);
            });
        }
        /// <summary>
        /// get isInit
        /// </summary>
        /// <returns>boolean</returns>
        public static bool isInited()
        {
            return Cmn_Var.isInit;
        }
       
        /// <summary>
        ///     SignUp
        /// </summary>
        /// <param name="method">MonoBehaviour</param>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <param name="cb_success">Success Callback Function</param>LitJson JsonData
        /// <param name="cb_fail">Fail Callback Function</param>LitJson JsonData
        public static void SignUp(MonoBehaviour method, String email, String password, CBFunc_Success cb_success, CBFunc_Fail cb_fail)
        {
            String murl = Cmn_Var.url + Cmn_Var.apimember + "signup";

            //Debug.Log("murl is " + murl);
            WWWForm form = new WWWForm();
            form.AddField("email", email);
            form.AddField("password", password);

            //resultFunction rf = new resultFunction(LoginResult);
            method.StartCoroutine(ConnectManager.getInst().SendData(murl, form, (String result) =>
            {
                //ex){"code":100,"message":"sent successfully"}
                Debug.Log("SignUp result is " + result);
                JsonData jsonObject = JsonMapper.ToObject(result);
                JsonData msg = new JsonData();
                int returncode = int.Parse(jsonObject["code"].ToString());
                if (returncode == 100)
                {
                    //Debug.Log("SignUp Success " + result);
                    //Cmn_Func.setSessionInfo(jsonObject["user_id"].ToString(), jsonObject["token"].ToString());
                    //msg["message"] = jsonObject["message"].ToString();
                    //cb_success(msg);
                    SignIn(method,email,password,
                         (JsonData r1) =>    //success 
                         {
                             cb_success(r1);
                         },
                          (JsonData r2) =>    //fail
                          {
                              cb_success(r2);
                          });
                }
                else
                {
                    msg["returncode"] = returncode + "";
                    msg["message"] = jsonObject["message"].ToString();
                    cb_fail(msg);
                }
            }));
        }
        /// <summary>
        /// SignIn
        /// </summary>
        /// <param name="method">MonoBehaviour</param>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <param name="cb_success">Success Callback Function</param>LitJson JsonData
        /// <param name="cb_fail">Fail Callback Function</param>LitJson JsonData
        public static void SignIn(MonoBehaviour method, String email, String password, CBFunc_Success cb_success, CBFunc_Fail cb_fail)
        {
            if (Cmn_Var.game["gameNo"] != null)
            {
                String murl = Cmn_Var.url + Cmn_Var.apimember + "signin";
                //Debug.Log("murl is " + murl);
                WWWForm form = new WWWForm();
                form.AddField("email", email);
                form.AddField("password", password);
                form.AddField("game_no", Cmn_Var.game["gameNo"].ToString());
                //form.AddField("user_id", Cmn_Var.user["user_id"].ToString());
                
                //resultFunction rf = new resultFunction(LoginResult);
                method.StartCoroutine(ConnectManager.getInst().SendData(murl, form, (String result) =>
                {
                    //ex){code: 100, user_id: "rzYkZFqciABs", token: "oAyzZFhQvwLJcaHLDcvT"}
                    //Debug.Log("result is " + result);
                    JsonData jsonObject = JsonMapper.ToObject(result);
                    JsonData msg = new JsonData();
                    int returncode = int.Parse(jsonObject["code"].ToString());
                    if (returncode == 100)
                    {
                        Cmn_Func.setSessionInfo(jsonObject["user_id"].ToString(), jsonObject["token"].ToString());
                        //msg["user_id"] = jsonObject["user_id"].ToString();
                        //msg["token"] = jsonObject["token"].ToString();
                        msg["message"] = "success";
                        cb_success(msg);
                    }
                    else
                    {
                        msg["returncode"] = returncode + "";
                        msg["message"] = jsonObject["message"].ToString();
                        cb_fail(msg);
                    }
                }));
            }
            else
            {
                //Debug.Log("Login fail!!!");
                cb_fail(Cmn_Func.getFileMessage());
            }
        }
        /// <summary>
        /// SignOut
        /// </summary>
        /// <param name="method">MonoBehaviour</param>
        /// <param name="cb_success">Success Callback Function</param>LitJson JsonData
        /// <param name="cb_fail">Fail Callback Function</param>LitJson JsonData
        public static void SignOut(MonoBehaviour method, CBFunc_Success cb_success, CBFunc_Fail cb_fail)
        {

             Cmn_Func.setDefaultUserData();
             Cmn_Var.isInit = false;
             JsonData msg = new JsonData();
             msg["message"] = "success";
             cb_success(msg);
           
        }
        /// <summary>
        ///  Reset Password
        /// </summary>
        /// <param name="method"MonoBehaviour></param>
        /// <param name="email">email</param>
        /// <param name="cb_success">Success Callback Function</param>LitJson JsonData
        /// <param name="cb_fail">Fail Callback Function</param>LitJson JsonData
        public static void resetPassword(MonoBehaviour method, String email, CBFunc_Success cb_success, CBFunc_Fail cb_fail)
        {

            String murl = Cmn_Var.url + Cmn_Var.apimember + "reset_password";

            //Debug.Log("murl is " + murl);
            WWWForm form = new WWWForm();
            form.AddField("email", email);
           
            //resultFunction rf = new resultFunction(LoginResult);
            method.StartCoroutine(ConnectManager.getInst().SendData(murl, form, (String result) =>
            {
                //Debug.Log("resetPwd result is " + result);
                JsonData jsonObject = JsonMapper.ToObject(result);
                JsonData msg = new JsonData();
                int returncode = int.Parse(jsonObject["code"].ToString());
                if (returncode == 100)
                {
                    msg["message"] = jsonObject["message"];
                    cb_success(msg);
                }
                else
                {
                    msg["returncode"] = returncode + "";
                    msg["message"] = jsonObject["message"].ToString();
                    cb_fail(msg);
                }
            }));
        }
        /// <summary>
        ///  get My Info
        /// </summary>
        /// <param name="method">MonoBehaviour</param>
        /// <param name="cb_success">Success Callback Function</param>LitJson JsonData
        /// <param name="cb_fail">Fail Callback Function</param>LitJson JsonData
        public static void getMyInfo(MonoBehaviour method, CBFunc_Success cb_success, CBFunc_Fail cb_fail)
        {
            
            JsonData hasSession = Cmn_Func.checkSessionInfo();
            //cb_success(Cmn_Func.getFileMessage());//test
            
            if(Cmn_Var.game["gameNo"] != null && hasSession != null)
            {

                String murl = Cmn_Var.url + Cmn_Var.apimember + "view";

                //Debug.Log("murl is " + murl);
                WWWForm form = new WWWForm();
                form.AddField("game_no", Cmn_Var.game["gameNo"].ToString());
                form.AddField("user_id", Cmn_Var.user["user_id"].ToString());
                form.AddField("token", Cmn_Var.user["token"].ToString());
           
                //resultFunction rf = new resultFunction(LoginResult);
                method.StartCoroutine(ConnectManager.getInst().SendData(murl, form, (String result) =>
                {
                    //Debug.Log("result is " + result);
                    JsonData jsonObject = JsonMapper.ToObject(result);
                    JsonData msg = new JsonData();
                    int returncode = int.Parse(jsonObject["code"].ToString());
                    if (returncode == 100)
                    {
                        //msg["message"] = jsonObject["message"];
                        Cmn_Var.playerInfo["email"] = jsonObject["message"]["EMAIL"];
                        Cmn_Var.playerInfo["name"] = jsonObject["message"]["NAME"];
                        Cmn_Var.playerInfo["birthday"] = jsonObject["message"]["BIRTH_DATE"];
                        Cmn_Var.playerInfo["country"] = jsonObject["message"]["COUNTRY"];

                        msg["USER_NO"] = jsonObject["message"]["USER_NO"] != null ? jsonObject["message"]["USER_NO"] : "";
                        msg["STATUS"] = jsonObject["message"]["STATUS"] != null ? jsonObject["message"]["STATUS"] : "";
                        msg["STATUS_PACOIN"] = jsonObject["message"]["STATUS_PACOIN"] != null ? jsonObject["message"]["STATUS_PACOIN"] : "";
                        msg["EMAIL"] = jsonObject["message"]["EMAIL"] != null ? jsonObject["message"]["EMAIL"] : "";
                        msg["NAME"] = jsonObject["message"]["NAME"] != null ? jsonObject["message"]["NAME"] : "";
                        msg["COUNTRY"] = jsonObject["message"]["COUNTRY"] != null ? jsonObject["message"]["COUNTRY"] : "";
                        msg["BIRTH_DATE"] = jsonObject["message"]["BIRTH_DATE"] != null ? jsonObject["message"]["BIRTH_DATE"] : "";
                        msg["GENDER"] = jsonObject["message"]["GENDER"] != null ? jsonObject["message"]["GENDER"] : "";
                        
                        //Debug.Log("getMyInfo success ");
                        cb_success(msg);
                    }
                    else
                    {
                        //Debug.Log("getMyInfo fail");
                        msg["returncode"] = returncode + "";
                        msg["message"] = jsonObject["message"].ToString();
                        cb_fail(msg);
                    }
                }));
            }
            else
            {
                cb_fail(Cmn_Func.getFileMessage());
            }
        }
        /// <summary>
        /// set My Info
        /// </summary>
        /// <param name="method">MonoBehaviour</param>
        /// <param name="name">name</param>
        /// <param name="birthday">birthday</param>
        /// <param name="gender">gender</param>
        /// <param name="password">password</param>
        /// <param name="cb_success">Success Callback Function</param>LitJson JsonData
        /// <param name="cb_fail">Fail Callback Function</param>LitJson JsonData
        public static void setMyInfo(MonoBehaviour method, String name, String birthday, String gender , String password, CBFunc_Success cb_success, CBFunc_Fail cb_fail)
        {

            JsonData hasSession = Cmn_Func.checkSessionInfo();
            if(Cmn_Var.game["gameNo"] != null && hasSession != null)
            {
                String murl = Cmn_Var.url + Cmn_Var.apimember + "edit";

                //Debug.Log("murl is " + murl);
                WWWForm form = new WWWForm();
                form.AddField("game_no", Cmn_Var.game["gameNo"].ToString());
                form.AddField("user_id", Cmn_Var.user["user_id"].ToString());
                form.AddField("token", Cmn_Var.user["token"].ToString());
                
                JsonData d = new JsonData();
                d["name"] = name.ToString();
                d["birthday"] = birthday.ToString();
                d["gender"] = gender.ToString();
                d["password"] = password.ToString();

                form.AddField("data", d.ToJson());
           
                //resultFunction rf = new resultFunction(LoginResult);
                method.StartCoroutine(ConnectManager.getInst().SendData(murl, form, (String result) =>
                {
                    //Debug.Log("result is " + result);
                    JsonData jsonObject = JsonMapper.ToObject(result);
                    JsonData msg = new JsonData();
                    int returncode = int.Parse(jsonObject["code"].ToString());
                    if (returncode == 100)
                    {
                        //msg["message"] = jsonObject["message"];
                        cb_success(jsonObject["message"]);
                    }
                    else
                    {
                        msg["returncode"] = returncode + "";
                        msg["message"] = jsonObject["message"].ToString();
                        cb_fail(msg);
                    }
                }));
            }
            else
            {
                cb_fail(Cmn_Func.getFileMessage());
            }
        }
        /// <summary>
        /// getMessage
        /// </summary>
        /// <param name="method">MonoBehaviour</param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="cb_success">Success Callback Function</param>LitJson JsonData
        /// <param name="cb_fail">Fail Callback Function</param>LitJson JsonData
        public static void getMessage(MonoBehaviour method, int limit, int page, CBFunc_Success cb_success, CBFunc_Fail cb_fail)
        {

            JsonData hasSession = Cmn_Func.checkSessionInfo();
            if (Cmn_Var.game["gameNo"] != null && hasSession != null)
            {
                String murl = Cmn_Var.gameApiUrl + "getNotification" + Cmn_Var.gameApiExt;

                //Debug.Log("murl is " + murl);
                WWWForm form = new WWWForm();
                form.AddField("gameid", Cmn_Var.gameID);
                form.AddField("userid", Cmn_Var.user["user_id"].ToString());
                form.AddField("token", Cmn_Var.user["token"].ToString());
                form.AddField("listcount", limit);
                form.AddField("page", page);

                //resultFunction rf = new resultFunction(LoginResult);
                method.StartCoroutine(ConnectManager.getInst().SendData(murl, form, (String result) =>
                {
                    //Debug.Log("getMessage result is " + result);
                    JsonData jsonObject = JsonMapper.ToObject(result);
                    JsonData msg = new JsonData();
                    int returncode = int.Parse(jsonObject["returncode"].ToString());
                    if (returncode == 100)
                    {
                        //msg["message"] = jsonObject["message"];
                        msg["message"] = jsonObject["message"] != null ? jsonObject["message"] : "";
                        msg["totalcount"] = int.Parse(jsonObject["totalcount"].ToString());
                        msg["hasnext"] = int.Parse(jsonObject["hasnext"].ToString());
                        msg["currentpage"] = int.Parse(jsonObject["currentpage"].ToString());
                        msg["notification"] = jsonObject["notification"];
                        
                        cb_success(msg);
                      
                    }
                    else
                    {
                        msg["returncode"] = returncode + "";
                        msg["message"] = jsonObject["message"].ToString();
                        cb_fail(msg);
                    }
                }));
            }
            else
            {
                cb_fail(Cmn_Func.getFileMessage());
            }
        }
        /// <summary>
        /// Get POC Coin
        /// </summary>
        /// <returns></returns>
        public static long getMyPoc()
        {
            return Cmn_Var.playerPocBalance;
        }
        /// <summary>
        ///  pocView get POC coin
        /// </summary>
        /// <param name="method">MonoBehaviour</param>
        /// <param name="cb_success">Success Callback Function</param>LitJson JsonData
        /// <param name="cb_fail">Fail Callback Function</param>LitJson JsonData
        public static void pocView(MonoBehaviour method, CBFunc_Success cb_success, CBFunc_Fail cb_fail)
        {

            JsonData hasSession = Cmn_Func.checkSessionInfo();
            if (Cmn_Var.game["gameNo"] != null && hasSession != null)
            {
                String murl = Cmn_Var.url + Cmn_Var.apipoc + "view";

                //Debug.Log("hasSession is " + hasSession.ToJson() + " user_id " + Cmn_Var.user["user_id"].ToString());
                WWWForm form = new WWWForm();

                form.AddField("game_no", Cmn_Var.game["gameNo"].ToString());
                form.AddField("user_id", Cmn_Var.user["user_id"].ToString());
                form.AddField("token", Cmn_Var.user["token"].ToString());

                //Debug.Log("pocView : 000 gameNo " + Cmn_Var.game["gameNo"].ToString());
                //Debug.Log("pocView : 111 user_id " + Cmn_Var.user["user_id"].ToString());
                //Debug.Log("pocView : 222 token " + Cmn_Var.user["token"].ToString());
                //resultFunction rf = new resultFunction(LoginResult);
                method.StartCoroutine(ConnectManager.getInst().SendData(murl, form, (String result) =>
                {
                    //Debug.Log("pocView : 111");
                    //Debug.Log("result is " + result);
                    JsonData jsonObject = JsonMapper.ToObject(result);
                    JsonData msg = new JsonData();
                    int returncode = int.Parse(jsonObject["code"].ToString());
                    if (returncode == 100)
                    {
                        //msg["message"] = jsonObject["message"];
                        if(jsonObject["message"] != null)
                            Cmn_Var.playerPocBalance = long.Parse(jsonObject["message"].ToString());
                        msg["POC"] = Cmn_Var.playerPocBalance.ToString();
                        cb_success(msg);
                    }
                    else
                    {
                        msg["returncode"] = returncode + "";
                        msg["message"] = jsonObject["message"].ToString();
                        cb_fail(msg);
                    }
                }));
            }
            else
            {
                cb_fail(Cmn_Func.getFileMessage());
            }
        }
        /// <summary>
        ///  Start Game 
        /// </summary>
        /// <param name="method">MonoBehaviour</param>
        /// <param name="cb_success">Success Callback Function</param>LitJson JsonData
        /// <param name="cb_fail">Fail Callback Function</param>LitJson JsonData
        public static void startGame(MonoBehaviour method, CBFunc_Success cb_success, CBFunc_Fail cb_fail)
        {

            JsonData hasSession = Cmn_Func.checkSessionInfo();
            if (Cmn_Var.game["gameNo"] != null && hasSession != null)
            {
                String murl = Cmn_Var.gameApiUrl + "startGame" + Cmn_Var.gameApiExt;

                //Debug.Log("murl is " + murl);
                WWWForm form = new WWWForm();
                form.AddField("gameid", Cmn_Var.gameID);
                form.AddField("userid", Cmn_Var.user["user_id"].ToString());
                form.AddField("token", Cmn_Var.user["token"].ToString());
               
                //Debug.Log("gameid : " + Cmn_Var.gameID);
                //Debug.Log("user_id : " + Cmn_Var.user["user_id"].ToString());
                //Debug.Log("token : " + Cmn_Var.user["token"].ToString());
              

                //resultFunction rf = new resultFunction(LoginResult);
                method.StartCoroutine(ConnectManager.getInst().SendData(murl, form, (String result) =>
                {
                    //Debug.Log("startGame result is " + result);
                    JsonData jsonObject = JsonMapper.ToObject(result);
                    JsonData msg = new JsonData();
                    int returncode = int.Parse(jsonObject["returncode"].ToString());
                    if (returncode == 100)
                    {

                        Cmn_Var.playLogoNo = jsonObject["playlogno"].ToString();
                        msg["message"] = jsonObject["message"].ToString();
                        //msg["playlogno"] = jsonObject["playlogno"].ToString();
                        
                        cb_success(msg);
                    }
                    else
                    {
                        msg["returncode"] = returncode + "";
                        msg["message"] = jsonObject["message"].ToString();
                        cb_fail(msg);
                    }
                }));
            }
            else
            {
                cb_fail(Cmn_Func.getFileMessage());
            }
        }
        /// <summary>
        /// setUserPlay : get reward poc
        /// </summary>
        /// <param name="method">MonoBehaviour</param>
        /// <param name="gametype">0 : SINGLE PLAY | 1 : MULTI PLAY</param>
        /// <param name="gameresult">0 : SINGLE | 1 : WIN | 2 : LOSE | 3 : DRAW</param>
        /// <param name="cb_success">Success Callback Function</param>LitJson JsonData
        /// <param name="cb_fail">Fail Callback Function</param>LitJson JsonData
        public static void setUserPlay(MonoBehaviour method, int gametype, int gameresult, CBFunc_Success cb_success, CBFunc_Fail cb_fail)
        {
            //Debug.Log("********* gametype is " + gametype + " gameResult is " + gameresult);
            JsonData hasSession = Cmn_Func.checkSessionInfo();
            if (Cmn_Var.game["gameNo"] != null && hasSession != null)
            {
                String murl = Cmn_Var.gameApiUrl + "setPlayData" + Cmn_Var.gameApiExt;

                //Debug.Log("murl is " + murl);
                WWWForm form = new WWWForm();
                form.AddField("gameid", Cmn_Var.gameID);
                form.AddField("userid", Cmn_Var.user["user_id"].ToString());
                form.AddField("token", Cmn_Var.user["token"].ToString());
                form.AddField("gametype", gametype);
                form.AddField("gameresult", gameresult);
                form.AddField("playlogno", Cmn_Var.playLogoNo);

               /* Debug.Log("1gameid : " + Cmn_Var.gameID);
                Debug.Log("1user_id : " + Cmn_Var.user["user_id"].ToString());
                Debug.Log("1token : " + Cmn_Var.user["token"].ToString());
                Debug.Log("1gametype : " + gametype.ToString());
                Debug.Log("1gameresult : " + gameresult.ToString());
                Debug.Log("1playlogno : " + Cmn_Var.playLogoNo);*/

                int tcnt = 0;

                //resultFunction rf = new resultFunction(LoginResult);
                method.StartCoroutine(ConnectManager.getInst().SendData(murl, form, (String result) =>
                {
                    tcnt++;
                    JsonData jsonObject = JsonMapper.ToObject(result);
                    JsonData msg = new JsonData();
                    int returncode = int.Parse(jsonObject["returncode"].ToString());
                    if (returncode == 100)
                    {
                        long rewardpoc = 0;
                            if(jsonObject["rewardpoc"] != null && jsonObject["rewardpoc"].ToString() != "")
                                rewardpoc = long.Parse(jsonObject["rewardpoc"].ToString());

                        Cmn_Var.playerPocBalance = Cmn_Var.playerPocBalance + rewardpoc;
                        msg["message"] = jsonObject["message"].ToString();
                        msg["rewardpoc"] = jsonObject["rewardpoc"] != null ? jsonObject["rewardpoc"] : "";
                        cb_success(msg);
                        
                    }
                    else
                    {
                        msg["returncode"] = returncode + "";
                        msg["message"] = jsonObject["message"].ToString();
                        cb_fail(msg);
                    }
                }));
            }
            else
            {
                cb_fail(Cmn_Func.getFileMessage());
            }
        }
        /// <summary>
        /// getShopInfo
        /// </summary>
        /// <param name="method">MonoBehaviour</param>
        /// <param name="cb_success">Success Callback Function</param>LitJson JsonData
        /// <param name="cb_fail">Fail Callback Function</param>LitJson JsonData
        public static void getShopInfo(MonoBehaviour method, CBFunc_Success cb_success, CBFunc_Fail cb_fail)
        {

            JsonData hasSession = Cmn_Func.checkSessionInfo();
            if (hasSession != null)
            {
                String murl = Cmn_Var.gameApiUrl + "getShop" + Cmn_Var.gameApiExt;

                //Debug.Log("hasSession is " + hasSession.ToJson());
                WWWForm form = new WWWForm();
                form.AddField("gameid", Cmn_Var.gameID);
                form.AddField("userid", Cmn_Var.user["user_id"].ToString());
                form.AddField("token", Cmn_Var.user["token"].ToString());
             
                //resultFunction rf = new resultFunction(LoginResult);
                method.StartCoroutine(ConnectManager.getInst().SendData(murl, form, (String result) =>
                {
                    //Debug.Log("getShopInfo result is " + result);
                    JsonData jsonObject = JsonMapper.ToObject(result);
                    JsonData msg = new JsonData();
                    int returncode = int.Parse(jsonObject["returncode"].ToString());
                    if (returncode == 100)
                    {
                        msg["message"] = jsonObject["message"].ToString();
                        msg["coinamount"] = jsonObject["coinamount"];
                        msg["gameshop"] = jsonObject["gameshop"];
                        cb_success(msg);
                    }
                    else
                    {
                        msg["returncode"] = returncode + "";
                        msg["message"] = jsonObject["message"].ToString();
                        cb_fail(msg);
                    }
                }));
            }
            else
            {
                cb_fail(Cmn_Func.getFileMessage());
            }
        }
        /// <summary>
        /// itemPurchase  
        /// </summary>
        /// <param name="method">MonoBehaviour</param>
        /// <param name="gameShopNo">buy item number 2~5</param>
        private static void itemPurchase(MonoBehaviour method, int gameShopNo)
        {

            JsonData hasSession = Cmn_Func.checkSessionInfo();
            if (hasSession != null)
            {
                String murl = Cmn_Var.gameApiUrl + "purchaseLog" + Cmn_Var.gameApiExt;

                //Debug.Log("murl is " + murl);
                WWWForm form = new WWWForm();
                form.AddField("gameid", Cmn_Var.gameID);
                form.AddField("userid", Cmn_Var.user["user_id"].ToString());
                form.AddField("token", Cmn_Var.user["token"].ToString());
                form.AddField("gameshopno", gameShopNo);

                //resultFunction rf = new resultFunction(LoginResult);
                method.StartCoroutine(ConnectManager.getInst().SendData(murl, form, (String result) =>
                {
                    //Debug.Log("itemPurchase result is " + result);
                    JsonData jsonObject = JsonMapper.ToObject(result);
                    JsonData msg = new JsonData();
                    int returncode = int.Parse(jsonObject["returncode"].ToString());
                    if (returncode == 100)
                    {
                        msg["message"] = jsonObject["message"] != null ? jsonObject["message"].ToString() : "";
                        // Cmn_Var.playerPocBalance = long.Parse(jsonObject["message"].ToString());
                        //cb_success(msg);
                    }
                    else
                    {
                        msg["returncode"] = returncode + "";
                        msg["message"] = jsonObject["message"].ToString();
                        //cb_fail(msg);
                    }
                }));
            }
            else
            {
                //cb_fail(Cmn_Func.getFileMessage());
            }
        }

        
        /// <summary>
        ///  pocPurchase   POC Coin Purchase
        /// </summary>
        /// <param name="method">MonoBehaviour</param>
        /// <param name="gameShopNo">gameShop Number</param>
        /// <param name="cb_success">Success Callback Function</param>LitJson JsonData
        /// <param name="cb_fail">Fail Callback Function</param>LitJson JsonData
        public static void pocPurchase(MonoBehaviour method,int gameShopNo, CBFunc_Success cb_success, CBFunc_Fail cb_fail)
        {

            JsonData hasSession = Cmn_Func.checkSessionInfo();
            if (Cmn_Var.game["gameNo"] != null && hasSession != null)
            {
                String murl = Cmn_Var.url + Cmn_Var.apipoc + "get_money";

                //Debug.Log("murl is " + murl);
                WWWForm form = new WWWForm();
                form.AddField("game_no", Cmn_Var.game["gameNo"].ToString());
                form.AddField("user_id", Cmn_Var.user["user_id"].ToString());
                form.AddField("token", Cmn_Var.user["token"].ToString());
                form.AddField("game_shop_no", gameShopNo);
                //Debug.Log("game_no is " + Cmn_Var.game["gameNo"].ToString());
                //resultFunction rf = new resultFunction(LoginResult);
                method.StartCoroutine(ConnectManager.getInst().SendData(murl, form, (String result) =>
                {
                    //Debug.Log("pocPurchase result is " + result);
                    JsonData jsonObject = JsonMapper.ToObject(result);
                    JsonData msg = new JsonData();
                    int returncode = int.Parse(jsonObject["code"].ToString());
                    if (returncode == 100)
                    {
                        itemPurchase(method, gameShopNo);
                        msg["message"] = jsonObject["message"];
                        cb_success(msg);
                    }
                    else
                    {
                        msg["returncode"] = returncode + "";
                        msg["message"] = jsonObject["message"].ToString();
                        cb_fail(msg);
                    }
                }));
            }
            else
            {
                cb_fail(Cmn_Func.getFileMessage());
            }
        }
        /// <summary>
        ///  getGameInfo
        /// </summary>
        /// <param name="method">MonoBehaviour</param>
        /// <param name="game_id">game_id</param>
        /// <param name="cb_success">Success Callback Function</param>LitJson JsonData
        /// <param name="cb_fail">Fail Callback Function</param>LitJson JsonData
        public static void getGameInfo(MonoBehaviour method, String game_id, CBFunc_Success cb_success, CBFunc_Fail cb_fail)
        {
            Cmn_Var.gameID = game_id;
            String murl = Cmn_Var.gameApiUrl + "getGameInfo" + Cmn_Var.gameApiExt;


            //Debug.Log("murl is " + murl);
            WWWForm form = new WWWForm();
            form.AddField("gameid", game_id);
          
            //resultFunction rf = new resultFunction(LoginResult);
            method.StartCoroutine(ConnectManager.getInst().SendData(murl, form, (String result) =>
            {
                //{"returncode":"100","message":"success","gameno":"1","startdate":1523383094,"enddate":1523987894,"gameversion":"1","leaderyn":"1","leadertype":"0"}
                //Debug.Log("result is " + result);
                JsonData jsonObject = JsonMapper.ToObject(result);
                JsonData msg = new JsonData();
                int returncode = int.Parse(jsonObject["returncode"].ToString());
                if (returncode == 100)
                {
                    //Debug.Log("getGameInfo Success : " + jsonObject["message"].ToString());
                    //Debug.Log("gameNo is " + jsonObject["gameno"]);
                    if(jsonObject["gameno"] != null)
                        Cmn_Var.game["gameNo"] = jsonObject["gameno"];

                    msg["message"] = jsonObject["message"] != null ? jsonObject["message"].ToString() : "";
                    msg["gameno"] = jsonObject["gameno"] != null ? jsonObject["gameno"].ToString() : "";
                    msg["startdate"] = jsonObject["startdate"] != null ? jsonObject["startdate"].ToString() : "";
                    msg["enddate"] = jsonObject["enddate"] != null ? jsonObject["enddate"].ToString() : "";
                    msg["gameversion"] = jsonObject["gameversion"] != null ? jsonObject["gameversion"].ToString() : "";
                    msg["leaderyn"] = jsonObject["leaderyn"] != null ? jsonObject["leaderyn"].ToString() : "";
                    msg["leadertype"] = jsonObject["leadertype"] != null ? jsonObject["leadertype"].ToString() : "";
                    
                    //Debug.Log("111 gameNo is " + Cmn_Var.game["gameNo"]);
                    cb_success(msg);
                }
                else
                {
                    Debug.Log("getGameInfo Fail : " + jsonObject["message"].ToString());
                    msg["returncode"] = returncode + "";
                    msg["message"] = jsonObject["message"].ToString();
                    cb_fail(msg);
                }
            }));
        }
        /// <summary>
        /// resend Email
        /// </summary>
        /// <param name="method">MonoBehaviour</param>
        /// <param name="cb_success">Success Callback Function</param>LitJson JsonData
        /// <param name="cb_fail">Fail Callback Function</param>LitJson JsonData
        public static void resendEmail(MonoBehaviour method, CBFunc_Success cb_success, CBFunc_Fail cb_fail)
        {

            JsonData hasSession = Cmn_Func.checkSessionInfo();
            if (Cmn_Var.game["gameNo"] != null && hasSession != null)
            {
                String murl = Cmn_Var.url + Cmn_Var.apimember + "resend_email";

                //Debug.Log("hasSession is " + hasSession.ToJson() + " user_id " + Cmn_Var.user["user_id"].ToString());
                WWWForm form = new WWWForm();

                form.AddField("game_no", Cmn_Var.game["gameNo"].ToString());
                form.AddField("user_id", Cmn_Var.user["user_id"].ToString());
                form.AddField("token", Cmn_Var.user["token"].ToString());

                method.StartCoroutine(ConnectManager.getInst().SendData(murl, form, (String result) =>
                {
                    //Debug.Log("pocView : 111");
                    //Debug.Log("resendEmail result is " + result);
                    JsonData jsonObject = JsonMapper.ToObject(result);
                    JsonData msg = new JsonData();
                    int returncode = int.Parse(jsonObject["code"].ToString());
                    if (returncode == 100)
                    {
                        msg["message"] = jsonObject["message"];
                        cb_success(msg);
                    }
                    else
                    {
                        msg["returncode"] = returncode + "";
                        msg["message"] = jsonObject["message"].ToString();
                        cb_fail(msg);
                    }
                }));
            }
            else
            {
                cb_fail(Cmn_Func.getFileMessage());
            }
        }
       

    }
}
