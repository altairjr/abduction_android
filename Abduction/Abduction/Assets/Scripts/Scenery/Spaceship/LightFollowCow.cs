using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollowCow : MonoBehaviour
{
    [Header("Cow")]
    [SerializeField] private Transform cow_;

    [Header("Light abduction")]
    [SerializeField] private GameObject light2d_;

    [Header("Velocity light follow the cow")]
    public float speedMove_;

    private float time_;

    private void Update()
    {
        LightFollow();
        ActiveLight();
    }

    private void ActiveLight()
    {
        if (GameController.startGame_)
        {
            time_ += Time.deltaTime;
        }
        else
        {
            time_ = 0;
        }

        if(time_ >= 2)
        {
            light2d_.SetActive(true);
        }

        if(time_ == 0)
        {
            light2d_.SetActive(false);
        }
    }

    private void LightFollow()
    {
        light2d_.transform.rotation = Quaternion.Lerp(light2d_.transform.rotation, Quaternion.Euler(0, 0, (cow_.position.x * 15)), speedMove_ * Time.deltaTime);
    }
}
