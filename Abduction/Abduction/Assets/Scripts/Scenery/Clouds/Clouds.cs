using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [Header("Velocity multiplier")]
    public float multiplayerSpeed_X_;
    public float multiplayerSpeed_Y_;
    private float speed_x_ = 1;
    private float speed_y_ = 1;

    private bool cloudsDown_;
    private float time_;

    private MeshRenderer cloudsMaterial_;

    private void Awake()
    {
        SetComponent();
    }
    private void Update()
    {
        MoveClouds();
    }

    private void SetComponent()
    {
        if(cloudsMaterial_ == null)
            cloudsMaterial_ = GetComponent<MeshRenderer>();
    }

    private void MoveClouds()
    {
        if (!GameController.startGame_)
        {
            speed_x_ = 1;
            cloudsMaterial_.material.mainTextureOffset += new Vector2((speed_x_ * multiplayerSpeed_X_) * Time.deltaTime, 0);
            cloudsDown_ = false;
            time_ = 0;
        }

        if(GameController.startGame_)
        {
            speed_x_ = 1;
            cloudsDown_ = true;
            cloudsMaterial_.material.mainTextureOffset += new Vector2((speed_x_ * multiplayerSpeed_X_) * Time.deltaTime, 0);
        }

        if (cloudsDown_)
        {
            time_ += Time.deltaTime;
        }

        if(time_ >= 5)
        {
            time_ = 5;
            speed_x_ = 0.2f;
            cloudsMaterial_.material.mainTextureOffset += new Vector2((speed_x_ * multiplayerSpeed_X_) * Time.deltaTime, (speed_y_ * multiplayerSpeed_Y_) * Time.deltaTime);
        }
    }
}
