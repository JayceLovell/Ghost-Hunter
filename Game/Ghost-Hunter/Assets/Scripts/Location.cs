using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    public float latitude;
    public float longitude;
    public Text Error;
    private void Start()
    {
        StartCoroutine(StartLocationService());
    }
    void Update()
    {        
        StartCoroutine(StartLocationService());
    }
    private IEnumerator StartLocationService()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Error.text ="Error: User has not enabled GPS";
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
            Error.text = "Error: Timed out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Error.text += "Error: Unable to determine device location";
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            //Error.text += "Error: Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}
