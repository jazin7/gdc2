using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleUIController : MonoBehaviour
{
    public void BackToDungeon()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && GameManagerScript.instance != null)
        {
            player.transform.position = GameManagerScript.instance.playerPosition;
        }
        SceneManager.LoadScene("Dungeon");
    }
}
