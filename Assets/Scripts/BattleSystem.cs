using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    public TextMeshProUGUI battleMessage;
    public TextMeshProUGUI playerHealthUI;
    public TextMeshProUGUI enemyHealthUI;

    private int playerHealth = 10;
    private int enemyHealth = 20;

    private bool enemyIsCharging = false;
    private bool playerIsDefending = false;

    public Button attackButton;
    public Button healButton;
    public Button defendButton;
    public Button runButton;

    public Animator enemyAnimator;
    public Animator enemyAnimator2;
    public Animator enemyAnimator3;


    public AudioClip SFXJump;
    public AudioClip SFXSword;
    public AudioClip SFXHit;
    public AudioClip SFXHit2;
    public AudioClip SFXAlert;

    private AudioSource audioSource;


    public Image playerHealthBar;
    public Image enemyHealthBar;

    private const float MAX_HEALTH_BAR_WIDTH = 300;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void UpdateUI()
    {
        playerHealthUI.text = "Player HP: " + playerHealth;
        enemyHealthUI.text = "Enemy HP: " + enemyHealth;


        float playerHealthPercentage = (float)playerHealth / 10; // assuming max player health is 10
        float enemyHealthPercentage = (float)enemyHealth / 20; // assuming max enemy health is 20

        playerHealthBar.rectTransform.sizeDelta = new Vector2(MAX_HEALTH_BAR_WIDTH * playerHealthPercentage, playerHealthBar.rectTransform.sizeDelta.y);
        enemyHealthBar.rectTransform.sizeDelta = new Vector2(MAX_HEALTH_BAR_WIDTH * enemyHealthPercentage, enemyHealthBar.rectTransform.sizeDelta.y);


    }

    public void PlayerAttack()
    {
        ButtonDisable();
        audioSource.PlayOneShot(SFXHit); // Play SFXHit when the player attacks

        int damage = Random.Range(1, 3);
        enemyHealth -= damage;
        enemyAnimator.SetBool("isHit", true);
        enemyAnimator2.SetBool("isHit", true);
        enemyAnimator3.SetBool("isHit", true);
        StartCoroutine(ResetHitAnimation());
        battleMessage.text = "<color=lightblue>You hit the enemy for " + damage + " damage.</color>";

        playerIsDefending = false; // The player is not defending anymore after attacking

        if (enemyHealth <= 0)
        {
            EndTurn();
            return;
        }
        StartCoroutine(WaitThenEnemyTurn());
    }

    private IEnumerator WaitThenEnemyTurn()
    {
        yield return new WaitForSeconds(2.0f); // adjust delay as needed
        EnemyTurn();
        UpdateUI();
    }



    public void PlayerHeal()
    {
        int healAmount = 5;
        playerHealth += healAmount;

        audioSource.PlayOneShot(SFXJump); // Play SFXJump when the player heals


        if (playerHealth > 10)
        {
            playerHealth = 10;
        }

        battleMessage.text = "<color=lightblue>You healed yourself for " + healAmount + " health.</color>";

        playerIsDefending = false; // The player is not defending anymore after healing
        EnemyTurn();

        UpdateUI();
    }

    public void PlayerDefend()
    {
        playerIsDefending = true;
        audioSource.PlayOneShot(SFXSword); // Play SFXSword when the player defends

        battleMessage.text = "<color=lightblue>You are defending.</color>";

        EnemyTurn();

        UpdateUI();
    }

    private void EnemyTurn()
    {
        ButtonDisable();

        if (enemyIsCharging)
        {
            audioSource.PlayOneShot(SFXAlert); // Play SFXAlert when the enemy is charging
            enemyAnimator.SetBool("usesSpecial", true);
            enemyAnimator2.SetBool("usesSpecial", true);
            enemyAnimator3.SetBool("usesSpecial", true);
            StartCoroutine(ResetSpecialAttackAnimation());

            if (playerIsDefending)
            {
                battleMessage.text += "<color=lightblue>\nYou defended the enemy's attack!</color>";
            }
            else
            {

                playerHealth -= 10;
                battleMessage.text += "<color=#FF8080FF>\nThe enemy hit you with the special Attack!</color>";
            }

            enemyIsCharging = false;
            playerIsDefending = false; // The player is not defending anymore after the enemy's turn
        }
        else
        {
            float chance = Random.value; // Generates a random value between 0 and 1

            if (chance <= 0.8f)
            {
                audioSource.PlayOneShot(SFXHit2); // Play SFXHit2 when the enemy does a normal attack

                // Enemy does a normal attack
                int damage = playerIsDefending ? 0 : Random.Range(2, 5);
                playerHealth -= damage;
                battleMessage.text += "<color=#FF8080FF>\nThe enemy hit you for " + damage + " damage.</color>";
                enemyAnimator.SetBool("usesAttack", true);
                enemyAnimator2.SetBool("usesAttack", true);
                enemyAnimator3.SetBool("usesAttack", true);
                StartCoroutine(ResetAttackAnimation());
            }
            else
            {
                // Enemy starts charging its attack
                enemyIsCharging = true;
                battleMessage.text += "<color=#FF8080FF>\nThe enemy is charging its attack!</color>";
            }

            playerIsDefending = false; // The player is not defending anymore after the enemy's turn
        }

        if (playerHealth <= 0)
        {
            StartCoroutine(EndBattle("<color=red>You lost the battle...</color>"));
        }
        else if
        (
           (!enemyAnimator.GetBool("usesAttack") && !enemyAnimator2.GetBool("usesAttack") && !enemyAnimator3.GetBool("usesAttack"))
           &&
           (!enemyAnimator.GetBool("usesSpecial") && !enemyAnimator2.GetBool("usesSpecial") && !enemyAnimator3.GetBool("usesSpecial"))
        )
        {
            ButtonEnable();
        }


    }




    private IEnumerator EndBattle(string endMessage)
    {
        battleMessage.text = endMessage;
        audioSource.volume = 0.2f;

        ButtonDisable();

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Dungeon");
    }

    public void EndTurn() // Call this method when the enemy's health reaches 0
    {
        ButtonDisable();
        StartCoroutine(EndBattle("<color=green>You won the battle!</color>"));
    }


    private IEnumerator ResetHitAnimation()
    {
        yield return new WaitForSeconds(1.0f); // Wait for one second
        enemyAnimator.SetBool("isHit", false);
        enemyAnimator2.SetBool("isHit", false);
        enemyAnimator3.SetBool("isHit", false);
    }

    private IEnumerator ResetAttackAnimation()
    {
        yield return new WaitForSeconds(1.0f); // Wait for 1 second
        enemyAnimator.SetBool("usesAttack", false);
        enemyAnimator2.SetBool("usesAttack", false);
        enemyAnimator3.SetBool("usesAttack", false);
        ButtonEnable();
    }

    private IEnumerator ResetSpecialAttackAnimation()
    {
        yield return new WaitForSeconds(5.0f);
        enemyAnimator.SetBool("usesSpecial", false);
        enemyAnimator2.SetBool("usesSpecial", false);
        enemyAnimator3.SetBool("usesSpecial", false);
        ButtonEnable();
    }


    private void ButtonDisable()
    {
        attackButton.interactable = false;
        healButton.interactable = false;
        defendButton.interactable = false;
        runButton.interactable = false;
    }

    private void ButtonEnable()
    {
        if (playerHealth > 0 && enemyHealth > 0)
        {
            attackButton.interactable = true;
            healButton.interactable = true;
            defendButton.interactable = true;
            runButton.interactable = true;
        }
    }
}
