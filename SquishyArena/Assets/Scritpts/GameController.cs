using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public enum PlayerType
{
    Player,
    Enemy1,
    Enemy2,
    Enemy3,
    Enemy4
}

public enum AttackType
{
    Attack1,
    Attack2,
    Attack3
}
public class GameController : MonoBehaviour
{
    public static GameController controller;

    public IDamagable target;
    public IDamagable player;

    public Player playerObject;
    public Enemy enemyObject;

    public float attackTime = 1.5f;

    public string[] gameOverMessages = new string[] { "You lost.", "Game over.", "You're done.", "You suck :(" };
    public string[] winningMessages = new string[] { "You won!", "Good job.", "You're awesome.", "You are great :)" };

    public int winCount;
    public int enemyCount;

    void Awake()
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
        }
        else if (controller != this)
        {
            Destroy(gameObject);
        }

        playerObject = GameObject.FindObjectOfType<Player>();
        enemyObject = GameObject.FindObjectOfType<Enemy>();
        player = playerObject.GetComponentInChildren<IDamagable>();
        target = enemyObject.GetComponentInChildren<IDamagable>();
        StartCoroutine(AttackSequence());
    }

    public bool playerTurn = true;
    public int numberOfEnemies;
    string message;

    IEnumerator AttackSequence()
    {
        message = "Good luck, make you sure kill them all!";
        yield return StartCoroutine(MessageController.controller.StartDisplayMessage(message, 2f));
        for (int i = 0; i < numberOfEnemies; i++)
        {
 
            enemyObject.SetupEnemy();
            yield return StartCoroutine(SingleMatch());
            message = (player.IsDead() ? gameOverMessages[UnityEngine.Random.Range(0, winningMessages.Length)] : winningMessages[UnityEngine.Random.Range(0, winningMessages.Length)]);
            yield return StartCoroutine(MessageController.controller.StartDisplayMessage(message, 2f));
        }
        message = "Here's How It Went Down" + Environment.NewLine + "Battles: "+ enemyCount + Environment.NewLine + "Wins: "+ winCount + Environment.NewLine + "Awesome score: " + winCount*1f/enemyCount*1f ;
        yield return StartCoroutine(MessageController.controller.StartDisplayMessage(message, 10f));
    }


    IEnumerator SingleMatch()
    {
        enemyCount++;
        playerTurn = true;
        while (!player.IsDead() && !target.IsDead())
        {
            if (playerTurn)
            {
                // Let player choose action
                Debug.Log("Player turn");
                playerObject.playerLight.TurnOn();
                playerObject.OpenMenu();
                enemyObject.playerLight.TurnOff();
                yield return StartCoroutine(playerObject.WaitForAction());
            }
            else
            {
                Debug.Log("Enemy turn");
                enemyObject.playerLight.TurnOn();
                enemyObject.OpenMenu();
                playerObject.playerLight.TurnOff();
                yield return new WaitForSeconds(attackTime / 2f);
                enemyObject.Attack(AttackType.Attack1, true);
            }
            yield return new WaitForSeconds(attackTime / 2f);
            playerTurn = !playerTurn;
            //Take turns attacking
        }

        if (!player.IsDead())
        {
            winCount++;
            player.ResetHealth();
            target.ResetHealth();
        }
        else
        {
            //TODO death :)
        }
        yield return null;
    }

    public static float GetHealth(PlayerType type)
    {
        switch (type)
        {
            default:
            case PlayerType.Player: return 10f;
            case PlayerType.Enemy1: return 8f;
            case PlayerType.Enemy2: return 12f;
            case PlayerType.Enemy3: return 15f;
            case PlayerType.Enemy4: return 18f;
        }
    }

    public static float GetDamage(PlayerType type)
    {
        switch (type)
        {
            default:
            case PlayerType.Player: return 2f;
            case PlayerType.Enemy1: return 1f;
            case PlayerType.Enemy2: return 1f;
            case PlayerType.Enemy3: return 1.5f;
            case PlayerType.Enemy4: return 2f;
        }
    }
}
