using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool tileRevelada = false;  // indicador da carta virada ou n�o
    public Sprite originalCarta;        // Sprite da carta desejada
    public Sprite backCarta;            //Sprite do avesso da carta

    // Start is called before the first frame update
    void Start()
    {
        EscondeCarta();     // chama a fun��o para esconder as cartas renderizadas
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
        GameObject.Find("gameManager").GetComponent<ManagerCartas>().CartaSelecionada(gameObject);  //executa a fun��o cartaselecionada quando a carta � clicada
    }

    public void EscondeCarta()
    {
        GetComponent<SpriteRenderer>().sprite = backCarta;      //fun��o para renderizara parte de tras da carta renderizada
      
        tileRevelada = false;   //seta o status da carta como nao revelada
    }

    public void RevelaCarta()
    {
        GetComponent<SpriteRenderer>().sprite = originalCarta;          // fun��o para renderizar aparta da frente da carta
        tileRevelada = true;            //seta o status da carta como visivel
    }

    public void setCartaOriginal(Sprite novaCarta)
    {
        originalCarta = novaCarta;  // seta o valor do objeto carta 
    }


}
