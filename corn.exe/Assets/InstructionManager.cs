using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class InstructionManager : MonoBehaviour
{
    [SerializeField] TMP_InputField input;
    [SerializeField] ChatBox chatbox;
    [SerializeField] Rain rain;
    [SerializeField] Rain doubleRain;
    [SerializeField] GameObject _gameState;
    Dictionary<string, string> OptionsAndResponses = new Dictionary<string, string>();
    List<Vector3> _NeedsPlants = new List<Vector3>();
    public GameData gameData;
    
    

    String _instruction;
    public float timeLeft = 20.0f;
    int _days = 0;
    int _sacrificed = 0;
    int _harvested = 0;
    int _limit = 9;
    int _ending = 50;
    
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
                cx.UpdateScreen("--another day passes");
        }
    }

    string TextMatching(){
        string pattern = @"[a-z]+";
        Match m = Regex.Match(_instruction, pattern, RegexOptions.IgnoreCase);
        return VerifyInstructions(m.Value);
    }

    private void PopulateOptions(){
        OptionsAndResponses.Add("plant", "--you plant {0} corn into your plots");
        OptionsAndResponses.Add("water", "--you water {0} corn");
        OptionsAndResponses.Add("feed", "--you feed {0} corn");
        OptionsAndResponses.Add("harvest", "--you harvest {0} corn");
        OptionsAndResponses.Add("wait", "--one day passes");
        OptionsAndResponses.Add("sacrifice", "--you sacrifice {0} corn");
        OptionsAndResponses.Add("help", "--use 'plant' to plant corn" + '\n'
                                    + "--use 'water' to water your corn" + '\n'
                                    + "--use 'feed' to feed your corn" + '\n'
                                    + "--use 'harvest' to harvest your corn" + '\n'
                                    + "--use 'wait' to skip to the next day" + '\n'
                                    + "--use 'sacrifice' to sacrifice your corn" + '\n'
                                    + "--ex: 'plant a1 b1 b3' or 'water all'");
        OptionsAndResponses.Add("other", "--please use the format 'instruction a1 a2 a3'" + 
                                    '\n' + "--use the keyword help for a list of commands");
    }

    private string VerifyInstructions(string instruct) {
        string _otherResponse = "other";
        List<Plot> plots = FindPlots();
        String toFormat = "";
        var temporaryHarvest = 0;
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
                    toFormat = OptionsAndResponses[instruct];
                    return String.Format(toFormat, plots.Count);
                case "water":
                    foreach(Plot p in plots){
                        p.Water();
                    }
                    toFormat = OptionsAndResponses[instruct];
                    return String.Format(toFormat, plots.Count);
                case "feed":
                    toFormat = OptionsAndResponses[instruct];
                    return String.Format(toFormat, plots.Count);
                case "harvest":
                    foreach(Plot p in plots){
                        var worked = p.Harvested();
                        if (worked){
                            temporaryHarvest++;
                            _harvested++;
                        }
                    }
                    
                    toFormat = OptionsAndResponses[instruct];
                    return String.Format(toFormat, temporaryHarvest);
                case "sacrifice":
                    _sacrificed = _sacrificed + _harvested;
                    if(_sacrificed >= _limit){
                        _limit = _limit + 9;
                        rain.IncreaseRain();
                    }
                    if(_sacrificed > _ending - 10){
                        BeginTheEnd();
                    }
                    if(_sacrificed > _ending){
                        GameOver.Instance.InitiateGameOver(_days);
                        Restart();
                    }
                    toFormat = OptionsAndResponses[instruct];
                    var toReturn = String.Format(toFormat, _harvested);
                    _harvested = 0;
                    return toReturn;
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
            return "--another day passes";
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
        if(_instruction.Contains("all")){
            NeedChanges.Add(gameData.A1);
            NeedChanges.Add(gameData.A2);
            NeedChanges.Add(gameData.A3);
            NeedChanges.Add(gameData.B1);
            NeedChanges.Add(gameData.B2);
            NeedChanges.Add(gameData.B3);
            NeedChanges.Add(gameData.C1);
            NeedChanges.Add(gameData.C2);
            NeedChanges.Add(gameData.C3);
        }
        return NeedChanges;
        
    }

    private void Restart(){
        _harvested = 0;
        _sacrificed = 0;
        rain.RestartRain();
    }

    private void BeginTheEnd() {
        doubleRain.StartEndRain();
    }

}
