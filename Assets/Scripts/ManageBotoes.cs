using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManageBotoes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt("Jogadas", 0);

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
        SceneManager.LoadScene("SampleScene");
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
