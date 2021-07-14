using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [Header("Set velocity obstacles")]
    public float velocityMovementObstacle;

    void Update()
    {
        ControllerObstacles();
    }

    #region Controll Obstacles

    void ControllerObstacles()
    {
        gameObject.transform.Translate(0, velocityMovementObstacle * Time.deltaTime, 0);
    }

    #endregion

    #region System Trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Endzone"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
