using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score Instance;

    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;

    private int _score;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
   private void Start()
   {
        _currentScoreText.text = _score.ToString();
        _highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        UpdateHighScore();
   }

    // Update is called once per frame
   private void UpdateHighScore()
   {
        if (_score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetString("NewHighScore", "true");
            PlayerPrefs.SetInt("HighScore", _score);
            _highScoreText.text = _score.ToString();
        }
   }

    public void UpdateScore()
    {
        GameObject.Find("Audio_Flap").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/point"));
        _score++;
        _currentScoreText.text = _score.ToString();
        UpdateHighScore();
    }

    public int GetScore()
    {
        return _score;
    }

}
