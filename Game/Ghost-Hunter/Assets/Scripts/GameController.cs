using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private UpdatePlayerLocation updatePlayerLocation;

    public Object[] AllGameObjects;
    public GameObject Player;
    private bool playerOutOfBounds;
    public Vector3 Playerpositions;
    public float distanceBetweenX;
    public float distanceBetweenZ;
    public bool XoutOfBounds;
    public bool ZoutOfBounds;
    public Text Error;
    public bool Testing;

    // Start is called before the first frame update
    void Start()
    {
        if (Testing)
        {
            lookForErrorText(true);  
        }
        else
        {
            lookForErrorText(false);
        }
        Player = GameObject.FindGameObjectWithTag("Player");
        updatePlayerLocation = GetComponent<UpdatePlayerLocation>();
        playerOutOfBounds = false;
    }

    // Update is called once per frame
    void Update()
    {
        //sets player position
        Player.transform.position = updatePlayerLocation.PlayerGpsLocation;

        Playerpositions = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
        distanceBetweenX = this.gameObject.transform.position.x - Player.transform.position.x;
        distanceBetweenZ = this.gameObject.transform.position.z - Player.transform.position.z;
        XoutOfBounds = (distanceBetweenX < -114.4000f || distanceBetweenX > 171.0600f);
        ZoutOfBounds = (distanceBetweenZ < -82.6800f || distanceBetweenZ > 99.0200f);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // this code is to go to a scene
            //Application.LoadLevel("YourPreviousLevel");
            Application.Quit();
        }   
    }
    void LateUpdate()
    {
        StartCoroutine(CheckIfPlayerInGameZone());
    }
    private IEnumerator CheckIfPlayerInGameZone()
    {
        yield return new WaitForSeconds(10);
        //lookForErrorText(true);
        if (distanceBetweenX < -114 || distanceBetweenX > 171.06 || distanceBetweenZ < -82.68 || distanceBetweenZ > 99.02)
        {
            //Error.text += "\n Out of Bounds \n X: " + XoutOfBounds + " \n Y: " + ZoutOfBounds;
            //Error.text += "\n " + distanceBetweenX + "\n " + distanceBetweenZ;
            playerOutOfBounds = true;
        }
        if (playerOutOfBounds)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void lookForErrorText(bool value)
    {
        AllGameObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        foreach (GameObject gameobject in AllGameObjects)
        {
            if (gameobject.name == "txtError")
            {
                gameobject.SetActive(value);
                Error = gameobject.GetComponent<Text>();
                Error.enabled = value;
                Error.text = "Testing out of Bounds";
            }
        }
    }
}
