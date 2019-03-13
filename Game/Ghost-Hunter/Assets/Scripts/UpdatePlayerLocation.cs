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
        float inX, inY;
        if(TestLatitude != 0 && TestLongitude != 0)
        {
            //TO DO : Scaling
            //float latScale = 287 / 43;  //latitude goes from 0(Npole) to 180(Spole)
            //float longiScale = 190 / -79; //longitude goes from 0 to 360 (Greenwich)
            //testpos = new Vector3((TestLatitude * latScale), 0, (TestLongitude * longiScale));
            //transform.position = new Vector3((TestLatitude * latScale), 0, (TestLongitude * longiScale));
            //transform.position = Quaternion.AngleAxis(TestLongitude, Vector3.up) * Quaternion.AngleAxis(TestLatitude, Vector3.right) * new Vector3(0, 0, 1);
            inX = TestLatitude;
            inY = TestLongitude;
        }
        else
        {
            inX = Location.Latitude; 
            inY = Location.Longitude;

        }

        float latToPos = (inY + 79.22752172f) * 36880.32669263936f;
        float lonToPos = (inX - 43.78543639f) * 36880.32669263936f;

        transform.position = new Vector3(latToPos, 0, lonToPos);

    }
}
