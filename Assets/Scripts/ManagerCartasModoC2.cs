using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ManagerCartasModoC2 : MonoBehaviour
{
    public GameObject carta;        //carta a ser descartada
    public Sprite novaCarta;        //updata acarta
    private bool primeiraCartaSelecionada, segundaCartaSelecionada; //indicadores para cada carta escolhida em cada linha
    private GameObject carta1, carta2;  // gameObjects das primeira e segunda carta selecionada
    private string linhaCarta1, linhaCarta2;    //linha da carta selecionada

    bool timerAcionado, timerPausado;   //indicador de pausa no timer ou start
    float timer;                        //variavel de tempo
    int numTentativas = 0;                //numero de tentativas na rodada
    int numAcertos = 0;                 //numero de acertos de pares
    AudioSource somOK;                  // som de acerto 
    int ultimoJogo = 0;
    int recorde = 0;

    // Start is called before the first frame update   
    void Start()
    {
        MostraCartas();
        UpDateTentativas();
        somOK = GetComponent<AudioSource>();
        ultimoJogo = PlayerPrefs.GetInt("Jogadas", 0);
        recorde = PlayerPrefs.GetInt("recordeModoC2", 0);
        GameObject.Find("ultimaJogada").GetComponent<Text>().text = "Ultimo Jogo-" + ultimoJogo;
        //GameObject.Find("recorde").GetComponent<Text>().text = "Recorde-" + recorde;
        PlayerPrefs.SetInt("modoAnterior", 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerAcionado)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                timerPausado = true;
                timerAcionado = false;
                if ((carta1.tag == carta2.tag) && (carta1.name[0] != carta2.name[0]))
                {
                    Destroy(carta1);
                    Destroy(carta2);
                    numAcertos++;
                    //somOK.Play();
                    if (numAcertos == 13)
                    {
                        if (recorde == 0)
                        {
                            recorde = numTentativas;
                            PlayerPrefs.SetInt("recordeModoC2", numTentativas);
                        }
                        else
                        {
                            if (recorde > numTentativas)
                            {
                                recorde = numTentativas;
                                PlayerPrefs.SetInt("recordeModoC2", numTentativas);
                            }
                        }


                        PlayerPrefs.SetInt("Jogadas", numTentativas);
                        SceneManager.LoadScene("Lab3_vitoria");
                    }
                }
                else
                {
                    if (carta1.name[0] == '1')
                    {
                        carta1.GetComponent<TileModoC2>().EscondeCartaAzul();

                    }
                    else
                    {
                        carta1.GetComponent<TileModoC2>().EscondeCarta();
                    }
                    if (carta2.name[0] == '1')
                    {
                        carta2.GetComponent<TileModoC2>().EscondeCartaAzul();

                    }
                    else
                    {
                        carta2.GetComponent<TileModoC2>().EscondeCarta();
                    }
                }
                primeiraCartaSelecionada = false;
                segundaCartaSelecionada = false;
                carta1 = null;
                carta2 = null;
                linhaCarta1 = "";
                linhaCarta2 = "";
                timer = 0;

            }
        }
    }

    void MostraCartas()
    {

        //Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity);
        //AddUmaCarta();
        for (int i = 0; i < 4; i++)
        {
            //    AddUmaCarta(i);
            //AddUmaCarta(i, arrayEmbaralhado[i]);
            AddUmaCarta(0, i, criaArrayEmbaralhado()[i]);
            AddUmaCarta(1, i, criaArrayEmbaralhado()[i]);
            AddUmaCarta(2, i, criaArrayEmbaralhado()[i]);
            AddUmaCarta(3, i, criaArrayEmbaralhado()[i]);
        }
        for (int i = 8; i < 13; i++)
        {
            //    AddUmaCarta(i);
            //AddUmaCarta(i, arrayEmbaralhado[i]);
            AddUmaCarta(0, i, criaArrayEmbaralhado()[i-8]);
            AddUmaCarta(1, i, criaArrayEmbaralhado()[i-8]);
            AddUmaCarta(2, i, criaArrayEmbaralhado()[i-8]);
            AddUmaCarta(3, i, criaArrayEmbaralhado()[i-8]);
        }
    }
    void AddUmaCarta(int linha, int rank, int valor)
    {
        GameObject centro = GameObject.Find("centroDaTela");
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (650 * escalaCartaOriginal) / 110.0f;
        float fatorEscalaY = (945 * escalaCartaOriginal) / 110.0f;
        //Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2) * 1.3f), centro.transform.position.y, centro.transform.position.z);
        //        Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2) * fatorEscalaX), centro.transform.position.y, centro.transform.position.z);
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2) * fatorEscalaX), 
            centro.transform.position.y + ((linha - 4 / 2) * fatorEscalaY), 
            centro.transform.position.z);
        //GameObject c = (GameObject)(Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity));
        //GameObject c = (GameObject)(Instantiate(carta, new Vector3(rank * 1.5f, 0, 0), Quaternion.identity));
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));
        c.tag = "" + (valor );
        //c.name = "" + valor;
        c.name = "" + linha + "_" + valor;
        string nomeDaCarta = "";
        string numeroCarta = "";
        /*if (rank == 0)
            numeroCarta = "ace";
        else if (rank == 10)
            numeroCarta = "jack";
        else if (rank == 11)
            numeroCarta = "queen";
        else if (rank == 12)
            numeroCarta = "king";
        else
            numeroCarta = "" + (rank + 1);*/ //else if para array ordenado no deck
        if (valor == 0)
            numeroCarta = "ace";
        else if (valor == 10)
            numeroCarta = "jack";
        else if (valor == 11)
            numeroCarta = "queen";
        else if (valor == 12)
            numeroCarta = "king";
        else
            numeroCarta = "" + (valor + 1);
        nomeDaCarta = numeroCarta + "_of_hearts";
        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
        print("S1: " + s1);
        //GameObject.Find("" + rank).GetComponent<TileModoC1>().setCartaOriginal(s1);
        GameObject.Find("" + linha + "_" + valor).GetComponent<TileModoC2>().setCartaOriginal(s1, linha);
    }

    public int[] criaArrayEmbaralhado()
    {
        int[] novoArray = new int[] { 0, 10, 11, 12 };
        int temp;
        for (int t = 0; t < 4; t++)
        {
            temp = novoArray[t];
            int r = Random.Range(t, 4);
            novoArray[t] = novoArray[r];
            novoArray[r] = temp;
        }
        return novoArray;
    }

    public void CartaSelecionada(GameObject carta)
    {
        if (!primeiraCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);
            linhaCarta1 = linha;
            primeiraCartaSelecionada = true;
            carta1 = carta;
            carta1.GetComponent<TileModoC2>().RevelaCarta();
        }
        else if (primeiraCartaSelecionada && !segundaCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);
            linhaCarta2 = linha;
            primeiraCartaSelecionada = true;
            carta2 = carta;
            carta2.GetComponent<TileModoC2>().RevelaCarta();
            VerificaCartas();
        }
    }

    public void VerificaCartas()
    {
        DisparaTimer();
        numTentativas++;
        UpDateTentativas();
    }

    public void DisparaTimer()
    {
        timerPausado = false;
        timerAcionado = true;
    }
    void UpDateTentativas()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = "Tentativas = " + numTentativas;
    }
}
