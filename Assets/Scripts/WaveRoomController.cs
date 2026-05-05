using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveRoomController : MonoBehaviour
{
    
    public Connector[] connectors;
    public List<GameObject> enemies;
    public Transform[] spawns;

    int alive;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        Close();

        foreach (var e in enemies)
        {
            Transform sp = spawns[Random.Range(0, spawns.Length)];

            GameObject go = Instantiate(e, sp.position, sp.rotation);

            var eh = go.GetComponent<EnemyHealth>();

            if (eh != null) eh.OnEnemyDeath += OnDeath;

            alive++;
        }

        yield return new WaitUntil(() => alive <= 0);

        Open();
    }

    void OnDeath() => alive--;

    void Close()
    {
        foreach (var c in connectors)
            if (c.role == ConnectorRole.Gate)
                c.SetState(ConnectorState.Closed);
    }

    void Open()
    {
        foreach (var c in connectors)
        {
            c.OnRoomCleared();
            c.UnlockShortcut();
        }
    }
}