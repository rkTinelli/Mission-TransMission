using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderScript : MonoBehaviour
{

    public int valorLido;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
            case "Cima": valorLido = 1; break;
            case "Baixo": valorLido = 2; break;
            case "Esquerda": valorLido = 3; break;
            case "Direita": valorLido = 4; break;
            case "Acao1": valorLido = 5; break;
            case "Acao2": valorLido = 6; break;
            
        }



     }
    

}
