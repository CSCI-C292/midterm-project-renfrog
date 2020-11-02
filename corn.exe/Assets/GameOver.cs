using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject _gameOverText;

    int _score = 0;

    bool _isGameOver = false;
    
    public static GameOver Instance;

    void Awake() {
        _gameOverText.SetActive(true);
        _gameOverText.SetActive(false);
        Instance = this;
    } 

    void Update() {
        if (Input.GetButtonDown("Fire2") && _isGameOver) {
            SceneManager.LoadScene("GameScene");
        }
    }

    public void InitiateGameOver(int days) {
        _isGameOver = true;
        _gameOverText.GetComponent<Text>().text = "You have flooded the state of Indiana" + "\n" + "in " + days + " days." 
                               + "\n" + "Press 'r' if you would like to begin again " + "\n" + "and flood Indiana faster.";
        Debug.Log(_gameOverText);
        _gameOverText.SetActive(true);
    }
}
