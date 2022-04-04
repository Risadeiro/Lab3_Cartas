using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManageBotoes : MonoBehaviour
{
    public int modoDeJogoAnterior = 0;  //seta o valor do tipo de jogo anterior como o modo feito em aula por padrao
    // Start is called before the first frame update
    void Start()        // executavel ao começar o script
    {
        PlayerPrefs.GetInt("Jogadas", 0);   // seta o numero de jogadas do jogo para 0 pq ta começando
        modoDeJogoAnterior = PlayerPrefs.GetInt("modoAnterior", 0);     // seta o modo de jogo para tela seguinte de acordo com o que a ultima tela setou, se nao setou nada, coloca o modo feit oem aula por padrao
    }

    // Update is called once per frame
    void Update()       
    {
        
    }
    public void StartModoTradicional()      
    {
        SceneManager.LoadScene("Lab3");     //carrega a cena do modo tradicional
    }
    public void StartModoC1()
    {
        SceneManager.LoadScene("Lab3_modo_c1");         // carrega a cena do modo c1
    }
    public void StartModoC2()
    {
        SceneManager.LoadScene("Lab3_modo_c2");     //carrega a cena do modo c2
    }

    public void Retry()     //funcao que carrega  a cena do modo de jogo de acordo com o que o usuario setou por ultimo
    {
        switch (modoDeJogoAnterior)
        {
            case 0:
                SceneManager.LoadScene("Lab3"); //carrega cena no modo feito em aula
                break;
            case 1:
                SceneManager.LoadScene("Lab3_modo_c1"); //carrega a cena do modo c1
                break;
            case 2:
                SceneManager.LoadScene("Lab3_modo_c2"); // carrega a cena do modo c2
                break;

            default:
                SceneManager.LoadScene("Lab3");     // carrega a cena do modo feito em aula por padrao caso o usuario nao tenha setado nada
                break;

        }

    }
    
    public void Quit()
    {
        SceneManager.LoadScene("Lab3_start_menu");  //carrega a cena do menu inicial
    }
    public void Creditos()
    {
        SceneManager.LoadScene("Lab3_Creditos");    //carrega a cena de creditos
    }

}
