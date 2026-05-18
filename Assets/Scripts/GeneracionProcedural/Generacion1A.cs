using UnityEngine;

public class Generacion1A : MonoBehaviour
{
    int Eleccion;
    public GameObject[] salasArriba;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Eleccion = Random.Range(0, salasArriba.Length);

        if(Eleccion == 0)
        {
            Instantiate(salasArriba[Eleccion], new Vector3(0.01f, 0f, -0.04f), Quaternion.identity);
        }
        else if(Eleccion == 1)
        {
            Instantiate(salasArriba[Eleccion], new Vector3(0.01f, 0f, -0.05f), Quaternion.identity);
        }
        else
        {
            Instantiate(salasArriba[Eleccion], new Vector3(0f, 0f, -0.04f), Quaternion.identity);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
