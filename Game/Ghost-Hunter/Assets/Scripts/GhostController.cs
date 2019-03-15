using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostController : MonoBehaviour {

    private int count = 3;
    private float speed;
    private bool ghostCaught = false;
    private bool ghostMove = false;
    public Text countText;
    public Text winText;
    public Vector3 wayPoint = new Vector3(0, 0, 0);
    public int difficulty = 1;

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
            case 4:
                speed = 15;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {

        countText.text = count.ToString();

        while (count > 0) {
            count--;
        }

        if (count <= 0)
        {
            ghostMove = true;
        }

        if (ghostMove == true)
        {
            if (Vector3.Distance(transform.position, wayPoint) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
            }
            else
            {
                wayPoint = new Vector3(Random.Range(-5, 5), Random.Range(-3, 3), 0);
            }
        }

    }


}
