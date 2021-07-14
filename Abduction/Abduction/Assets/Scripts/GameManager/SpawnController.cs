using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    //Posição do spawn de objetos
    [Header("Points of Spawn Obstacle")]
    [SerializeField] private Transform spawnRigh;
    [SerializeField] private Transform spawnLeft;
    [SerializeField] private Transform instatiatePos_;

    //Posição onde o obstaculo vai spawnar
    private Vector3 spawnPoint;

    //Array para adicionar os obstaculos
    [Header("Obstacles of Spawn")]
    [SerializeField] private GameObject[] obstacle;

    //Array para adicionar os coletaveis
    [Header("Collectable of spawn")]
    [SerializeField] private GameObject[] collectable;

    //Temporizador de quanto em quanto tempo vai ter um obstaculo sendo spawnado
    private float couldownSpawnTimeObstacle = 1f;
    private float spawnTimeObstacle = 0;


    //Temporizador de quanto em quanto muda a posição do spawn dos obstaculos
    private float timeObstacle = 0;
    private float maxTimeObstacle = 0.5f;
    private int positionObstacle;

    //Temporizador de quanto em quanto tempo vai ter um coletavel sendo spawnado
    private float couldownSpawnTimeCollecatable = 1f;
    private float spawnTimeCollectable = 0;

    //Temporizador de quanto em quanto muda a posição do spawn dos obstaculos
    private float timeCollectable = 0;
    private float maxTimeCollectable = 0.5f;
    private int positionCollectable;

    void Update()
    {
        if (GameController.startGame_ && GameController.cutSceneStartGameRunnable_)
        {
            SpawnObstacles();
            SpawnCollectables();
        }
    }

    void SpawnObstacles()
    {
        timeObstacle += Time.deltaTime;
        if (timeObstacle >= maxTimeObstacle)
        {
            timeObstacle = 0;
            positionObstacle = Random.Range(0, 2);
        }

        if (positionObstacle == 0)
        {
            spawnPoint = spawnRigh.transform.position;
        }
        else if (positionObstacle == 1)
        {
            spawnPoint = spawnLeft.transform.position;
        }

        spawnTimeObstacle += Time.deltaTime;
        if (spawnTimeObstacle >= couldownSpawnTimeObstacle)
        {
            spawnTimeObstacle = 0;
            Instantiate(obstacle[0], spawnPoint, Quaternion.identity, instatiatePos_.transform.parent);
        }
    }

    void SpawnCollectables()
    {
        timeCollectable += Time.deltaTime;
        if (timeCollectable >= maxTimeCollectable)
        {
            timeCollectable = 0;
            positionCollectable = Random.Range(0, 2);
        }

        if (positionCollectable == 0 && positionObstacle == 1)
        {
            spawnPoint = spawnRigh.transform.position;
        }
        else if (positionCollectable == 1 && positionObstacle == 0)
        {
            spawnPoint = spawnLeft.transform.position;
        }

        spawnTimeCollectable += Time.deltaTime;
        if (spawnTimeCollectable >= couldownSpawnTimeCollecatable)
        {
            spawnTimeCollectable = 0;
            Instantiate(collectable[0], spawnPoint, Quaternion.identity, instatiatePos_.transform.parent);
        }
    }
}
