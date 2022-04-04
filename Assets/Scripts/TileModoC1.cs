using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileModoC1 : MonoBehaviour
{
    private bool tileRevelada = false;  // indicador da carta virada ou não
    public Sprite originalCarta;        // Sprite da carta desejada
    public Sprite backCartaVermelho;            //Sprite do avesso Vermelho da carta
    public Sprite backCartaAzul;            //Sprite do avesso Azul da carta
    private int linha;              // linha da carta
    // Start is called before the first frame update
    void Start()
    {
        print("LINHAA: " + linha);
        if(linha == 0)
        {
            EscondeCarta(); //mostra costa da carta vermelha
        }
        else
        {
            EscondeCartaAzul(); //mostra costa do baralho azul
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        //if (tileRevelada)       
        //    EscondeCarta();
        //else
        //    RevelaCarta();  //aqui nao se guardava numero de cartas
        GameObject.Find("gameManagerModoC1").GetComponent<ManagerCartasModoC1>().CartaSelecionada(gameObject); //executa a função cartaselecionada quando a carta é clicada
    }

    public void EscondeCarta()  //mostra costa da carta vermelha
    {
        GetComponent<SpriteRenderer>().sprite = backCartaVermelho;

        tileRevelada = false;
    }

    public void EscondeCartaAzul()//mostra costa da carta azul
    {
        GetComponent<SpriteRenderer>().sprite = backCartaAzul;

        tileRevelada = false;
    }

    public void RevelaCarta()   //mostra frente da carta
    {
        GetComponent<SpriteRenderer>().sprite = originalCarta;
        tileRevelada = true;
    }

    public void setCartaOriginal(Sprite novaCarta, int linhaParam)  //define o vlor da carta e qual tipo de carta sera
    {
        originalCarta = novaCarta;
        linha = linhaParam;
    }


}
