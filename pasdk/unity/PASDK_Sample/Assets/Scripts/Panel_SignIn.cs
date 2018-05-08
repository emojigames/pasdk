using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Panel_SignIn : MonoBehaviour {

    [Header("SigninPanel")]
    public InputField emailField;
    public InputField passField;
    // Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (emailField.isFocused)
                passField.Select();
            else if (passField.isFocused)
                emailField.Select();
        }
            
	}
}
