using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _highScoreCanvas;
    [SerializeField] private GameObject _highScoreText;
    [SerializeField] private GameObject _gameOverText;

    [SerializeField] private GameObject bird1;
    [SerializeField] private GameObject bird2;
    [SerializeField] private GameObject _pipeSpawner;

    public bool playing = false;

    public bool died = false;
    // Start is called before the first frame update
    private void Awake()
    {
        _highScoreCanvas.SetActive(false);
        if (instance == null)
        {
            instance = this;
        }

        Time.timeScale = 1f;
        bird1.SetActive(false);
        bird2.SetActive(false);
    }

    private void Update()
    {
        if (died)
        {
            if (Gamepad.all[0].aButton.wasPressedThisFrame)
            {
                RestartGame();
                return;
            }
        }
        if (bird1.activeSelf || bird2.activeSelf) return;
        if (Gamepad.all[0].startButton.wasPressedThisFrame || Gamepad.all[0].selectButton.wasPressedThisFrame)
        {
            // Switch players
            if (PlayerPrefs.HasKey("PlayerSwitched"))
            {
                if (PlayerPrefs.GetInt("PlayerSwitched") == 0)
                {
                    PlayerPrefs.SetInt("PlayerSwitched", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("PlayerSwitched", 0);
                }
            } else {
                PlayerPrefs.SetInt("PlayerSwitched", 1);
            }
            return;
        }
        if (Gamepad.all[0].aButton.wasPressedThisFrame)
        {
            bird1.SetActive(true);
            bird2.SetActive(true);
            _highScoreCanvas.SetActive(true);
            _gameOverCanvas.SetActive(false);
            playing = true;
            _pipeSpawner.SetActive(true);
        }
    }

    private int playersDead = 0;
    // Update is called once per frame
    public void Gameover(FlyBehavior flyBehavior)
    {
        GameObject.Find("Audio_Flap").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/die"));
        if (playersDead == 0)
        {
            playersDead++;
            flyBehavior.gameObject.SetActive(false);
            return;
        }
        playing = false;
        bird1.SetActive(false);
        bird2.SetActive(false);
        _gameOverCanvas.SetActive(true);
        _gameOverText.SetActive(true);
        died = true;
        if (PlayerPrefs.GetString("NewHighScore") == "true")
        {
            PlayerPrefs.DeleteKey("NewHighScore");
            //TODO: MOVE WINGS 
            _highScoreText.SetActive(true);
        }
        else
        {
            _highScoreText.SetActive(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
