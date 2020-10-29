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
        Instance = this;
    } 

    void Update() {
        if (Input.GetButtonDown("Fire2") && _isGameOver) {
            SceneManager.LoadScene("GameScene");
        }
    }

    public void InitiateGameOver() {
        _isGameOver = true;
        _gameOverText.SetActive(true);
    }
}
