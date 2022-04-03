using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManageBotoes : MonoBehaviour
{
    public int modoDeJogoAnterior = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt("Jogadas", 0);
        modoDeJogoAnterior = PlayerPrefs.GetInt("modoAnterior", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartModoTradicional()
    {
        SceneManager.LoadScene("Lab3");
    }
    public void StartModoC1()
    {
        SceneManager.LoadScene("Lab3_modo_c1");
    }
    public void StartModoC2()
    {
        SceneManager.LoadScene("Lab3_modo_c2");
    }

    public void Retry()
    {
        switch (modoDeJogoAnterior)
        {
            case 0:
                SceneManager.LoadScene("Lab3");
                break;
            case 1:
                SceneManager.LoadScene("Lab3_modo_c1");
                break;
            case 2:
                SceneManager.LoadScene("Lab3_modo_c2");
                break;

            default:
                SceneManager.LoadScene("Lab3");
                break;

        }

    }
    
    public void Quit()
    {
        SceneManager.LoadScene("Lab3_start_menu");
    }
    public void Creditos()
    {
        SceneManager.LoadScene("Lab3_Creditos");
    }

}
