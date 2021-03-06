﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Class that deals with location stuff
/// </summary>
public class Location : MonoBehaviour
{
    private float latitude;
    private float longitude;

    public Object[] AllGameObjects;
    public float SecondsBeforeLocationUpdate;
    public bool Testing;
    public Text Error;
    public float Latitude { get => latitude; set => latitude = value; }
    public float Longitude { get => longitude; set => longitude = value; }

    private void Start()
    {
        if (Testing)
        {
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
        }
        else
        {

            AllGameObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject));
            foreach (GameObject gameobject in AllGameObjects)
            {
                if (gameobject.name == "txtError")
                {
                    gameobject.SetActive(false);
                    Error = gameobject.GetComponent<Text>();
                    Error.enabled = false;
                }
            }
        }
        StartCoroutine(StartLocationService());
    }
    /// <summary>
    /// Gets the Location data from Mobile Users
    /// MOBILE ONLY!!!
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartLocationService()
    {
        if (Testing)
            Error.text += "\n Checking for location";

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
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
            Error.text += "\n User has not enabled GPS";
            
            yield break;
        }
        else
        {
            if (!Testing)
            {
                AllGameObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject));
                foreach (GameObject gameobject in AllGameObjects)
                {
                    if (gameobject.name == "txtError")
                    {
                        gameobject.SetActive(false);
                        Error = gameobject.GetComponent<Text>();
                        Error.enabled = false;
                    }
                }
            }
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            if (Testing)
                Error.text += "\n Error: Timed out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            if (Testing)
                Error.text += "\n Error: Unable to determine device location";
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;

            if (Testing)
            {
                Error.text = "\n Location Details \n Latitude: " + Input.location.lastData.latitude + "\n Longitude: " + Input.location.lastData.longitude + "\n Altitude: " + Input.location.lastData.altitude + "\n Horizontal Accuracy: " + Input.location.lastData.horizontalAccuracy + "\n TimeStamp: " + Input.location.lastData.timestamp;         
            }
            yield return new WaitForSeconds(SecondsBeforeLocationUpdate);
            StartCoroutine(StartLocationService());
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

    public static float longitudeToScen(float lon)
    {
        return ((lon + 79.22752172f) * 36880.32669263936f) * 10;
    }
    public static float latitudeToScen(float lat)
    {
        return ((lat - 43.78543639f) * 36880.32669263936f) * 10;
    }


    public static float latToZ(double lat)
    {

        lat = (lat - 43.78543639f) / 0.00001 * 0.12179047095976932582726898256213;
        double z = lat;

        return (float)z;
    }

    public static float lonToX(double lon)
    {

        lon = (lon + 79.22752172f) / 0.000001 * 0.00728553580298947812081345114627;
        double x = lon;

        return (float)x;
    }

}
