using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour 
{

    public HealthStates _health;
    public PlantStates _age;
    public Vector3 position;
    public GameObject seedsPrefab;
    public GameObject youngPrefab;
    public GameObject adultPrefab;
    public GameObject wateredPlot;
    
    void Start()
    {
        _age = PlantStates.Empty;
        _health = HealthStates.Dry;
        Debug.Log("wow");
        seedsPrefab.SetActive(true);
        youngPrefab.SetActive(true);
        adultPrefab.SetActive(true);
        wateredPlot.SetActive(true);
        seedsPrefab.SetActive(false);
        youngPrefab.SetActive(false);
        adultPrefab.SetActive(false);
        wateredPlot.SetActive(false);
    }

    public void Water() {
        switch(_health){
            case HealthStates.Dry:
                _health = HealthStates.Watered;
                wateredPlot.SetActive(true);
                break;
            case HealthStates.Watered:
                _health = HealthStates.OverWatered;
                wateredPlot.SetActive(false);
                break;
            case HealthStates.OverWatered:
                _health = HealthStates.Drowned;
                break;
            default:
                break;
        }
    }

    public void Harvest(){
        _age = PlantStates.Empty;
        seedsPrefab.SetActive(false);
        youngPrefab.SetActive(false);
        adultPrefab.SetActive(false);

    }

    public void Plant(){
        if (_age == PlantStates.Empty){
            _age = PlantStates.Seeds;
            seedsPrefab.SetActive(true);
        }
    }

    public void Grow(){
        if(_health != HealthStates.Dry){
            switch(_age){
                case PlantStates.Seeds:
                    _age = PlantStates.Young;
                    seedsPrefab.SetActive(false);
                    youngPrefab.SetActive(true);
                    break;
                case PlantStates.Young:
                    _age = PlantStates.Adult;
                    youngPrefab.SetActive(false);
                    adultPrefab.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        _health = HealthStates.Dry;
        wateredPlot.SetActive(false);
    }

    public void SetPosition(Vector3 pos){
        position = pos;
    }

    public Vector3 GetPosition(){
        return position;
    }

    public HealthStates GetHealth(){
        return _health;
    }

    public PlantStates GetAge(){
        return _age;
    }

    public void SetHealth(HealthStates health){
        _health = health;
    }

    public void SetAge(PlantStates age){
        _age = age;
    }
}
