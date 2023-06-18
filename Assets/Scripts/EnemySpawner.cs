using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject bat;
    public GameObject ghost;
    public GameObject bear;

    void Start()
    {
        string enemyToSpawn = PlayerPrefs.GetString("EnemyToSpawn");
        if (enemyToSpawn == "Bat")
        {
            bat.SetActive(true);
            ghost.SetActive(false);
            bear.SetActive(false);
        }
        else if (enemyToSpawn == "Ghost")
        {
            bat.SetActive(false);
            ghost.SetActive(true);
            bear.SetActive(false);
        }
        else if (enemyToSpawn == "Bear")
        {
            bat.SetActive(false);
            ghost.SetActive(false);
            bear.SetActive(true);
        }
    }


}
