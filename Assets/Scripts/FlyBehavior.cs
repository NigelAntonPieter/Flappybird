using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


public class FlyBehavior : MonoBehaviour
{
    [SerializeField] private float _velocity = 1.5f;
    [SerializeField] private float __rotationSpeed = 10f;
    [SerializeField] private int _playerId = 0;
    
    private Rigidbody2D _rb;

    private List<ButtonControl> buttons = new List<ButtonControl>();


    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    private void Update()
    {
        int selectedPlayer = _playerId;
        if (PlayerPrefs.HasKey("PlayerSwitched"))
        {
            if (PlayerPrefs.GetInt("PlayerSwitched") == 1)
            {
                selectedPlayer = 1 - _playerId;
            }
        }
        buttons.Clear();
        buttons.Add(Gamepad.all[selectedPlayer].aButton);
        buttons.Add(Gamepad.all[selectedPlayer].bButton);
        buttons.Add(Gamepad.all[selectedPlayer].xButton);
        buttons.Add(Gamepad.all[selectedPlayer].yButton);
        buttons.Add(Gamepad.all[selectedPlayer].leftStickButton);
        buttons.Add(Gamepad.all[selectedPlayer].rightStickButton);
        buttons.Add(Gamepad.all[selectedPlayer].buttonEast);
        buttons.Add(Gamepad.all[selectedPlayer].buttonSouth);
        buttons.Add(Gamepad.all[selectedPlayer].buttonNorth);
        buttons.Add(Gamepad.all[selectedPlayer].buttonWest);
        foreach (ButtonControl button in buttons)
        {
            if (button.wasPressedThisFrame)
            {
                if (_rb.bodyType == RigidbodyType2D.Static)
                {
                    _rb.bodyType = RigidbodyType2D.Dynamic;
                }
                _rb.linearVelocity = Vector2.up * _velocity;

                GameObject.Find("Audio_Flap").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/flap"), 0.2f);
                gameObject.GetComponent<Animator>().SetTrigger("Flap");
            }
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, _rb.linearVelocity.y * __rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") return;
        GameManager.instance.Gameover(this);
    }
}