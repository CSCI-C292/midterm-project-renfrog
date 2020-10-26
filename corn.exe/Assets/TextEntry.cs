using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class TextEntry : MonoBehaviour
{
    [SerializeField] TMP_InputField input;
    [SerializeField] ChatBox chatbox;
    [SerializeField] RunData runtimeData;
    [SerializeField] GameObject _seedsPrefab;
    Dictionary<string, string> OptionsAndResponses = new Dictionary<string, string>();
    List<Vector3> _NeedsPlants = new List<Vector3>();
    String _instruction;
    
    void Start()
    {
        SetUpPlots();
        PopulateOptions();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") && input.text != ""){
            _instruction = input.text;
            input.text = "";
            ChatBox cx = chatbox.GetComponent<ChatBox>();
            string result = TextMatching();
            cx.UpdateScreen(_instruction + "\n" + result);
        }
    }

    string TextMatching(){
        string pattern = @"[a-z]+";
        Match m = Regex.Match(_instruction, pattern, RegexOptions.IgnoreCase);
        return VerifyInstructions(m.Value);
    }

    private void PopulateOptions(){
        OptionsAndResponses.Add("plant", "--you planted corn into your plots");
        OptionsAndResponses.Add("water", "--you watered your plants");
        OptionsAndResponses.Add("feed", "--you fed your corn");
        OptionsAndResponses.Add("harvest", "--you harvested your corn");
        OptionsAndResponses.Add("wait", "--one day passes");
        OptionsAndResponses.Add("help", "--use 'plant' to plant corn" + '\n'
                                    + "--use 'water' to water your corn" + '\n'
                                    + "--use 'feed' to feed your corn" + '\n'
                                    + "--use 'harvest' to harvest your corn" + '\n'
                                    + "--use 'wait' to skip to the next day" + '\n'
                                    + "--ex: 'plant a1 b1 b3' or 'water b2'");
        OptionsAndResponses.Add("other", "--please use the format 'instruction a1 a2 a3'" + 
                                    '\n' + "--use the keyword help for a list of commands");
    }

    private string VerifyInstructions(string instruct) {
        string _otherResponse = "other";
        List<Plot> plots = FindPlots();
        if(!OptionsAndResponses.ContainsKey(instruct)){
            return OptionsAndResponses[_otherResponse];
        }
        else if(plots.Capacity == 0 && instruct != "help" && instruct != "wait"){
            return OptionsAndResponses[_otherResponse];
        }
        else {
            switch(instruct){
                case "help":
                    return OptionsAndResponses[instruct];
                case "plant":
                    var i = 0;
                    foreach(Plot p in plots){
                        p.Plant();
                        Instantiate(_seedsPrefab, _NeedsPlants[i], Quaternion.identity);
                        Debug.Log(i);
                        i++;
                    }
                    return OptionsAndResponses[instruct];
                case "water":
                    foreach(Plot p in plots){
                        p.Water();
                    }
                    return OptionsAndResponses[instruct];
                case "feed":
                    return OptionsAndResponses[instruct];
                case "harvest":
                    return OptionsAndResponses[instruct];
                case "wait":
                    Debug.Log("wait time");
                    return OptionsAndResponses[instruct];
                default:
                    return OptionsAndResponses[_otherResponse];    
            }

        }     
    }

    private List<Plot> FindPlots(){
        List<Plot> NeedChanges = new List<Plot>();
        _NeedsPlants = new List<Vector3>();
        if(_instruction.Contains("a1")){
            NeedChanges.Add(RunData.A1);
            _NeedsPlants.Add(new Vector3(-6.31f,-0.38f,-2.25f));
        }
        if(_instruction.Contains("a2")){
            NeedChanges.Add(RunData.A2);
            _NeedsPlants.Add(new Vector3(-5.19f, 0.15f, -2.25f));
        }
        if(_instruction.Contains("a3")){
            NeedChanges.Add(RunData.A3);
            _NeedsPlants.Add(new Vector3(-3.95f, 0.66f, -2.25f));
        }
        if(_instruction.Contains("b1")){
            NeedChanges.Add(RunData.B1);
            _NeedsPlants.Add(new Vector3(-5.17f, -0.95f, -2.25f));
        }
        if(_instruction.Contains("b2")){
            NeedChanges.Add(RunData.B2);
            _NeedsPlants.Add(new Vector3(-3.93f, -0.40f, -2.25f));
        }
        if(_instruction.Contains("b3")){
            NeedChanges.Add(RunData.B3);
            _NeedsPlants.Add(new Vector3(-2.76f, 0.11f, -2.25f));
        }
        if(_instruction.Contains("c1")){
            NeedChanges.Add(RunData.C1);
            _NeedsPlants.Add(new Vector3(-3.98f, -1.46f, -2.25f));
        }
        if(_instruction.Contains("c2")){
            NeedChanges.Add(RunData.C2);
            _NeedsPlants.Add(new Vector3(-2.76f, -0.97f, -2.25f));
        }
        if(_instruction.Contains("c3")){
            NeedChanges.Add(RunData.C3);
            _NeedsPlants.Add(new Vector3(-1.56f, -0.38f, -2.25f));
        }
        return NeedChanges;
    }

    private void SetUpPlots() {
        RunData.A1.SetPosition(new Vector3(-6.31f,-0.38f,-2.25f));
        RunData.A2.SetPosition(new Vector3(-5.19f, 0.15f, -2.25f));
        RunData.A3.SetPosition(new Vector3(-3.95f, 0.66f, -2.25f));
        RunData.B1.SetPosition(new Vector3(-5.17f, -0.95f, -2.25f));
        RunData.B2.SetPosition(new Vector3(-3.93f, -0.40f, -2.25f));
        RunData.B3.SetPosition(new Vector3(-2.76f, 0.11f, -2.25f));
        RunData.C1.SetPosition(new Vector3(-3.98f, -1.46f, -2.25f));
        RunData.C2.SetPosition(new Vector3(-2.76f, -0.97f, -2.25f));
        RunData.C3.SetPosition(new Vector3(-1.56f, -0.38f, -2.25f));
    }

}
