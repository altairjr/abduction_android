using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [Header("Time to Spawn clouds")]
    public float timeSpawn_;

    [Header("Prefab Cloud")]
    [SerializeField] private GameObject[] cloud_;

    [Header("Spawn Point Cloud")]
    [SerializeField] private Transform spawnPoint_01;
    [SerializeField] private Transform spawnPoint_02;

    [Header("Parent Spawn Clouds")]
    [SerializeField] private GameObject packClouds_;

    private float timeCountSpawn_;
    public static List<GameObject> clouds_ = new List<GameObject>();

    private void Update()
    {
        SpawnClouds();
    }

    private void SpawnClouds()
    {
        float _point_01 = spawnPoint_01.position.y;
        float _point_02 = spawnPoint_02.position.y;

        var position = new Vector3(spawnPoint_01.position.x, Random.Range(spawnPoint_01.position.y, spawnPoint_02.position.y), 0);

        for (int i = 0; i < cloud_.Length; i++)
        {
            if (clouds_.Count <= 15)
            {
                timeCountSpawn_ += Time.deltaTime;
                if (timeCountSpawn_ >= timeSpawn_)
                {
                    timeCountSpawn_ = 0;
                    clouds_.Add(Instantiate(cloud_[i], position, Quaternion.identity, packClouds_.transform.parent));
                }
            }
        }
    }
}
