using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Draws on the screen to indicate the player catching a ghost
/// </summary>
public class LineDrawer : MonoBehaviour
{
    private LineRenderer line;
    private Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.positionCount++;
            line.SetPosition(line.positionCount - 1, mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            line.positionCount = 0;
        }
    }
}
