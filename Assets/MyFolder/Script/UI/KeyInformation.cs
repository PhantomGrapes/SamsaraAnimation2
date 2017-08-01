using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyInformation : MonoBehaviour {
    InputField text;
    public KeyCode current;

    private void Start()
    {
        //text.transform.GetChild(1).GetComponent<Text>().fontSize = text.transform.GetChild(0).GetComponent<Text>().fontSize;
        text = GetComponentInChildren<InputField>();
    }

    private void Update()
    {
        if (!Input.anyKey)
            text.text = current.ToString().ToUpper();
        else
            text.text = text.text.ToUpper();
    }

    private void OnGUI()
    {
        if (Event.current.isKey && text.isFocused) {
            current = Event.current.keyCode;
        }
    }
}
