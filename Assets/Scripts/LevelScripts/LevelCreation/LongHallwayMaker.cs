using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LongHallwayMaker : MonoBehaviour
{
    //Ejejem!!! Presenting. Le Variables du nivelat. danke. dank u. oh np np
    [SerializeField] private GameObject[] recoveryRooms; //Church
    [SerializeField] private GameObject[] combatRooms; //Town
    [SerializeField] private GameObject[] portalRooms; //boss but portal
    [SerializeField] private string sceneToLoad = "DinnerTimeOrReplacemeIguess";//modificar anterior con escena d victoria

    [SerializeField] private int difficultySlider = 0; //aka: cuantos kuartos crear.... di is no procgen but. eh. valor inicial es 1!
                                                       

    private List<GameObject> spawnedRooms = new List<GameObject>();

    [SerializeField] private bool generateOnStart = true; //debug!!! no activar en el build!!!


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (generateOnStart == true) 
        {
            GenerateFloor();
        }
        else
        {
            NextFloor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextFloor()
    {
        difficultySlider++;
        if (difficultySlider < 5)
        {
            SceneManager.LoadScene(sceneToLoad);

        }
        else
        { 
            GenerateFloor();
        }
    }

    public void GenerateFloor()
    {
        for (int i = 0; i < spawnedRooms.Count; i++)
        {
            Destroy(spawnedRooms[i]);
        }
        spawnedRooms.Clear();

        //reposo no 1
        GameObject recoveryRoomOne = SpawnRandomRoom(recoveryRooms, Vector3.zero, Quaternion.identity);
        spawnedRooms.Add(recoveryRoomOne);
        Transform previousExit = recoveryRoomOne.transform.Find("exit");
        //attaque no 1
        for (int i = 0; i < difficultySlider; i++)
        {
            GameObject combatRoom = SpawnRandomRoom(combatRooms);
            AlignToDoor(combatRoom, previousExit);
            spawnedRooms.Add(combatRoom);
            previousExit = combatRoom.transform.Find("exit");
        }
        //reposo no 2
        GameObject secondRecovery = SpawnRandomRoom(recoveryRooms);
        AlignToDoor(secondRecovery, previousExit);
        spawnedRooms.Add(secondRecovery);
        previousExit = secondRecovery.transform.Find("exit");

        //combate no 2
        for (int i = 0; i < difficultySlider + 1; i++)
        {
            GameObject combatRoom = SpawnRandomRoom(combatRooms);
            AlignToDoor(combatRoom, previousExit);
            spawnedRooms.Add(combatRoom);
            previousExit = combatRoom.transform.Find("exit");
        }
        // portal room!!!!1!1!!
        GameObject portal = SpawnRandomRoom(portalRooms);
        AlignToDoor(portal, previousExit);
        spawnedRooms.Add(portal);

    }


    private GameObject SpawnRandomRoom(GameObject[] prefabArray, Vector3 position = default, Quaternion rotation = default)
    {
        if (prefabArray == null || prefabArray.Length == 0)
        {
            Debug.LogError("Ermmm... please load me up with rooms i beg ;-;");
            return null;
        }

        int index = Random.Range(0, prefabArray.Length);
        return Instantiate(prefabArray[index], position, rotation);
    }

    private void AlignToDoor(GameObject newRoom, Transform previousExit)
    {
        Transform door = newRoom.transform.Find("door");
        if (door == null)
        {
            Debug.LogError($"Room '{newRoom.name}' has no 'door' child!");
            return;
        }


        Vector3 offset = newRoom.transform.position - door.position;
        newRoom.transform.position = previousExit.position + offset;
    }

}
