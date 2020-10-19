using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BeginTiming();
    }

    // Update is called once per frame
    void Update()
    {
        //look if "wait" was user, then suspend co routine
    }

    void BeginTiming() {
        StartCoroutine(Wait20Seconds());
    }

    IEnumerator Wait20Seconds() {
        Debug.Log("0 seconds");

        yield return new WaitForSeconds(20);

        Debug.Log("20 seconds");

        BeginTiming();
    }
}
