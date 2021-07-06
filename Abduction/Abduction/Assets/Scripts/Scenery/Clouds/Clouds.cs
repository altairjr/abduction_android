using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [Header("Velocity multiplier")]
    public float multiplayerSpeed_;

    private float speed_ = 1;

    private bool innerConfiner_ = true; 

    private void Update()
    {
        CloudsMove();
        UpdateList();
    }

    private void CloudsMove()
    {
        float _speed;
        _speed = (speed_ * multiplayerSpeed_) * Time.deltaTime;
        gameObject.transform.Translate(new Vector2(_speed, 0));
    }

    private void UpdateList()
    {
        if(innerConfiner_ == false)
        {
            CloudController.clouds_.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ConfinerCloud"))
        {
            innerConfiner_ = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ConfinerCloud"))
        {
            innerConfiner_ = false;
        }
    }
}
