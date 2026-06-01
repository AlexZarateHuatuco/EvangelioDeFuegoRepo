
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
                    "WASD - MOVERSE",
                    4f
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "¿Me recibes?... Bien.",
                    "Pensé que se había cortado la comunicación.",
                    "Muévete, debemos cerrar el portal."
                );

                break;

            case TutorialState.RoomClose:

                objectiveUI.SetObjective(
                    "LIBERA LA ZONA"
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Para liberar este cuarto necesitas derrotar a todos los enemigos de la sala."
                );

                break;

            case TutorialState.FirstCombat:

                objectiveUI.SetObjective(
                    "ELIMINA AL DEMONIO"
                );

                hintUI.ShowHint(
                    "CLICK IZQUIERDO - DISPARAR",
                    4f
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Uso de armas básicas autorizado",
                    "Puedes usar tu rifle para eliminarlos."
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
                    "Enemigo nuevo identificado",
                    "El rifle no penetrará su escudo.",
                    "Puedes usar tu escopeta o las granadas"
                );

                break;

            case TutorialState.MiniBoss:

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

            case TutorialState.SecretRoom:

                objectiveUI.SetObjective(
                    "CUARTO SECRETO ENCONTRADO"
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Vaya, encontraste un cuarto secreto",
                    "Veamos que ocultaron aquí"
                );

                break;

            case TutorialState.BossLair:

                objectiveUI.SetObjective(
                    "SALA DE JEFE APROXIMÁNDOSE"
                );

                cassandraUI.ShowDialogue(
                    "CASSANDRA",
                    "Estás acercándote a la guarida del General",
                    "Asegúrate de estar preparado"
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
            "Esa arma es inutil contra este enemigo, cambia de arma."
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
    FirstCombat,
    WeaponSwitch,
    RoomClose,
    MiniBoss,
    Finished,
    SecretRoom,
    BossLair
}