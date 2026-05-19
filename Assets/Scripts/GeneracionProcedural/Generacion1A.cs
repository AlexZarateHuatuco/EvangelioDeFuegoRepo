using UnityEngine;

public class Generacion1A : MonoBehaviour
{
    int Eleccion;
    public float Generaacion1X, Generaacion1Y, Generaacion1Z;
    public float Generaacion2X, Generaacion2Y, Generaacion2Z;
    public float Generaacion3X, Generaacion3Y, Generaacion3Z;
    public GameObject[] salasArriba;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Eleccion = Random.Range(0, salasArriba.Length);

        if(Eleccion == 0)
        {
            Instantiate(salasArriba[Eleccion], new Vector3(Generaacion1X, Generaacion1Y, Generaacion1Z), Quaternion.identity);
            //Instantiate(salasArriba[Eleccion], new Vector3(0.01f, 0f, -0.04f), Quaternion.identity);
        }
        else if(Eleccion == 1)
        {
            Instantiate(salasArriba[Eleccion], new Vector3(Generaacion2X, Generaacion2Y, Generaacion2Z), Quaternion.identity);
            //Instantiate(salasArriba[Eleccion], new Vector3(0.01f, 0f, -0.05f), Quaternion.identity);
        }
        else
        {
            Instantiate(salasArriba[Eleccion], new Vector3(Generaacion3X, Generaacion3Y, Generaacion3Z), Quaternion.identity);
            //Instantiate(salasArriba[Eleccion], new Vector3(0f, 0f, -0.04f), Quaternion.identity);
        }


    }
}
