using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Panel_SignUp : MonoBehaviour {

    [Header("SignUpPanel")]
    public InputField NewEmailField;
    public InputField NewPassField;
    public InputField NewPassConfirmField;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (NewEmailField.isFocused)
                NewPassField.Select();
            else if (NewPassField.isFocused)
                NewPassConfirmField.Select();
            else if (NewPassConfirmField.isFocused)
                NewEmailField.Select();

        }
	}
}
