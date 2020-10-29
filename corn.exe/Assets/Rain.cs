using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    // Start is called before the first frame update

    ParticleSystem rain;
    int currentEmissionRate;

    void Start()
    {
        rain = this.GetComponent<ParticleSystem>();
        currentEmissionRate = 5;
    }

    // Update is called once per frame
    void Update()
    {
    }

    //found how to work with runtime particles here:
    //https://forum.unity.com/threads/how-do-you-change-a-particle-systems-emission-rate-over-time-in-script.824292/
    public void IncreaseRain(){
        currentEmissionRate = currentEmissionRate * 4;
        var rainEmission = rain.emission;
        rainEmission.rateOverTime = currentEmissionRate;
    }

    public void RestartRain(){
        var rainEmission = rain.emission;
        rainEmission.rateOverTime = 5;
    }
}
