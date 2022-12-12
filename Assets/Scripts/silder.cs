using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class silder : MonoBehaviour
{
    public Image RunSpeed_silder;
    public Image Bg;
    public GameObject player;
    public GameObject run;
    // Start is called before the first frame update
    void Start()
    {
    

    }

    // Update is called once per frame
    void Update()
    {

        float speed = player.GetComponent<PlayerMovment>().sprintAmount / player.GetComponent<PlayerMovment>().MaxSprintSpeed;
        RunSpeed_silder.fillAmount = speed;


        if (RunSpeed_silder.fillAmount <= 0)
        {
            StartCoroutine(wait());
        }

        if (RunSpeed_silder.fillAmount >= 1)
        {
            //max
            StartCoroutine(wait());
        }

        if(RunSpeed_silder.fillAmount > 0)
        {
            if (RunSpeed_silder.fillAmount != 1)
            {
                run.SetActive(true);
            }
          
        }

        //Debug.Log(speed);
    }


    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        run.SetActive(false);

    }
   
}
