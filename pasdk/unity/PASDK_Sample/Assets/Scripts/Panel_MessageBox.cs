using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Panel_MessageBox : MonoBehaviour {

    [Header("MessageBoxPanel")]
    public GameObject MessageBoxPanelObj;
    public Text TextTitle;
    public Text TextMessage;
    public InputField InputFieldMessage;
    public Button OKBtn;
  
	// Use this for initialization
    private static Panel_MessageBox modalPanel;
    public static Panel_MessageBox Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(Panel_MessageBox)) as Panel_MessageBox;
            if (!modalPanel)
                Debug.Log("There needs to be one active Panel_MessageBox script on a GameObject in your scene.");
        }
        return modalPanel;
    }
	
	
    //public void ShowMessageBoxOK(string title, string message, UnityAction OKEvent)
    public void ShowMessageBoxOK(string title, string message)
    {
        
        MessageBoxPanelObj.SetActive(true);
        TextTitle.text = title;
        TextMessage.text = message;
        InputFieldMessage.text = message;
        OKBtn.onClick.RemoveAllListeners();
        //OKBtn.onClick.AddListener(OKEvent);
        OKBtn.onClick.AddListener(ClickOKButton);
        
    }
    public void ClickOKButton()
    {
        
        TextTitle.text = "";
        TextMessage.text = "";
        TextMessage.text = "";
        MessageBoxPanelObj.SetActive(false);
    }
   
}
