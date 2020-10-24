using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // found guidance on this at this link https://forum.unity.com/threads/using-a-button-to-switch-scenes.379945/
    public void NextScene(string name) {
        SceneManager.LoadScene(name);
    }
}
