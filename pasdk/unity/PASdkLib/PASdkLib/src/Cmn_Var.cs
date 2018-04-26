using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LitJson;

namespace PASdkLib.src
{
    class Cmn_Var
    {
        public static bool isInit = false;
        public static String gameID = "default";
        public static String KEY_USER_ID = "user_id";
        public static String KEY_USER_TOKEN = "userToken";
        public static long playerPocBalance = 0;
        public static String playLogoNo = "";
        public static String url = "https://pa.emojigames.io";
        public static String gameApiUrl = "https://pa.emojigames.io/ssapi/";
        public static String gameApiExt = ".php";
        public static String apimember = "/member/";
        public static String apipoc = "/poc/";
        public static JsonData game = null;
        public static JsonData playerInfo = null;
        public static JsonData user = null;
        public static JsonData localKeys = null;
        public static JsonData initmsg = new JsonData();

    }
}
