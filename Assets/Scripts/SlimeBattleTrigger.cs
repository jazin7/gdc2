using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeBattleTrigger : MonoBehaviour
{
    public string enemyName;  // add this line
    private bool isPlayerInRange = false;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            // Save player position
            GameManagerScript.instance.playerPosition = GameObject.Find("Player").transform.position;

            // Save the name of the enemy to spawn
            PlayerPrefs.SetString("EnemyToSpawn", enemyName);

            // Load BattleScene
            SceneManager.LoadScene("BattleScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
