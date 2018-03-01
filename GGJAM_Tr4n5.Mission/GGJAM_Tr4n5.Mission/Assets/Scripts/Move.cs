using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{   
    //variaveis de compensacao de direcao
    private static Vector3 CIMA = new Vector3(0, 0.10f, 1);
    private static Vector3 BAIXO = new Vector3(0, 0.10f, -1);
    private static Vector3 ESQUERDA = new Vector3(-1, 0.10f, 0);
    private static Vector3 DIREITA = new Vector3(1, 0.10f, 0);

    //variaveis necessarias
    Grid grid;
    Vector3 direcao;
    AudioSource Error_audio;
    AudioSource Death_audio;
    public int counter;
    public bool finishedDelay = false;
    private bool isCoroutineExecuting = false;
    private int[] positionAranha = new int[2];
    public int[] intransponiveis = { 1, 2 };
    public int[] letais = { 4 };
    public LevelGeneratorScript lgs;
    private bool isTransmit = true;

    public GameObject caveira;

    //array de comandos
    int[] comandos = {};

    void Start()
    {
        //reseta counter
        counter = 0;

        //pega a posicao inicial
        lgs = GameObject.Find("LevelGenerator").GetComponent<LevelGeneratorScript>();
        positionAranha = lgs.startPosition;

        //cria componentes de áudio
        Error_audio = GameObject.Find("SoundError").GetComponent<AudioSource>();
        Death_audio = GameObject.Find("SoundDeath").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine("MoveDelay");

        if (CanTryMoving())
        {
            if (NextNotFire(comandos[counter], positionAranha))
            {
                if (NotBlocked(comandos[counter], positionAranha))
                {
                    //Debug.Log("pode mover e instrucao = " + comandos[counter]);
                    MoveBug(counter);
                }
                else
                {
                    Error_audio.Play();
                }
            }
            else
            {
                killSpider();
            }

            finishedDelay = false;

            ChecaPosicaoAtual();
            if (comandos[counter] == 5)
            {
                Debug.Log("Vai tentar apertar o botao");
                tentaInteragir();
            }

            counter++;

        }
    }

    private void MoveBug(int i)
    {
        switch (comandos[i])
        {
            case 1:
                //Move Up
                MoveDirecao(CIMA);
                positionAranha[1] -= 1;
                break;
            case 2:
                //Move Down
                MoveDirecao(BAIXO);
                positionAranha[1] += 1;
                break;
            case 3:
                //Move Left
                MoveDirecao(ESQUERDA);
                positionAranha[0] -= 1;
                break;
            case 4:
                //Move Right
                MoveDirecao(DIREITA);
                positionAranha[0] += 1;
                break;
            default:
                break;
        }
    }

    private void MoveDirecao(Vector3 direcao)
    {
        grid = GameObject.FindObjectOfType<Grid>();
        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        Vector3 pos = grid.GetCellCenterWorld(cellPosition) + direcao;
        this.transform.position = pos;
    }

    private bool CanTryMoving()
    {
        //return false;
        if (finishedDelay && counter < comandos.Length)
            return true;
        else
            return false;
    }

    IEnumerator MoveDelay()
    {
        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(1);
        finishedDelay = true;

        isCoroutineExecuting = false;
    }

    private bool NotBlocked(int currentInstruction, int[] positionAranha)
    {
        int[,] levelLayout = lgs.LevelLayout; //level layout

        switch (currentInstruction)
        {
            case 1: //move up
                if (intransponiveis.Contains(levelLayout[positionAranha[0], positionAranha[1] - 1]))
                    return false;
                if (levelLayout[positionAranha[0], positionAranha[1] - 1] == 5)//chave
                    if (GameObject.Find("Portao_Chave(Clone)").transform.GetChild(0).gameObject.activeSelf)
                        return false;
                if (levelLayout[positionAranha[0], positionAranha[1] - 1] == 6)//botao
                    if (GameObject.Find("Portao_Botao(Clone)").transform.GetChild(0).gameObject.activeSelf)
                        return false;
                break;

            case 2: //move down
                if (intransponiveis.Contains(levelLayout[positionAranha[0], positionAranha[1] + 1]))
                    return false;
                if (levelLayout[positionAranha[0], positionAranha[1] + 1] == 5)//chave
                    if (GameObject.Find("Portao_Chave(Clone)").transform.GetChild(0).gameObject.activeSelf)
                        return false;
                if (levelLayout[positionAranha[0], positionAranha[1] + 1] == 6)//botao
                    if (GameObject.Find("Portao_Botao(Clone)").transform.GetChild(0).gameObject.activeSelf)
                        return false;
                break;

            case 3: //move left
                if (intransponiveis.Contains(levelLayout[positionAranha[0] - 1, positionAranha[1]]))
                    return false;
                if (levelLayout[positionAranha[0] - 1, positionAranha[1]] == 5)//chave
                    if (GameObject.Find("Portao_Chave(Clone)").transform.GetChild(0).gameObject.activeSelf)
                        return false;
                if (levelLayout[positionAranha[0] - 1, positionAranha[1]] == 6)//botao
                    if (GameObject.Find("Portao_Botao(Clone)").transform.GetChild(0).gameObject.activeSelf)
                        return false;
                break;

            case 4: //move right
                if (intransponiveis.Contains(levelLayout[positionAranha[0] + 1, positionAranha[1]]))
                    return false;
                if (levelLayout[positionAranha[0] + 1, positionAranha[1]] == 5)//chave
                    if (GameObject.Find("Portao_Chave(Clone)").transform.GetChild(0).gameObject.activeSelf)
                        return false;
                if (levelLayout[positionAranha[0] + 1, positionAranha[1]] == 6)//botao
                    if (GameObject.Find("Portao_Botao(Clone)").transform.GetChild(0).gameObject.activeSelf)
                        return false;
                break;
        }
        return true;
    }

    public void transmitClicked()
    {
        ComandosScript cs = GameObject.Find("Comandos").GetComponent<ComandosScript>();
        comandos = new int[10];
        comandos = cs.pos;
    }

    public void abortExec()
    {
        lgs.clearLevel();
        lgs.CreateLevel();
        comandos = new int[10];
        counter = 0;

    }

    public void ChecaPosicaoAtual()
    {
        int currentTile = lgs.LevelLayout[positionAranha[0], positionAranha[1]];
        if (letais.Contains(currentTile))
        {
            //this kills the spider
            killSpider();
        }
        if (currentTile == 9) // chave
        {
            Debug.Log("current tile tem chave");
            Destroy(GameObject.Find("KeyCard(Clone)"));
            AbrePortao("Chave");
        }

        if (currentTile == 3) // saida
        {
            if (lgs.currentLevel != 10)
            {
                Debug.Log("Entrou no if");
                lgs.clearLevel();
                lgs.currentLevel += 1;
                lgs.loadLevel(lgs.currentLevel);
                lgs.CreateLevel();
            }
            else
            {
                SceneManager.LoadScene("WINNER");
            }

        }

    }

    public void AbrePortao(string tipoPortao)
    {
        Debug.Log("abre portao tipo " + tipoPortao);
        Debug.Log("find Portao_" + tipoPortao);
        GameObject.Find("Portao_" + tipoPortao + "(Clone)").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Portao_" + tipoPortao + "(Clone)").transform.GetChild(1).gameObject.SetActive(true);
    }

    public bool NextNotFire(int currentInstruction, int[] positionAranha)
    {
        int[,] levelLayout = lgs.LevelLayout; //level layout

        switch (currentInstruction)
        {
            case 1: //move up
                if (levelLayout[positionAranha[0], positionAranha[1] - 1] == 1)
                    return false;
                break;

            case 2: //move down
                if (levelLayout[positionAranha[0], positionAranha[1] + 1] == 1)
                    return false;
                break;

            case 3: //move left
                if (levelLayout[positionAranha[0] - 1, positionAranha[1]] == 1)
                    return false;
                break;

            case 4: //move right
                if (levelLayout[positionAranha[0] + 1, positionAranha[1]] == 1)
                    return false;
                break;
        }

        return true;
    }

    void killSpider()
    {
        Death_audio.Play();
        //GameObject caveirinha = Instantiate(caveira, grid.GetCellCenterWorld(new Vector3Int(positionAranha[0], positionAranha[1], 0)), this.transform.rotation);

        lgs.clearLevel();
        lgs.CreateLevel();
    }

    void tentaInteragir()
    {
        Debug.Log("current position = " + positionAranha[0] + "," + positionAranha[1]);
        int[,] levelLayout = lgs.LevelLayout;
        bool taDoLado = false;
        if (levelLayout[positionAranha[0], positionAranha[1] + 1] == 7)
            taDoLado = true;
        if (levelLayout[positionAranha[0], positionAranha[1] - 1] == 7)
            taDoLado = true;
        if (levelLayout[positionAranha[0] + 1, positionAranha[1]] == 7)
            taDoLado = true;
        if (levelLayout[positionAranha[0] - 1, positionAranha[1]] == 7)
            taDoLado = true;

        if (taDoLado)
        {
            Debug.Log("ta do lado");
            GameObject.Find("BotaoGame(Clone)").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("BotaoGame(Clone)").transform.GetChild(1).gameObject.SetActive(true);
            AbrePortao("Botao");
        }
        else
        {
            Error_audio.Play();
        }
    }
}
