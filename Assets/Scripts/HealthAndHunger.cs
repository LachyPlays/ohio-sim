using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndHunger : MonoBehaviour
{
    public int player_health = 100;
    public GameObject player;
    public int player_hunger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void take_damage()
    {
        if (player_health <= 0)
        {
            Destroy(player.gameObject);
        }

        if(player_hunger <= 0)
        {
            player_health -= 1;
        }

        player_health -= 1;
    }

    public void GiveHealth()
    {
        player_health += 1;
    }

    public void fixHunger()
    {
        player_hunger += 1;
    }
}
