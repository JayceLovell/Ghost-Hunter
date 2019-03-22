using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
/// <summary>
/// Controller for MiniGame Scene
/// </summary>
public class MiniGameController : MonoBehaviour
{
    public GameObject Obsticle;
    public GameObject ActiveGhost;
    public RaycastHit hit;
    public GameObject[] Ghosts;

    private GameObject _ghostToGet;
    private LineRenderer _drawer;
    private GameObject[] _traps;
    private GameManager _gameManager;
    private GhostMovement _ghoststats;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        Instantiate(_ghostToGet, new Vector3(0,0,0),Quaternion.identity);
        ActiveGhost = GameObject.Find(_ghostToGet.name);
        SetGhostStats();
    }

    void Init()
    {
        _drawer = GetComponent<LineRenderer>();
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Add code here to chose ghost from network. this random is only here for testing
        _ghostToGet = Ghosts[Random.Range(0, 11)];
        _ghoststats = _ghostToGet.GetComponent<GhostMovement>();
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
        if (_ghostToGet.GetComponent<GhostMovement>().Stuck)
        {
            _traps = GameObject.FindGameObjectsWithTag("Boarder");
            foreach(GameObject trap in _traps)
            {
                Destroy(trap);
            }
            _ghostToGet.GetComponent<GhostMovement>().Stuck = false;
        }
    }
    /// <summary>
    /// Sets Ghost stats depending on type of Ghost
    /// </summary>
    private void SetGhostStats()
    {
        _ghoststats.Speed = 6f;
        _ghoststats.WanderRadius = 10f;
        _ghoststats.RandomRate = 4f;
    }
}
