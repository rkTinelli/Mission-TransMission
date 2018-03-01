using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragdrop : MonoBehaviour
{

    float distancia = 8.65f;
    GameObject getTarget;
    public GameObject bloco;
    bool trg1, trg2, trg3, trg4, trg5, trg6, trg7, trg8, trg9, trg10;
    bool tagComm;
    static float comandos = 98.263f;
    static float dist = 1.17f;
    float a = comandos-5.2288f;
    float b = comandos-4.09f;
    float c = comandos-2.9288f; //comandos-2.9288f;
    float d = comandos-1.75f;
    float e = comandos -0.57f;
    float f = comandos + 0.61f;
    float g = comandos + 1.79f;
    float h = comandos + 2.97f;
    float i = comandos + 4.15f;
    float j = comandos + 5.33f;

    public ComandosScript comandoScript;
    private int numeroTag;
    private int ondeEstava;

    public Camera rightCamera;

    private void Start()
    {
        comandoScript = GameObject.Find("Comandos").GetComponent<ComandosScript>();
    }

    private void Update()
    {
        //Mouse Button Press Down
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            getTarget = ReturnClickedObject(out hitInfo);
            if (getTarget != null)
            {
                //Instantiate(bloco,getTarget.transform.position,getTarget.transform.rotation);
            }
        }
       /* Debug.Log("TagComm =" + tagComm);
        Debug.Log("Pos1 = " + trg1);
        Debug.Log("Pos2 = " + trg2);
        Debug.Log("Pos3 = " + trg3);*/
    }



    private void OnMouseDrag()
    {


        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distancia);

        Vector3 objPosition = rightCamera.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;




    }
    //other.transform.position = this.transform.position;




    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = rightCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Comm")
        { tagComm = true; }

        if (tagComm == true)
        {

            if (col.tag == "Pos1")
            { trg1 = true; }
            if (col.tag == "Pos2")
            { trg2 = true; }
            if (col.tag == "Pos3")
            { trg3 = true; }
            if (col.tag == "Pos4")
            { trg4 = true; }
            if (col.tag == "Pos5")
            { trg5 = true; }
            if (col.tag == "Pos6")
            { trg6 = true; }
            if (col.tag == "Pos7")
            { trg7 = true; }
            if (col.tag == "Pos8")
            { trg8 = true; }
            if (col.tag == "Pos9")
            { trg9 = true; }
            if (col.tag == "Pos10")
            { trg10 = true; }
        }


    }

    private void OnTriggerExit(Collider col)
    {

        if (col.tag == "Comm")
        { tagComm = false; }
        if (col.tag == "Pos1")
        { trg1 = false; }
        if (col.tag == "Pos2")
        { trg2 = false; }
        if (col.tag == "Pos3")
        { trg3 = false; }
        if (col.tag == "Pos4")
        { trg4 = false; }
        if (col.tag == "Pos5")
        { trg5 = false; }
        if (col.tag == "Pos6")
        { trg6 = false; }
        if (col.tag == "Pos7")
        { trg7 = false; }
        if (col.tag == "Pos8")
        { trg8 = false; }
        if (col.tag == "Pos9")
        { trg9 = false; }
        if (col.tag == "Pos10")
        { trg10 = false; }

        //hgfghjuhgfghj

    }



    private void OnMouseUp()
    {
        switch (this.tag)
        {
            case "Cima":
                numeroTag = 1; break;
            case "Baixo":
                numeroTag = 2; break;
            case "Esquerda":
                numeroTag = 3; break;
            case "Direita":
                numeroTag = 4; break;
            case "Acao1":
                numeroTag = 5; break;
            case "Acao2":
                numeroTag = 6; break;
        }
            // int aux = numero

            if (tagComm == false)
        {
            Debug.Log("Morte");
            switch (ondeEstava)
            {
                case 1:
                    comandoScript.pos1 = 0; break;
                case 2:
                    comandoScript.pos2 = 0; break;
                case 3:
                    comandoScript.pos3 = 0; break;
                case 4:
                    comandoScript.pos4 = 0; break;
                case 5:
                    comandoScript.pos5 = 0; break;
                case 6:
                    comandoScript.pos6 = 0; break;
                case 7:
                    comandoScript.pos7 = 0; break;
                case 8:
                    comandoScript.pos8 = 0; break;
                case 9:
                    comandoScript.pos9 = 0; break;
                case 10:
                    comandoScript.pos10 = 0; break;
            }

            switch (this.tag)
            {
                case "Cima":
                    transform.position = GameObject.Find("ContCima").transform.position; break;
                case "Baixo":
                    transform.position = GameObject.Find("ContBaixo").transform.position; break;
                case "Esquerda":
                    transform.position = GameObject.Find("ContEsquerda").transform.position; break;
                case "Direita":
                    transform.position = GameObject.Find("ContDireita").transform.position; break;
                case "Acao1":
                    transform.position = GameObject.Find("ContAcao").transform.position; break;
                case "Acao2":
                    transform.position = GameObject.Find("ContStop").transform.position; break;
            }

        }

        

        else
        {

            if (trg1 == true)
            { transform.position = new Vector3(a, 0, 0);
                comandoScript.pos1 = numeroTag;
                ondeEstava = 1;
            }

            if (trg2 == true)
            { transform.position = new Vector3(b, 0, 0);
                comandoScript.pos2 = numeroTag;
                ondeEstava = 2;
            }

            if (trg3 == true)
            { transform.position = new Vector3(c, 0, 0);
                    comandoScript.pos3 = numeroTag;
                ondeEstava = 3;
            }

            if (trg4 == true)
            { transform.position = new Vector3(d, 0, 0);
                    comandoScript.pos4 = numeroTag;
                ondeEstava = 4;
            }

            if (trg5 == true)
            { transform.position = new Vector3(e, 0, 0);
                    comandoScript.pos5 = numeroTag;
                ondeEstava = 5;
            }

            if (trg6 == true)
            { transform.position = new Vector3(f, 0, 0);
                    comandoScript.pos6 = numeroTag;
                ondeEstava = 6;
            }

            if (trg7 == true)
            { transform.position = new Vector3(g, 0, 0);
                    comandoScript.pos7 = numeroTag;
                ondeEstava = 7;
            }

            if (trg8 == true)
            { transform.position = new Vector3(h, 0, 0);
                    comandoScript.pos8 = numeroTag;
                ondeEstava = 8;
            }

            if (trg9 == true)
            { transform.position = new Vector3(i, 0, 0);
                    comandoScript.pos9 = numeroTag;
                ondeEstava = 9;
            }

            if (trg10 == true)
            { transform.position = new Vector3(j, 0, 0);
                    comandoScript.pos10 = numeroTag;
                ondeEstava = 10;
            }

        }
    }

}
