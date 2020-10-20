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
        /*
        if (history.Capacity > _maxInstructions){
            history.RemoveAt(0);
            history.RemoveAt(1);
            history.RemoveAt(2);
            history.RemoveAt(3);
            history.RemoveAt(4);
            string newHistory = "";
            foreach(var instruct in history) {
                if (instruct )
                newHistory = newHistory + "\n";
            }
            newHistory = newHistory + instruction;
            chatBox.GetComponent<Text>().text = newHistory;
            history.Add(instruction);
        }
        */
        chatBox.GetComponent<Text>().text = chatBox.GetComponent<Text>().text + "\n" + instruction;
        history.Add(instruction);
        
    }
}
