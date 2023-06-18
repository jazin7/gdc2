using UnityEngine;

public class DungeonSceneLoading : MonoBehaviour
{
    void Start()
    {
        if (GameManagerScript.instance != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = GameManagerScript.instance.playerPosition;
        }
    }
}
