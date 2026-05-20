/*using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [Header("UI")]
    public CassandraUI cassandraUI;
    public HintUI hintUI;
    public ObjectiveUI objectiveUI;

    [Header("Tutorial State")]
    public TutorialState currentState;

    private bool weaponTutorialShown = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetState(TutorialState.WakeUp);
    }

    public void SetState(TutorialState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case TutorialState.WakeUp:

                objectiveUI.SetObjective(
                    "ESCAPA DE LA IGLESIA"
                );

                hintUI.ShowHint(
                    "",
                    3f
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "¿Me recibes?... Bien.",
                    "Pensé que se había cortado la comunicación."
                );

                break;

            case TutorialState.Move:

                hintUI.ShowHint(
                    "WASD - MOVERSE",
                    3f
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Muévete. El sector colapsará."
                );

                break;

            case TutorialState.Run:

                hintUI.ShowHint(
                    "SHIFT - CORRER",
                    3f
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Corre. Ahora."
                );

                break;

            case TutorialState.Jump:

                hintUI.ShowHint(
                    "SPACE - SALTAR",
                    3f
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Salta o muere."
                );

                break;

            case TutorialState.FirstCombat:

                objectiveUI.SetObjective(
                    "ELIMINA AL DEMONIO"
                );

                hintUI.ShowHint(
                    "CLICK IZQUIERDO - DISPARAR",
                    3f
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Movimiento al frente. No dudes."
                );

                break;

            case TutorialState.WeaponSwitch:

                objectiveUI.SetObjective(
                    "CAMBIA DE ARMA"
                );

                hintUI.ShowHint(
                    "X - CAMBIAR ARMA",
                    4f
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "El rifle no penetrará ese escudo.",
                    "Puedes usar tu escopeta o las granadas"
                );

                break;

            case TutorialState.Power:

                objectiveUI.SetObjective(
                    "USA EL FUEGO SAGRADO"
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "...Eso no debería existir."
                );

                break;

            case TutorialState.Boss:

                objectiveUI.SetObjective(
                    "DERROTA AL HERALDO INFERNAL"
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Esa cosa controla esta área."
                );

                break;

            case TutorialState.Finished:

                objectiveUI.SetObjective(
                    "TUTORIAL COMPLETADO"
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Heraldo derrotado, buen trabajo.",
                    "Ahora adéntrate en el pueblo",
                    "y busca al General de esta Invasión."
                );

                break;
        }
    }

    public void WrongWeaponUsed()
    {
        if (weaponTutorialShown)
            return;

        weaponTutorialShown = true;

        SetState(TutorialState.WeaponSwitch);

        cassandraUI.ShowDialogue(
            "CASSANDRA",
            "Cambia de arma. Ahora."
        );
    }
}

public enum TutorialState
{
    WakeUp,
    Move,
    Run,
    Jump,
    FirstCombat,
    WeaponSwitch,
    Power,
    Boss,
    Finished
}*/
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [Header("UI")]
    public CassandraUI cassandraUI;
    public HintUI hintUI;
    public ObjectiveUI objectiveUI;

    [Header("Tutorial State")]
    public TutorialState currentState;

    private bool weaponTutorialShown = false;
    private bool firstEnemyKilled = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetState(TutorialState.WakeUp);
    }

    public void SetState(TutorialState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case TutorialState.WakeUp:

                objectiveUI.SetObjective(
                    "ESCAPA DE LA IGLESIA"
                );

                hintUI.ShowHint(
                    "",
                    3f
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "¿Me recibes?... Bien.",
                    "Pensé que se había cortado la comunicación."
                );

                break;

            case TutorialState.Move:

                hintUI.ShowHint(
                    "WASD - MOVERSE",
                    3f
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Muévete. El sector colapsará."
                );

                break;

            case TutorialState.Run:

                hintUI.ShowHint(
                    "SHIFT - CORRER",
                    3f
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Corre. Ahora."
                );

                break;

            case TutorialState.Jump:

                hintUI.ShowHint(
                    "SPACE - SALTAR",
                    3f
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Salta o muere."
                );

                break;

            case TutorialState.FirstCombat:

                objectiveUI.SetObjective(
                    "ELIMINA AL DEMONIO"
                );

                hintUI.ShowHint(
                    "CLICK IZQUIERDO - DISPARAR",
                    3f
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Movimiento al frente. No dudes."
                );

                break;

            case TutorialState.WeaponSwitch:

                objectiveUI.SetObjective(
                    "CAMBIA DE ARMA"
                );

                hintUI.ShowHint(
                    "X - CAMBIAR ARMA",
                    4f
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "El rifle no penetrará ese escudo.",
                    "Puedes usar tu escopeta o las granadas"
                );

                break;

            /*
            case TutorialState.Power:

                objectiveUI.SetObjective(
                    "USA EL FUEGO SAGRADO"
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "...Eso no debería existir."
                );

                break;
            */

            case TutorialState.Boss:

                objectiveUI.SetObjective(
                    "DERROTA AL HERALDO INFERNAL"
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Esa cosa controla esta área."
                );

                break;

            case TutorialState.Finished:

                objectiveUI.SetObjective(
                    "TUTORIAL COMPLETADO"
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Heraldo derrotado, buen trabajo.",
                    "Ahora adéntrate en el pueblo",
                    "y busca al General de esta invasión."
                );

                break;
        }
    }

    // =========================================================
    // WRONG WEAPON
    // =========================================================

    public void WrongWeaponUsed()
    {
        if (weaponTutorialShown)
            return;

        weaponTutorialShown = true;

        SetState(TutorialState.WeaponSwitch);

        cassandraUI.ShowDialogue(
            "CASSANDRA",
            "Cambia de arma. Ahora."
        );
    }

    // =========================================================
    // ENEMY DAMAGED
    // =========================================================

    public void EnemyDamaged()
    {
        Debug.Log("Enemy damaged.");
    }

    // =========================================================
    // ENEMY KILLED
    // =========================================================

    public void EnemyKilled()
    {
        if (firstEnemyKilled)
            return;

        firstEnemyKilled = true;

        Debug.Log("Enemy killed.");

        if (currentState == TutorialState.FirstCombat)
        {
            cassandraUI.ShowDialogue(
                "CASSANDRA",
                "Objetivo eliminado.",
                "Continúa avanzando."
            );
        }
    }
}

public enum TutorialState
{
    WakeUp,
    Move,
    Run,
    Jump,
    FirstCombat,
    WeaponSwitch,
    Power,
    Boss,
    Finished
}