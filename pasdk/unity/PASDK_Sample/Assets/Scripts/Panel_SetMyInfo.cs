using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Panel_SetMyInfo : MonoBehaviour {

    [Header("SetMyInfoPanel")]
    public InputField NewNameField;
    public InputField CurrentPassField;
    public InputField NewPassField;
    public InputField NewPassConfirmField;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (NewNameField.isFocused)
                CurrentPassField.Select();
            else if (CurrentPassField.isFocused)
                NewPassConfirmField.Select();
            else if (NewPassField.isFocused)
                NewPassConfirmField.Select();
            else if (NewPassConfirmField.isFocused)
                NewNameField.Select();

        }
	}
}
