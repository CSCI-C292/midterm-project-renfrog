using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public Plot A1;
    public Plot A2;
    public Plot A3;
    public Plot B1;
    public Plot B2;
    public Plot B3;
    public Plot C1;
    public Plot C2;
    public Plot C3;

    public List<Plot> plotList = new List<Plot>();

    void Start()
    {

        plotList.Add(A1);
        plotList.Add(A2);
        plotList.Add(A3);
        plotList.Add(B1);
        plotList.Add(B2);
        plotList.Add(B3);
        plotList.Add(C1);
        plotList.Add(C2);
        plotList.Add(C3);
        Begin();

    }

    private void Begin(){
        for(var i = 0; i < 9; i++){
            plotList[i].SetHealth(HealthStates.Dry);
            plotList[i].SetAge(PlantStates.Empty);
        }
    }
}
