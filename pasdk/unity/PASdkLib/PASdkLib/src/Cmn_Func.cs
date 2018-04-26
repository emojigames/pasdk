using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;

namespace PASdkLib.src
{
    class Cmn_Func
    {
        public static void Init(JsonData initdata)
        {
            Cmn_Var.gameID = initdata["gameId"].ToString();
           
            Cmn_Var.game = new JsonData();
            Cmn_Var.game["gameNo"] = "";

            Cmn_Var.user = new JsonData();
            Cmn_Var.user["user_id"] = "";
            Cmn_Var.user["token"] = "";
            
            Cmn_Var.playerInfo = new JsonData();
            Cmn_Var.playerInfo["email"] = "";
            Cmn_Var.playerInfo["name"] = "";
            Cmn_Var.playerInfo["birthday"] = "";
            Cmn_Var.playerInfo["country"] = "";

            Cmn_Var.localKeys = new JsonData();
            Cmn_Var.localKeys["user_id"] = "";
            Cmn_Var.localKeys["token"] = "";
            Cmn_Var.localKeys["gameId"] = "";

        }
        public static long getMyBalance(){
                return Cmn_Var.playerPocBalance;
        }
        public static void setMyBalance(long val) {
           Cmn_Var.playerPocBalance = val;
        }
        public static void setDefaultUserData()
        {
            clearSessionInfo();
        }
        public static void clearSessionInfo()
        {
            Cmn_Var.user = null;
            PlayerPrefs.SetString(Cmn_Var.KEY_USER_ID, "");
            PlayerPrefs.SetString(Cmn_Var.KEY_USER_TOKEN, "");
        }
        public static void setSessionInfo(String user_id, String token)
        {

            Cmn_Var.user["user_id"] = user_id;
            Cmn_Var.user["token"] = token;
            PlayerPrefs.SetString(Cmn_Var.KEY_USER_ID, user_id);
            PlayerPrefs.SetString(Cmn_Var.KEY_USER_TOKEN, token);
            
        }
        public static JsonData checkSessionInfo()
        {
            if (Cmn_Var.user["user_id"].ToString() == "") Cmn_Var.user["user_id"] = PlayerPrefs.GetString(Cmn_Var.KEY_USER_ID);
            if (Cmn_Var.user["token"].ToString() == "") Cmn_Var.user["token"] = PlayerPrefs.GetString(Cmn_Var.KEY_USER_TOKEN);

            //Debug.Log("checkSessionInfo : user id is " + Cmn_Var.user["user_id"].ToString());
            //Debug.Log("checkSessionInfo : token  is " + Cmn_Var.user["token"].ToString());

            if (Cmn_Var.user["user_id"].ToString() == "" || Cmn_Var.user["token"].ToString() == "")
                return null;
            else
                return Cmn_Var.user;
        }
        public static JsonData getFileMessage()
        {
            JsonData msg = new JsonData();
            msg["returncode"] = -99 + "";
            msg["message"] = "SDK IS NOT INITIALIZED";
            return msg;
        }
    
    }
}
