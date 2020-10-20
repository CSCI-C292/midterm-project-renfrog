using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatBox : MonoBehaviour
{
    [SerializeField] GameObject chatBox;

    List<string> history = new List<string>();
    int _maxInstructions = 12;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScreen(string instruction){
        chatBox.GetComponent<Text>().text = chatBox.GetComponent<Text>().text + "\n" + instruction;
        history.Add(instruction);
        
    }
}
