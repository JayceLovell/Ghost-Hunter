using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
/// <summary>
/// Controller for MiniGame Scene
/// </summary>
public class MiniGameController : MonoBehaviour
{
    [Header("Ghost Health")]
    public GhostHealth HealthBar;

    [Header("Prefabs")]
    public GameObject Obsticle;    
    public GameObject[] Ghosts;

    [Space()]
    public GameObject ActiveGhost;

    private RaycastHit _hit;
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
        ActiveGhost = GameObject.FindGameObjectWithTag("Ghost");
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

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, 100))
            {
                
                _drawer.SetPosition(_drawer.positionCount -1,_hit.point);
                Instantiate(Obsticle, new Vector3(_hit.point.x,0,_hit.point.z),new Quaternion(0,0,0,0));
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _drawer.positionCount = 0;
            foreach (GameObject trap in _traps)
            {
                Destroy(trap);
            }
        }
        if (ActiveGhost.GetComponent<GhostMovement>().Stuck)
        {
            GhostHealth();
            _traps = GameObject.FindGameObjectsWithTag("Boarder");
            foreach(GameObject trap in _traps)
            {
                Destroy(trap);
            }
            ActiveGhost.GetComponent<GhostMovement>().Stuck = false;
        }
    }
    /// <summary>
    /// Decrease Ghost Health Bar
    /// also if ghost dies it does other stuff
    /// </summary>
    private void GhostHealth()
    {
        HealthBar.BarValue -= Random.Range(1, 10);
        if (HealthBar.BarValue >= 0)
        {
            //Do stuff when ghost die
            SceneManager.LoadScene("Game");
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
