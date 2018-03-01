using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelGeneratorScript : MonoBehaviour {

    //variaveis de identificacao de objetos
    private static int CHAO = 0;
    private static int FOGO = 1;
    private static int PAREDE = 2;
    private static int SAIDA = 3;
    private static int BURACO = 4;
    private static int PORTACHAVE = 5;
    private static int PORTABOTAO = 6;
    private static int BOTAO = 7;
    private static int INICIO = 8;
    private static int CHAVE = 9;
    private static int VAZIO = 10;

    public Grid grid;

    private static int maxSize = 20;

    public GameObject fogo;
    public GameObject parede;
    public GameObject chao;
    public GameObject buraco;
    public GameObject portaChave;
    public GameObject portaBotao;
    public GameObject botao;
    public GameObject aranha;
    public GameObject chave;
    public GameObject saida;

    public Camera GameCamera;

    public int[,] LevelLayout = new int[maxSize, maxSize];
    private int[] levelSize = new int[2];
    public int[] startPosition = new int[2];

    public int currentLevel = 2;

    public 

    // Use this for initialization
    void Start () {
        loadLevel(currentLevel);
        CreateLevel();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateLevel()
    {
        for (int i = 0; i < levelSize[0]; i++)
        {
            for (int j = 0; j < levelSize[1]; j++)
            {
                PopulateTile(i, j, LevelLayout[i, j]);
            }

        }
       
        switch(currentLevel)
        {
            case 1:
                GameCamera.transform.position = new Vector3 (-95.5f , -3.8f, -9.8f);
            break;
            case 2:
                GameCamera.transform.position = new Vector3(-95.5f, -3.8f, -9.8f);
                break;
            case 3:
                GameCamera.transform.position = new Vector3(-95f, -3.8f, -10f);
                break;
            case 4:
                GameCamera.transform.position = new Vector3(-95f, -3.8f, -10.9f);
                break;
            case 5:
                GameCamera.transform.position = new Vector3(-94f, -4.1f, -9.1f);
                break;
            case 6:
                GameCamera.transform.position = new Vector3(-93.8f, -4.5f, -9.1f);
                break;
            case 7:
                GameCamera.transform.position = new Vector3(-95f, -3.8f, -10f);
                break;
            case 8:
                GameCamera.transform.position = new Vector3(-94f, -3f, -11f);
                break;
            case 9:
                GameCamera.transform.position = new Vector3(-95f, -4f, -9f);
                break;
            case 10:
                GameCamera.transform.position = new Vector3(-92f, -4f, -10.3f);
                break;

            default:
                GameCamera.transform.position = new Vector3(0, 0, 0);
                break;
        }

    }

    private void PopulateTile(int i, int j, int tileCode)
    {
        if (tileCode == PAREDE)
            addFeature(i, j, PAREDE);
        else if (tileCode == BURACO)
            addFeature(i, j, BURACO);
        else if (tileCode == SAIDA)
            addFeature(i, j, SAIDA);
        else
        {
            addFeature(i, j, CHAO);
            if (tileCode == FOGO)
                addFeature(i, j, FOGO);
            else if (tileCode == INICIO)
                addFeature(i, j, INICIO);
            else if (tileCode == CHAVE)
                addFeature(i, j, CHAVE);
            else
                addFeature(i, j, tileCode);

        }
    }

    private void addFeature(int i, int j, int feature)
    {
        Vector3 deltaY = new Vector3(0, (float)0.1, 0);
        //Vector3 deltaAranha = new Vector3((float)1.5, (float)0.1, (float)2.5);
        if (feature == FOGO)
            Instantiate(fogo, grid.GetCellCenterWorld(new Vector3Int(i, j, 0)) - deltaY, this.transform.rotation);
        if (feature == BURACO)
            Instantiate(buraco, grid.GetCellCenterWorld(new Vector3Int(i, j, 0)) - deltaY, this.transform.rotation);
        if (feature == CHAO)
            Instantiate(chao, grid.GetCellCenterWorld(new Vector3Int(i, j, 0)) - deltaY, this.transform.rotation);
        if (feature == PAREDE)
            Instantiate(parede, grid.GetCellCenterWorld(new Vector3Int(i, j, 0)) - deltaY, this.transform.rotation);
        if (feature == PORTABOTAO)
            Instantiate(portaBotao, grid.GetCellCenterWorld(new Vector3Int(i, j, 0)) - deltaY, this.transform.rotation);
        if (feature == PORTACHAVE)
            Instantiate(portaChave, grid.GetCellCenterWorld(new Vector3Int(i, j, 0)) - deltaY, this.transform.rotation);
        if (feature == INICIO)
        {
            Instantiate(aranha, grid.GetCellCenterWorld(new Vector3Int(i, j, 0)) - deltaY, this.transform.rotation);
            startPosition[0] = i;
            startPosition[1] = j;
        }
        if (feature == CHAVE)
            Instantiate(chave, grid.GetCellCenterWorld(new Vector3Int(i, j, 0)) + 4 * deltaY, this.transform.rotation * Quaternion.Euler(0, -45, 0));
        if (feature == BOTAO)
            Instantiate(botao, grid.GetCellCenterWorld(new Vector3Int(i, j, 0)) - deltaY, this.transform.rotation);
        if (feature == SAIDA)
            Instantiate(saida, grid.GetCellCenterWorld(new Vector3Int(i, j, 0)) - deltaY, this.transform.rotation);
    }

    public void loadLevel(int level)
    {
        int counter = 0;
        string line;
        string path = "/Resources/Levels/" + level.ToString() + ".txt";


        // Read the file and display it line by line.  
        System.IO.StreamReader file =
            new System.IO.StreamReader(Application.dataPath + path);

        //TextAsset lvl = 

        while ((line = file.ReadLine()) != null)
        {
            string[] values = line.Split(' ');
            levelSize[0] = values.Length;
            for (int i = 0; i < values.Length; i++)
                LevelLayout[i, counter] = Convert.ToInt32(values[i]);
            counter++;
        }
        levelSize[1] = counter;

        file.Close();
    }

    public void clearLevel()
    {
        Debug.Log("entrou no clear()");
        foreach (GameObject gaob in GameObject.FindGameObjectsWithTag("Level"))
        {
            Destroy(gaob);
        }
    }

}
