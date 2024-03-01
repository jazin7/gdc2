using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleUIController : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip SFXOk;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void BackToDungeon()
    {
        audioSource.PlayOneShot(SFXOk); 

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && GameManagerScript.instance != null)
        {
            player.transform.position = GameManagerScript.instance.playerPosition;
        }
        SceneManager.LoadScene("Dungeon");
    }
}
