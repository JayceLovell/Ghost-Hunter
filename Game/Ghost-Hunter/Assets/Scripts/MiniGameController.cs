using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MiniGameController : MonoBehaviour
{
    public GameObject Obsticle;
    public GameObject ActiveGhost;
    public RaycastHit hit;
    public GameObject[] Ghosts;
    public Transform Spawn;

    private LineRenderer _drawer;
    private GameObject[] _traps;
    private GameManager _gameManager;
    private GhostMovement _ghoststats;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        Instantiate(ActiveGhost, Spawn);
        SetGhostStats();
    }

    void Init()
    {
        //Spawn = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Transform>();
        _drawer = GetComponent<LineRenderer>();
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Add code here to chose ghost from network. this random is only here for testing
        ActiveGhost = Ghosts[Random.Range(0, 11)];
        _ghoststats = ActiveGhost.GetComponent<GhostMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _drawer.positionCount++;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                
                _drawer.SetPosition(_drawer.positionCount -1,hit.point);
                Instantiate(Obsticle, new Vector3(hit.point.x,0,hit.point.z),new Quaternion(0,0,0,0));
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _drawer.positionCount = 0;
        }
        if (ActiveGhost.GetComponent<GhostMovement>().Stuck)
        {
            _traps = GameObject.FindGameObjectsWithTag("Boarder");
            foreach(GameObject trap in _traps)
            {
                Destroy(trap);
            }
            ActiveGhost.GetComponent<GhostMovement>().Stuck = false;
        }
    }
    /// <summary>
    /// Sets Ghost stats depending on type of Ghost
    /// </summary>
    private void SetGhostStats()
    {
        _ghoststats.Agent.speed =3;
        _ghoststats.WanderRadius = 10;
        _ghoststats.RandomRate = 4;
    }
}
