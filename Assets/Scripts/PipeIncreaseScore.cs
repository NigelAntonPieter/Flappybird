using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeIncreaseScore : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Score.Instance.UpdateScore();
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (gameObject.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static)
                {
                    GameManager.instance.Gameover(gameObject.GetComponent<FlyBehavior>());
                }
            }
        }
    }
}
