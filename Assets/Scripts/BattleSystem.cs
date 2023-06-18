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


    private void UpdateUI()
    {
        playerHealthUI.text = "Player HP: " + playerHealth;
        enemyHealthUI.text = "Enemy HP: " + enemyHealth;
    }

    public void PlayerAttack()
    {
        int damage = Random.Range(1, 3);
        enemyHealth -= damage;
        battleMessage.text = "<color=lightblue>You hit the enemy for " + damage + " damage.</color>";

        playerIsDefending = false; // The player is not defending anymore after attacking

        if (enemyHealth <= 0)
        {
            EndTurn();
            return;
        }

        EnemyTurn();
        UpdateUI();
    }

    public void PlayerHeal()
    {
        int healAmount = 5;
        playerHealth += healAmount;

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
        battleMessage.text = "<color=lightblue>You are defending.</color>";

        EnemyTurn();

        UpdateUI();
    }

    private void EnemyTurn()
    {
        if (enemyIsCharging)
        {
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
                // Enemy does a normal attack
                int damage = playerIsDefending ? 0 : Random.Range(2, 5);
                playerHealth -= damage;
                battleMessage.text += "<color=#FF8080FF>\nThe enemy hit you for " + damage + " damage.</color>";
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

    }


    private IEnumerator EndBattle(string endMessage)
    {
        battleMessage.text = endMessage;

        attackButton.interactable = false;
        healButton.interactable = false;
        defendButton.interactable = false;
        runButton.interactable = false;

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Dungeon");
    }

    public void EndTurn() // Call this method when the enemy's health reaches 0
    {
        attackButton.interactable = false;
        healButton.interactable = false;
        defendButton.interactable = false;
        runButton.interactable = false;
        StartCoroutine(EndBattle("<color=green>You won the battle!</color>"));
    }
}
