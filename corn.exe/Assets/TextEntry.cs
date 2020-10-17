using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TextEntry : MonoBehaviour
{
    [SerializeField] TMP_InputField input;
    [SerializeField] ChatBox chatbox;
    // Start is called before the first frame update
    String _instruction;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit")){
            _instruction = input.text;
            input.text = "";
            ChatBox cx = chatbox.GetComponent<ChatBox>();
            cx.UpdateScreen(_instruction);
        }
    }

}
