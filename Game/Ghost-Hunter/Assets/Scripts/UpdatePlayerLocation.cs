using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerLocation : MonoBehaviour
{
    private Location Location;
    private Vector3 playerGpsLocation;

    public float TestLongitude = 0;
    public float TestLatitude = 0;
    public Vector3 testpos;
    public bool Testing;
    public Text Error;
    public Object[] AllGameObjects;

    public Vector3 PlayerGpsLocation { get => playerGpsLocation; set => playerGpsLocation = value; }

    // public GameObject Map;
    // Start is called before the first frame update
    void Start()
    {
        Location = GetComponent<Location>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Testing)
        {
            //TO DO : Scaling
            //float latScale = 287 / 43;  //latitude goes from 0(Npole) to 180(Spole)
            //float longiScale = 190 / -79; //longitude goes from 0 to 360 (Greenwich)
            //testpos = new Vector3((TestLatitude * latScale), 0, (TestLongitude * longiScale));
            //transform.position = new Vector3((TestLatitude * latScale), 0, (TestLongitude * longiScale));
            //transform.position = Quaternion.AngleAxis(TestLongitude, Vector3.up) * Quaternion.AngleAxis(TestLatitude, Vector3.right) * new Vector3(0, 0, 1);

            float latToPos = (TestLongitude + 79.22752172f) * 36880.32669263936f;
            float lonToPos = (TestLatitude - 43.78543639f) * 36880.32669263936f;
            testpos = new Vector3(latToPos, 0, lonToPos);
            AllGameObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject));
            foreach (GameObject gameobject in AllGameObjects)
            {
                if (gameobject.name == "txtError")
                {
                    gameobject.SetActive(true);
                    Error = gameobject.GetComponent<Text>();
                    Error.enabled = true;
                }
            }
            playerGpsLocation = new Vector3(latToPos, 0, lonToPos);
        }
        else
        {
            float latToPos = (Location.Longitude + 79.22752172f) * 36880.32669263936f;
            float lonToPos = (Location.Latitude - 43.78543639f) * 36880.32669263936f;

            playerGpsLocation = new Vector3(latToPos, 0, lonToPos);

        }

    }
}
