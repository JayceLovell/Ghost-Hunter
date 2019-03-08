using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerLocation : MonoBehaviour
{
    private Location Location;

    public float TestLongitude = 0;
    public float TestLatitude = 0;
    public GameObject Map;
    // Start is called before the first frame update
    void Start()
    {
        Location = GetComponent<Location>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TestLatitude != 0 && TestLongitude != 0)
        {
            transform.position = new Vector3(TestLongitude,0, TestLatitude);
        }
        else
        {
            transform.position = new Vector3(Location.Longitude, 0,Location.Latitude);
        }
    }
}
