﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This Updates Player Location Base on Data from Location Class
/// <see cref="Location"/>
/// </summary>
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

    public GameObject northWestLocationObject;
    public GameObject southEastLocationObject;

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


            float latScale = 61.4f / 0.000581f;  //latitude goes from 0(Npole) to 180(Spole)
            float longiScale = 50.8f / 0.000656f; //longitude goes from 0 to 360 (Greenwich)
            

            float latToPos = Location.latToZ(TestLatitude);
            float lonToPos = Location.lonToX(TestLongitude);
            Vector2 currentLatLong = new Vector2(TestLatitude, TestLongitude);

            playerGpsLocation = LatLon.GetUnityPosition(currentLatLong, northWestLocationObject.GetComponent<LocationMarker>().LatLon, southEastLocationObject.GetComponent<LocationMarker>().LatLon, northWestLocationObject.transform.position, southEastLocationObject.transform.position);


            //testpos = new Vector3(latToPos, 0, lonToPos);
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
            //playerGpsLocation = new Vector3(lonToPos, 0, latToPos);
        }
        else
        {
            float latToPos = Location.latitudeToScen(Location.Latitude);
            float lonToPos = Location.longitudeToScen(Location.Longitude);
            Vector2 currentLatLong = new Vector2(Location.Latitude, Location.Longitude);

            playerGpsLocation = LatLon.GetUnityPosition(currentLatLong, northWestLocationObject.GetComponent<LocationMarker>().LatLon, southEastLocationObject.GetComponent<LocationMarker>().LatLon, northWestLocationObject.transform.position, southEastLocationObject.transform.position);


        }

    }
}
