using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public PlayerMovementV3 playerMovement;
    public Canvas playerUI;

    private Slider sprintBar = null;

    // Start is called before the first frame update
    void Start()
    {
        Slider[] children = playerUI.GetComponentsInChildren<Slider>();

        foreach(Slider child in children)
        {
            if (child.gameObject.name == "Sprint bar")
            {
                sprintBar = child;
            }
        }

        if(sprintBar == null)
        {
            Debug.LogError("Could not find required slider named 'Sprint bar' in player ui");
        }

        sprintBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.sprintRemaining <= playerMovement.sprintTime)
        {
            sprintBar.gameObject.SetActive(true);
            sprintBar.value = playerMovement.sprintRemaining / playerMovement.sprintTime;
        } else
        {
            sprintBar.gameObject.SetActive(false);
        }
    }
}
