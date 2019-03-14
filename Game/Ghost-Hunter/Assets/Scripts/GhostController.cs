using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {

    public Vector3 wayPoint = new Vector3(0, 0, 0);
    public int difficulty = 1;
    float speed;

    // Use this for initialization
    void Start () {
        switch (difficulty)
        {
            case 1:
                speed = 1;
                break;
            case 2:
                speed = 5;
                break;
            case 3:
                speed = 10;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(transform.position, wayPoint) > 0.1f )
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
        }
        else
        {
            wayPoint = new Vector3(Random.Range(-5, 5), Random.Range(-3, 3), 0);
        }

    }
}
