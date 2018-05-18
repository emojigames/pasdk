using UnityEngine;
using System.Collections;

public class Cmn_Var {

    const string GAME_ID_REAL = "nFfCyLbf";       // real mode
    const string GAME_ID_TRIAL = "nFfCyLbf_trial"; // gameId + _trial =  test mode
    const string GAME_SERVER_REAL = "";         //real server
    const string GAME_SERVER_QA = "-qa";        //qa server
    const string GAME_SERVER_DEV1= "-dev1";     //dev server


    public static string GameID = GAME_ID_TRIAL;
    public static string GameServer = GAME_SERVER_QA; //GAME_SERVER_REAL  or  GAME_SERVER_QA  or GAME_SERVER_DEV1

}
