using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    private float latitude;
    private float longitude;

    public float secondsBeforeLocationUpdate;

    public Text Error;
    public float Latitude { get => latitude; set => latitude = value; }
    public float Longitude { get => longitude; set => longitude = value; }

    private void Start()
    {
        Error = GameObject.Find("txtError").GetComponent<Text>();
        StartCoroutine(StartLocationService());
    }
    private IEnumerator StartLocationService()
    {
        Error.text += "\n Checking for location";
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Error.text +="\n User has not enabled GPS";
            yield break;
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
            Error.text += "\n Error: Timed out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Error.text += "\n Error: Unable to determine device location";
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            //Error.text += "\n Location Details \n Latitude: " + Input.location.lastData.latitude + "\n Longitude: " + Input.location.lastData.longitude + "\n Altitude: " + Input.location.lastData.altitude + "\n Horizontal Accuracy: " + Input.location.lastData.horizontalAccuracy + "\n TimeStamp: " + Input.location.lastData.timestamp;
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            //Error.text += "\n latitude: "+ Input.location.lastData.latitude;
            //Error.text += "\n longitude: "+ Input.location.lastData.longitude;
            //Error.text += "\n Giving new corrdinates in 30 seconds \n \n";
            yield return new WaitForSeconds(secondsBeforeLocationUpdate);
            StartCoroutine(StartLocationService());
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}
