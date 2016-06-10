using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public PlayerLight playerLight;

    IAttackable attacker;
    IDamagable damageControl;
    UpdatePlayerUI updateUI;
    PlayerType playerType;

    bool attack1, attack2, attack3;

    bool action;

    bool menuOpen;

    public Canvas menuCanvas;
    public Button[] attackButtons;
    public int currentButton;

    public bool testAttacks = false;
    void Awake()
    {
        playerLight = GetComponent<PlayerLight>();
        attacker = GetComponentInChildren<IAttackable>();
        damageControl = GetComponentInChildren<IDamagable>();
        updateUI = GetComponent<UpdatePlayerUI>();
    }

    void Start()
    {
        playerType = GetComponentInChildren<AttackController>().playerType;
        updateUI.SetMaxHealth(GameController.GetHealth(playerType));

    }

    public void OpenMenu()
    {
        menuOpen = true;
        menuCanvas.gameObject.SetActive(true);
        attackButtons[0].Select();
    }

    void Update()
    {
        updateUI.SetHealth(damageControl.GetHealth());
        if (!GameController.controller.playerTurn)
        {
            action = false;
        }

        if (testAttacks)
        {
            attack1 = Input.GetButtonDown("Fire1");
            attack2 = Input.GetButtonDown("Fire2");
            attack3 = Input.GetButtonDown("Fire3");
            if (attack1 || attack2 || attack3)
            {
                if (attack1) Attack(AttackType.Attack1);
                else if (attack2) Attack(AttackType.Attack2);
                else if (attack3) Attack(AttackType.Attack3);
                action = true;
            }
        }
    }

    public IEnumerator WaitForAction()
    {

        while (!action)
        {
            yield return null;
        }
        yield return null;
    }

    public void Attack(AttackType attack)
    {
        float mod = 0;
        switch (attack)
        {
            case AttackType.Attack1: mod = 0; break;
            case AttackType.Attack2: mod = 0.5f; break;
            case AttackType.Attack3: mod = 1f; break;
        }
        attacker.Attack(attack, GameController.controller.target, mod);
    }

    public void Attack(int attack)
    {
        switch (attack)
        {
            case 1: action = true; Attack(AttackType.Attack1); break;
            case 2: action = true; Attack(AttackType.Attack2); break;
            case 3: action = true; Attack(AttackType.Attack3); break;
        }
    }
}
