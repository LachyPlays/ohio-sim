using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Global_Volume;
    // Start is called before the first frame update
    void Start()
    {
        Highs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Highs()
    {
        QualitySettings.SetQualityLevel(6);
        Global_Volume.SetActive(true);
    }


    public void low()
    {
        QualitySettings.SetQualityLevel(0);
        Global_Volume.SetActive(false);
    }

    public void med()
    {
        QualitySettings.SetQualityLevel(3);
        Global_Volume.SetActive(true);
    }

    public void MainGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}
