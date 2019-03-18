using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    private bool playerOutOfBounds;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerOutOfBounds = false;
        CheckIfPlayerInGameZone();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CheckIfPlayerInGameZone()
    {
        if(Player.transform.position.x < -171.27 || Player.transform.position.x > 114.7)
        {
            playerOutOfBounds = true;
        }
        if(Player.transform.position.y > 82.6 || Player.transform.position.y < -99.1)
        if (playerOutOfBounds)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
