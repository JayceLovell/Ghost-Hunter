using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerLocation : MonoBehaviour
{
    private Location Location;

    public float TestLongitude = 0;
    public float TestLatitude = 0;
    public Vector3 testpos;
   // public GameObject Map;
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
            //TO DO : Scaling
            float latScale = 287 / 180;  //latitude goes from 0(Npole) to 180(Spole)
            float longiScale = 720 / 360; //longitude goes from 0 to 360 (Greenwich)
            testpos = new Vector3((TestLatitude * latScale), 0, (TestLongitude * longiScale));
            transform.position = new Vector3((TestLatitude * latScale), 0, (TestLongitude * longiScale));
            //transform.position = Quaternion.AngleAxis(TestLongitude, Vector3.up) * Quaternion.AngleAxis(TestLatitude, Vector3.right) * new Vector3(0, 0, 1);
            //transform.position = new Vector3(TestLongitude,0, TestLatitude);
        }
        else
        {
            transform.position = new Vector3(Location.Longitude, 0,Location.Latitude);
        }
    }
}
