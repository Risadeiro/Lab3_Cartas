using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileModoC1 : MonoBehaviour
{
    private bool tileRevelada = false;  // indicador da carta virada ou não
    public Sprite originalCarta;        // Sprite da carta desejada
    public Sprite backCartaVermelho;            //Sprite do avesso Vermelho da carta
    public Sprite backCartaAzul;            //Sprite do avesso Azul da carta
    private int linha;
    // Start is called before the first frame update
    void Start()
    {
        print("LINHAA: " + linha);
        if(linha == 0)
        {
            EscondeCarta();
        }
        else
        {
            EscondeCartaAzul();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        print("Voce pressionou num Tile");
        //if (tileRevelada)       
        //    EscondeCarta();
        //else
        //    RevelaCarta();  //aqui nao se guardava numero de cartas
        GameObject.Find("gameManagerModoC1").GetComponent<ManagerCartasModoC1>().CartaSelecionada(gameObject);
    }

    public void EscondeCarta()
    {
        GetComponent<SpriteRenderer>().sprite = backCartaVermelho;

        tileRevelada = false;
    }

    public void EscondeCartaAzul()
    {
        GetComponent<SpriteRenderer>().sprite = backCartaAzul;

        tileRevelada = false;
    }

    public void RevelaCarta()
    {
        GetComponent<SpriteRenderer>().sprite = originalCarta;
        tileRevelada = true;
    }

    public void setCartaOriginal(Sprite novaCarta, int linhaParam)
    {
        originalCarta = novaCarta;
        linha = linhaParam;
    }


}
