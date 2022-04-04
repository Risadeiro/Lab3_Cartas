using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ManagerCartasModoC1 : MonoBehaviour
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
    int ultimoJogo = 0;                 //valor de tentativas do ultimo jgo
    int recorde = 0;                    // valor do recorde paraessa modalidade

    // Start is called before the first frame update   
    void Start()
    {
        MostraCartas();         // renderiza as cartas
        UpDateTentativas();     // 
        somOK = GetComponent<AudioSource>();
        ultimoJogo = PlayerPrefs.GetInt("Jogadas", 0);
        recorde = PlayerPrefs.GetInt("recordeModoC1", 0);
        GameObject.Find("ultimaJogada").GetComponent<Text>().text = "Ultimo Jogo-" + ultimoJogo;
        GameObject.Find("recorde").GetComponent<Text>().text = "Recorde-" + recorde;
        PlayerPrefs.SetInt("modoAnterior", 1);

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
                    somOK.Play();
                    if (numAcertos == 13)
                    {
                        if (recorde == 0)
                        {
                            recorde = numTentativas;
                            PlayerPrefs.SetInt("recordeModoC1", numTentativas);
                        }
                        else
                        {
                            if (recorde > numTentativas)
                            {
                                recorde = numTentativas;
                                PlayerPrefs.SetInt("recordeModoC1", numTentativas);
                            }
                        }


                        PlayerPrefs.SetInt("Jogadas", numTentativas);

                        SceneManager.LoadScene("Lab3_vitoria");
                    }
                }
                else
                {
                    if(carta1.name[0] == '1')
                    {
                        carta1.GetComponent<TileModoC1>().EscondeCartaAzul();

                    }
                    else
                    {
                        carta1.GetComponent<TileModoC1>().EscondeCarta();
                    }
                    if(carta2.name[0] == '1')
                    {
                        carta2.GetComponent<TileModoC1>().EscondeCartaAzul();

                    }
                    else
                    {
                        carta2.GetComponent<TileModoC1>().EscondeCarta();
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
        int[] arrayEmbaralhado = criaArrayEmbaralhado();
        int[] arrayEmbaralhado2 = criaArrayEmbaralhado();

        //Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity);
        //AddUmaCarta();
        for (int i = 0; i < 13; i++)
        {
            //    AddUmaCarta(i);
            //AddUmaCarta(i, arrayEmbaralhado[i]);
            AddUmaCarta(0, i, arrayEmbaralhado[i]);
            AddUmaCarta(1, i, arrayEmbaralhado2[i]);
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
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2) * fatorEscalaX), centro.transform.position.x + ((linha - 2 / 2) * fatorEscalaY), centro.transform.position.z);
        //GameObject c = (GameObject)(Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity));
        //GameObject c = (GameObject)(Instantiate(carta, new Vector3(rank * 1.5f, 0, 0), Quaternion.identity));
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity)); // // instancia o objeto da carta
        c.tag = "" + (valor + 1);
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
        nomeDaCarta = numeroCarta + "_of_hearts" ;
        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));//carrega o sprite da carta comvalor tal
        print("S1: " + s1);
        //GameObject.Find("" + rank).GetComponent<TileModoC1>().setCartaOriginal(s1);
        GameObject.Find("" + linha + "_" + valor).GetComponent<TileModoC1>().setCartaOriginal(s1, linha);    //pega o objeto da carta e define como o objeto criado aqui nessa função
    }

    public int[] criaArrayEmbaralhado()     //criae retorna array como 13 valoews de 0 a 12
    {
        int[] novoArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        int temp;
        for (int t = 0; t < 13; t++)
        {
            temp = novoArray[t];
            int r = Random.Range(t, 13);
            novoArray[t] = novoArray[r];
            novoArray[r] = temp;
        }
        return novoArray;
    }

    public void CartaSelecionada(GameObject carta)  // 
    {
        if (!primeiraCartaSelecionada)  //
        {
            string linha = carta.name.Substring(0, 1);  //pega linha da primeira carta selecionada
            linhaCarta1 = linha;    
            primeiraCartaSelecionada = true;    //seta valor de primera carta como selecionada
            carta1 = carta;                     // setaobjeto da carta selecionada como primera carta
            carta1.GetComponent<TileModoC1>().RevelaCarta();    //mostra frente da carta 1
        }
        else if (primeiraCartaSelecionada && !segundaCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);      //pega a linha da segunda carta selecionada
            linhaCarta2 = linha;                            
            primeiraCartaSelecionada = true;        
            carta2 = carta;                 //seta o objeto da carta selecionada como segunda carta
            carta2.GetComponent<TileModoC1>().RevelaCarta();    //mostra frente da carta 2
            VerificaCartas();                   //compara cartas
        }
    }

    public void VerificaCartas()//compara cartas
    {
        DisparaTimer();
        numTentativas++;    //aumenta o nuero de tentativas em um
        UpDateTentativas(); //chama função de atualizar texto de numero de tentativas
    }

    public void DisparaTimer()  //inicia contador
    {
        timerPausado = false;
        timerAcionado = true;
    }
    void UpDateTentativas() //muda o texto com numero de tentativas
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = "Tentativas = " + numTentativas;
    }
}
