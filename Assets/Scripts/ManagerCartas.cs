using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ManagerCartas : MonoBehaviour
{
    public GameObject carta;        //carta a ser descartada
    public Sprite novaCarta;        //updata acarta
    private bool primeiraCartaSelecionada, segundaCartaSelecionada; //indicadores para cada carta escolhida em cada linha
    private GameObject carta1, carta2;  // gameObjects das primeira e segunda carta selecionada
    private string linhaCarta1, linhaCarta2;    //linha da carta selecionada

    bool timerAcionado, timerPausado;   //indicador de pausa no timer ou start
    float timer;                        //variavel de tempo
    int numTentativas=0;                //numero de tentativas na rodada
    int numAcertos = 0;                 //numero de acertos de pares
    AudioSource somOK;                  // som de acerto 
    int ultimoJogo = 0;                 // numero que define o numero de tentativas do ultimo jogo
    int recorde = 0;                    // numero de tentativas minimo neste modo de jogo
    // Start is called before the first frame update   
    void Start()
    {
        MostraCartas();                 // função para renderizar as cartas
        UpDateTentativas();             // seta o numero de tentativas
        somOK = GetComponent<AudioSource>();    // variaavel do componente de som
        ultimoJogo = PlayerPrefs.GetInt("Jogadas", 0);  //carrega o valor de jogadas no jogo go antaterior 
        recorde = PlayerPrefs.GetInt("recordeTradicional", 0);  //carrega o recorde desse modo de jogo
        GameObject.Find("ultimaJogada").GetComponent<Text>().text = "Ultimo Jogo-" + ultimoJogo;    //seta o texto que mostra popntuação do ultimo jogo
        GameObject.Find("recorde").GetComponent<Text>().text = "Recorde-" + recorde;    //seta o valor do recorde deste modo de jogo
        PlayerPrefs.SetInt("modoAnterior", 0);  //define o modo de jogo anterior como o modo feito em aula

    }

    // Update is called once per frame
    void Update()
    {
        if (timerAcionado)
        {
            timer += Time.deltaTime;
            print(timer);
            if (timer > 1)
            {
                timerPausado = true;
                timerAcionado = false;
                if (carta1.tag == carta2.tag)   //se valor da carta selecionadafor igual entre primeira e segunda
                {
                    Destroy(carta1);    //elimina o reneder da acarta 1
                    Destroy(carta2);    // destroi o render da carta 2
                    numAcertos++;       //aumenta um no numero de acertos
                    somOK.Play();       //toca o audio de acerto
                    if (numAcertos == 13)       //se ja acertou todas as vezes:
                    {
                        if(recorde == 0)        //se ainda nao havia um recorde
                        {
                            recorde = numTentativas;    //seta o numero de tentativas como recorde atual 
                            PlayerPrefs.SetInt("recordeTradicional", numTentativas);//seta o numero de tentativas como recorde atual dessa modalidade
                        }
                        else
                        {
                            if (recorde > numTentativas)    //se numero de tentativas menor q o recorde
                            {
                                recorde = numTentativas;    //
                                PlayerPrefs.SetInt("recordeTradicional", numTentativas);//seta o numero de tentativas como recorde atual dessa modalidade
                            }
                        }
                        PlayerPrefs.SetInt("Jogadas", numTentativas);//seta o numero de tentativas como quantidade de jogadas 
                        SceneManager.LoadScene("Lab3_vitoria");//carregaa cena de vitoria
                    }
                }
                else
                {
                    carta1.GetComponent<Tile>().EscondeCarta(); //esconde a carta 1
                    carta2.GetComponent<Tile>().EscondeCarta(); //esconde carta 2
                }
                primeiraCartaSelecionada = false;   //seta q carta 1 n foi selecionada
                segundaCartaSelecionada = false;    //seta q carta 2 n foi selecionada
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
        int[] arrayEmbaralhado = criaArrayEmbaralhado();    //cria o vetor de cartas embaralhadas
        int[] arrayEmbaralhado2 = criaArrayEmbaralhado();   //cria o segundo vetor de cartas embaralhadas

        //Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity);
        //AddUmaCarta();
        for ( int i=0; i <13; i++)
        {
        //    AddUmaCarta(i);
            //AddUmaCarta(i, arrayEmbaralhado[i]);
            AddUmaCarta(0, i, arrayEmbaralhado[i]);     //adiciona o render da carta do baralho 1
            AddUmaCarta(1, i, arrayEmbaralhado2[i]);    //adiciona o render da carta do baralho 2
        }
    }
    void AddUmaCarta(int linha,int rank, int valor)
    {
        GameObject centro = GameObject.Find("centroDaTela");        //pega a referencia do ceontro da tela
        float escalaCartaOriginal = carta.transform.localScale.x;   
        float fatorEscalaX = (650 * escalaCartaOriginal) / 110.0f;
        float fatorEscalaY = (945 * escalaCartaOriginal) / 110.0f;
        //Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2) * 1.3f), centro.transform.position.y, centro.transform.position.z);
//        Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2) * fatorEscalaX), centro.transform.position.y, centro.transform.position.z);
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2) * fatorEscalaX), centro.transform.position.x + ((linha - 2 / 2) * fatorEscalaY), centro.transform.position.z);
        //GameObject c = (GameObject)(Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity));
        //GameObject c = (GameObject)(Instantiate(carta, new Vector3(rank * 1.5f, 0, 0), Quaternion.identity));
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));      // instancia o objeto da carta
        c.tag = "" + (valor);
        //c.name = "" + valor;
        c.name = "" + linha+"_"+valor;
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
        if (linha == 1)
        {
            nomeDaCarta = numeroCarta + "_of_hearts";

        }
        else
        {
            nomeDaCarta = numeroCarta + "_of_clubs";

        }
        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));  //carrega o sprite da carta comvalor tal
        print("S1: " + s1);
        //GameObject.Find("" + rank).GetComponent<Tile>().setCartaOriginal(s1);
        GameObject.Find("" +linha+"_"+ valor).GetComponent<Tile>().setCartaOriginal(s1);    //pega o objeto da carta e define como o objeto criado aqui nessa função
    }

    public int[] criaArrayEmbaralhado()     //embaralha e retorna um array com 13 valores de 0 a 12
    {
        int[] novoArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        int temp;
        for (int t=0; t<13; t++)
        {
            temp = novoArray[t];
            int r = Random.Range(t, 13);
            novoArray[t] = novoArray[r];
            novoArray[r] = temp;
        }
        return novoArray;
    }

    public void CartaSelecionada(GameObject carta)
    {
        if (!primeiraCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);      // pega a linha da carta selecionada
            linhaCarta1 = linha;           // pega a linha da carta selecionada coomo primeira
            primeiraCartaSelecionada = true;    //  seta primeita carta como selecionada 
            carta1 = carta;                 // define a primeira cartacomo a carta selecionada
            carta1.GetComponent<Tile>().RevelaCarta();  //mostra a frente da carta selecionada
        }
        else if(primeiraCartaSelecionada && !segundaCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);  //pega a linha da carta selecionada
            linhaCarta2 = linha;    //pega a linha da carta selecionada como segunda
            primeiraCartaSelecionada = true;    //seta a segunda carta como selecionada
            carta2 = carta; //define a segunda carta como a carta selecionada
            carta2.GetComponent<Tile>().RevelaCarta();  //mostra a grente da carta selecionada
            VerificaCartas();   //compara cartas
        }
    }

    public void VerificaCartas()
    {
        DisparaTimer();         //inicia o timer de virar cartas
        numTentativas++;        // aumenta o numero de tentativas
        UpDateTentativas();     //  chama função para mudar o texto de tentativas
    }

    public void DisparaTimer()
    {
        timerPausado = false;  
        timerAcionado = true;
    }
    void UpDateTentativas()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = "Tentativas = " + numTentativas;   //atualiza o texto de numero de tentativas
    }
}
