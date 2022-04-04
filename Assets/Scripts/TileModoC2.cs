using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileModoC2 : MonoBehaviour
{
    private bool tileRevelada = false;  // indicador da carta virada ou não
    public Sprite originalCarta;        // Sprite da carta desejada
    public Sprite backCartaVermelho;            //Sprite do avesso Vermelho da carta
    public Sprite backCartaAzul;            //Sprite do avesso Azul da carta
    private int linha;                  // linha da carta
    // Start is called before the first frame update
    void Start()
    {
        print("LINHAA: " + linha);
        if(linha == 0)
        {
            EscondeCarta(); //mostra costa da carta
        }
        else
        {
            EscondeCartaAzul(); //mostra costa da carta asul
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
        GameObject.Find("gameManagerModoC2").GetComponent<ManagerCartasModoC2>().CartaSelecionada(gameObject);  // //executa a função cartaselecionada quando a carta é clicada
    }

    public void EscondeCarta() //mostra costa da carta
    {
        GetComponent<SpriteRenderer>().sprite = backCartaVermelho;

        tileRevelada = false;
    }

    public void EscondeCartaAzul()  //mostra costa azul
    {
        GetComponent<SpriteRenderer>().sprite = backCartaAzul;

        tileRevelada = false;
    }

    public void RevelaCarta()   // mostr frente da carta
    {
        GetComponent<SpriteRenderer>().sprite = originalCarta;
        tileRevelada = true;
    }

    public void setCartaOriginal(Sprite novaCarta, int linhaParam)  //cria a carta com valor e cor dacarta
    {
        originalCarta = novaCarta;
        linha = linhaParam;
    }


}
