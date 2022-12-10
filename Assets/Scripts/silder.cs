using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class silder : MonoBehaviour
{
    public Image RunSpeed_silder;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float speed = player.GetComponent<PlayerMovment>().sprintAmount / player.GetComponent<PlayerMovment>().MaxSprintSpeed;
        RunSpeed_silder.fillAmount = speed;
        Debug.Log(speed);
    }
}
