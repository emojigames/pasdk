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

}
