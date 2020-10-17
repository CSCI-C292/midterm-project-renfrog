using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{

    public HealthStates _health;
    public PlantStates _age;

    // Start is called before the first frame update
    void Start()
    {
        _age = PlantStates.Empty;
        _health = HealthStates.Dry;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Water() {
        switch(_health){
            case HealthStates.Dry:
                _health = HealthStates.Watered;
                break;
            case HealthStates.Watered:
                _health = HealthStates.OverWatered;
                break;
            case HealthStates.OverWatered:
                _health = HealthStates.Drowned;
                break;
            default:
                break;
        }
    }

    void Harvest(){
        _age = PlantStates.Empty;
    }
}
