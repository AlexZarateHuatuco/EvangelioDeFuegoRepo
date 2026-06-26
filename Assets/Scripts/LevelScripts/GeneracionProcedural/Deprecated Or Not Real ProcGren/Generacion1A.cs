using UnityEngine;

public class Generacion1A : MonoBehaviour
{
    int Eleccion;
    public float Generaacion1X, Generaacion1Y, Generaacion1Z;
    public float Generaacion1RotacionX, Generaacion1RotacionY, Generaacion1RotacionZ;
    public float Generaacion2X, Generaacion2Y, Generaacion2Z;
    public float Generaacion2RotacionX, Generaacion2RotacionY, Generaacion2RotacionZ;
    public float Generaacion3X, Generaacion3Y, Generaacion3Z;
    public float Generaacion3RotacionX, Generaacion3RotacionY, Generaacion3RotacionZ;
    public GameObject[] salas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Eleccion = Random.Range(0, salas.Length);

        if(Eleccion == 0)
        {
            Instantiate(salas[Eleccion], new Vector3(Generaacion1X, Generaacion1Y, Generaacion1Z), Quaternion.Euler(Generaacion1RotacionX, Generaacion1RotacionY, Generaacion1RotacionZ));
        }
        else if(Eleccion == 1)
        {
            Instantiate(salas[Eleccion], new Vector3(Generaacion2X, Generaacion2Y, Generaacion2Z), Quaternion.Euler(Generaacion2RotacionX, Generaacion2RotacionY, Generaacion2RotacionZ));
        }
        else
        {
            Instantiate(salas[Eleccion], new Vector3(Generaacion3X, Generaacion3Y, Generaacion3Z), Quaternion.Euler(Generaacion3RotacionX, Generaacion3RotacionY, Generaacion3RotacionZ));
        }


    }
}
