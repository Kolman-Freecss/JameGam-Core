using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayers : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject room1;
    [SerializeField] GameObject room2;
    void Start()
    {
        
    }

    void Update()
    {
        if(player.transform.position.y > -76)
        {
            wall.SetActive(false); 
        }
        else
        {
            wall.SetActive(true);
        }
        if(player.transform.position.y > -129 && player.transform.position.x > 330)
        {
            room1.SetActive(false);
            room2.SetActive(true);
        }
        else if(player.transform.position.y < -129 && player.transform.position.x > 330)
        {
            room1.SetActive(true);
            room2.SetActive(false);
        }
    }
}
