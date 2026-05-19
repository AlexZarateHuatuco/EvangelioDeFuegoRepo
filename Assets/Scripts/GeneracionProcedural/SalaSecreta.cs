using UnityEngine;

public class SalaSecreta : MonoBehaviour
{
    int Eleccion;
    public float Generaacion1X, Generaacion1Y, Generaacion1Z;
    public float Generaacion2X, Generaacion2Y, Generaacion2Z;
    public float Generaacion2RotacionX, Generaacion2RotacionY, Generaacion2RotacionZ;
    public GameObject[] salasSecreta;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Eleccion = Random.Range(0, salasSecreta.Length);

        if (Eleccion == 0)
        {
            Instantiate(salasSecreta[Eleccion], new Vector3(Generaacion1X, Generaacion1Y, Generaacion1Z), Quaternion.identity);
        }
        else if (Eleccion == 1)
        {
            Instantiate(salasSecreta[Eleccion], new Vector3(Generaacion2X, Generaacion2Y, Generaacion2Z), Quaternion.Euler(Generaacion2RotacionX, Generaacion2RotacionY, Generaacion2RotacionZ));
        }
    }
}
