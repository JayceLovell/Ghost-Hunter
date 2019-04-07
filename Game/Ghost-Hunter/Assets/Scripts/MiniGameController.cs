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
    public bool MiniGameStart;
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
        StartGhost();
    }

    void Init()
    {
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _drawer = GetComponent<LineRenderer>();
        // Add code here to choose ghost from network.
        // Right now its just doing random for testing
        // I recommend doing a method to call it from Game Manager
        _ghostToGet = Ghosts[Random.Range(0, 11)];
    }

    // FixedUpdate is called duh
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && MiniGameStart)
        {
            _drawer.positionCount++;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, 100))
            {
                
                _drawer.SetPosition(_drawer.positionCount -1,_hit.point);
                Instantiate(Obsticle, new Vector3(_hit.point.x,0,_hit.point.z),new Quaternion(0,0,0,0));
            }
        }
        if (Input.GetMouseButtonUp(0) && MiniGameStart)
        {
            _drawer.positionCount = 0;
            _traps = GameObject.FindGameObjectsWithTag("Boarder");
            if (_traps.Length > 0)
            {
                foreach (GameObject trap in _traps)
                {
                    Destroy(trap);
                }
            }
        }
        if (ActiveGhost.GetComponent<GhostMovement>().Stuck && MiniGameStart)
        {
            _drawer.positionCount = 0;
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
        if (HealthBar.BarValue <= 0)
        {
            //Do stuff when ghost die
            SceneManager.UnloadSceneAsync("MinGame");
        }
    }
    /// <summary>
    /// Sets Ghost stats depending on type of Ghost
    /// Example the Difficulty of the Ghost
    /// </summary>
    private void StartGhost()
    {
        _ghoststats = ActiveGhost.GetComponent<GhostMovement>();
        //Speed the Ghost moves around
        _ghoststats.Speed = 10f;
        //How far from position the next random target is set
        //_ghoststats.WanderRadius = 20f; NO LONGER BEING USED
        //Set the level of the ghost by RandomRate
        // Lower the number faster the movement
        _ghoststats.RandomRate = 4f;
        _ghoststats.Stuck = false;
        StartCoroutine(_ghoststats.MoveAgent(5f));
        MiniGameStart = true;
    }
}
