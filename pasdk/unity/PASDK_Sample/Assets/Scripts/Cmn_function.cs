using UnityEngine;
using System.Collections;
using UnityEditor;

public class Cmn_function {

    public delegate void MessageCallback();
    public static void MessageBoxYesNo(string title, string message, string btnYesName, string btnNoName, MessageCallback callbackYes, MessageCallback callbackNo)
    {
        if (EditorUtility.DisplayDialog(title, message, btnYesName, btnNoName))
            callbackYes();
        else
            callbackNo();
    }
    public static void MessageWrongApproach()
    {
        MessageBoxOK("Error", "The wrong approach.", "OK", () => { /*OK Button Click*/ });
    }
    public static void MessageBoxOK(string title, string message, string btnYesName, MessageCallback callbackYes)
    {
        if (EditorUtility.DisplayDialog(title, message, btnYesName))
            callbackYes();
    }
    public static void EmailAgreementCheck(string poc)
    {
        if (poc.Equals("")) MessageBoxOK("Please Check Email", "need agreement for PAcoin", "OK", () => { });
    }
    public static bool ConfirmPasswordCheck(string p1, string p2)
    {
        if (p1.CompareTo(p2) == 0)
            return true;
        else
            return false;
    }
}
