using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change : MonoBehaviour
{
    public void loading()
    {
        SceneManager.LoadScene("Loading");
    }
}
