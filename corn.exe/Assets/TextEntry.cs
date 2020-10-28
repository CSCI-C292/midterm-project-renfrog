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
    [SerializeField] Rain rain;
    Dictionary<string, string> OptionsAndResponses = new Dictionary<string, string>();
    List<Vector3> _NeedsPlants = new List<Vector3>();
    public GameData gameData;

    String _instruction;
    public float timeLeft = 20.0f;
    int _days = 0;
    int _sacrificed = 0;
    int _harvested = 0;
    int _limit = 10;
    
    void Start()
    {
        PopulateOptions();
    }

    // Update is called once per frame
    void Update()
    {
        ChatBox cx = chatbox.GetComponent<ChatBox>();
        if(Input.GetButtonDown("Submit") && input.text != ""){
            _instruction = input.text;
            input.text = "";
            string result = TextMatching();
            cx.UpdateScreen(_instruction + "\n" + result);
        }

        string res = PassTime();
        if (res != ""){
                cx.UpdateScreen("another day passes");
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
        OptionsAndResponses.Add("sacrifice", "--you sacrifice your harvested corn" + '\n'
                                    + "--it begins to rain harder");
        OptionsAndResponses.Add("help", "--use 'plant' to plant corn" + '\n'
                                    + "--use 'water' to water your corn" + '\n'
                                    + "--use 'feed' to feed your corn" + '\n'
                                    + "--use 'harvest' to harvest your corn" + '\n'
                                    + "--use 'wait' to skip to the next day" + '\n'
                                    + "--use 'sacrifice' to sacrifice your corn" + '\n'
                                    + "--ex: 'plant a1 b1 b3' or 'sacrifice b2'");
        OptionsAndResponses.Add("other", "--please use the format 'instruction a1 a2 a3'" + 
                                    '\n' + "--use the keyword help for a list of commands");
    }

    private string VerifyInstructions(string instruct) {
        string _otherResponse = "other";
        List<Plot> plots = FindPlots();
        if(!OptionsAndResponses.ContainsKey(instruct)){
            return OptionsAndResponses[_otherResponse];
        }
        else if(plots.Capacity == 0 && instruct != "help" && instruct != "wait" && instruct != "sacrifice"){
            return OptionsAndResponses[_otherResponse];
        }
        else {
            switch(instruct){
                case "help":
                    return OptionsAndResponses[instruct];
                case "plant":
                    foreach(Plot p in plots){
                        p.Plant();
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
                    foreach(Plot p in plots){
                        var worked = p.Harvested();
                        if (worked){
                            _harvested++;
                        }
                        Debug.Log(_harvested);
                    }
                    return OptionsAndResponses[instruct];
                case "sacrifice":
                    _sacrificed = _sacrificed + _harvested;
                    if(_sacrificed > _limit){
                        _limit = _limit + 10;
                        rain.IncreaseRain();
                    }
                    return OptionsAndResponses[instruct];
                case "wait":
                    SkipTime();
                    DayPass();
                    return OptionsAndResponses[instruct];
                default:
                    return OptionsAndResponses[_otherResponse];    
            }

        }     
    }

    private String PassTime(){
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            _days++;
            timeLeft = 20.0f;
            DayPass();
            return "another day passes";
        }
        return "";
    }

    private void DayPass(){
        for (int i = 0; i < 9; i++){
            var current = gameData.plotList[i];
            current.Grow();
        }
    
    }

    private void SkipTime(){
        timeLeft = 20.0f;
        _days++;
    }

    private List<Plot> FindPlots(){
        List<Plot> NeedChanges = new List<Plot>();
        _NeedsPlants = new List<Vector3>();
        Debug.Log(_instruction);
        if(_instruction.Contains("a1")){
            NeedChanges.Add(gameData.A1);
        }
        if(_instruction.Contains("a2")){
            NeedChanges.Add(gameData.A2);
        }
        if(_instruction.Contains("a3")){
            NeedChanges.Add(gameData.A3);
        }
        if(_instruction.Contains("b1")){
            NeedChanges.Add(gameData.B1);
        }
        if(_instruction.Contains("b2")){
            NeedChanges.Add(gameData.B2);
        }
        if(_instruction.Contains("b3")){
            NeedChanges.Add(gameData.B3);
        }
        if(_instruction.Contains("c1")){
            NeedChanges.Add(gameData.C1);
        }
        if(_instruction.Contains("c2")){
            NeedChanges.Add(gameData.C2);
        }
        if(_instruction.Contains("c3")){
            NeedChanges.Add(gameData.C3);
        }
        return NeedChanges;
        
    }

}
