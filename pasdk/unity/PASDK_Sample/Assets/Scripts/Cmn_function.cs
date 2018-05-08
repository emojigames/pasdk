using UnityEngine;
using System.Collections;

using UnityEngine.UI;
public class Cmn_function {

   
    public delegate void MessageCallback();
    /*public static void MessageBoxYesNo(string title, string message, string btnYesName, string btnNoName, MessageCallback callbackYes, MessageCallback callbackNo)
    {
        if (EditorUtility.DisplayDialog(title, message, btnYesName, btnNoName))
            callbackYes();
        else
            callbackNo();
    }*/

    public static void MessageWrongApproach()
    {
        string title = "Error";
        string message = "The wrong approach.";
       
        //MessageBoxOK("Error", "The wrong approach.", "OK", () => { /*OK Button Click*/ });
        Panel_MessageBox.Instance().ShowMessageBoxOK(title, message);
    }
    public static void MessageBoxOK(string title, string message)
    {
       // if (EditorUtility.DisplayDialog(title, message, btnYesName))
       //     callbackYes();
      
        Panel_MessageBox.Instance().ShowMessageBoxOK(title, message);
    }
    public static void PanelMessageBox(string title, string message)
    {
        Panel_MessageBox.Instance().ShowMessageBoxOK(title, message);
    }
    public static void EmailAgreementCheck(string poc)
    {
        string title = "Please Check Email";
        string message = "need agreement for PAcoin";
        //if (poc.Equals("")) MessageBoxOK("Please Check Email", "need agreement for PAcoin", "OK", () => { });
        if (poc.Equals("")) Panel_MessageBox.Instance().ShowMessageBoxOK(title, message);
    }
    public static bool ConfirmPasswordCheck(string p1, string p2)
    {
        if (p1.CompareTo(p2) == 0)
            return true;
        else
            return false;
    }
}
