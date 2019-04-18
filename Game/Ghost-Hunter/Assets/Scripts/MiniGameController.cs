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

    Vector3 lastHitPoint = new Vector3(-1000,-1000,-1000);

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
        string id;
        try
        {
             id = GameObject.FindObjectOfType<GameManager>().ghost_id;
        }
        catch (Exception e) {
            id = "";
        }   
        switch (id) {
            case "5c8ae14c08dec30017a19b3e":
                _ghostToGet = Ghosts[3];
                break;
            case "5cafdc975f9359001740f15f":
                _ghostToGet = Ghosts[8];
                break;
            case "5cafdca85f9359001740f160":
                _ghostToGet = Ghosts[2];
                break;
            case "5cafdcb85f9359001740f161":
                _ghostToGet = Ghosts[6];
                break;
            case "5cafdcd65f9359001740f167":
                _ghostToGet = Ghosts[4];
                break;
            case "5cafdcf75f9359001740f168":
                _ghostToGet = Ghosts[0];
                break;
            case "5cafdd0b5f9359001740f169":
                _ghostToGet = Ghosts[9];
                break;
            case "5cafdd325f9359001740f16b":
                _ghostToGet = Ghosts[1];
                break;
            case "5cafdd485f9359001740f16c":
                _ghostToGet = Ghosts[5];
                break;
            case "5cafdd6b5f9359001740f16d":
                _ghostToGet = Ghosts[7];
                break;
            default:
                _ghostToGet = Ghosts[3];
                break;

        }
       // _ghostToGet = Ghosts[Random.Range(0, 11)];
    }

    // FixedUpdate is called duh
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && MiniGameStart)
        {
            _drawer.positionCount++;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, 100))
            {
                if (lastHitPoint == new Vector3(-1000, -1000, -1000))
                {
                    _drawer.SetPosition(_drawer.positionCount - 1, _hit.point);
                    Instantiate(Obsticle, new Vector3(_hit.point.x, 0, _hit.point.z), new Quaternion(0, 0, 0, 0));
                    lastHitPoint = new Vector3(_hit.point.x, 0, _hit.point.z);
                }
                else {
                    Vector3 newHitPoint = new Vector3(_hit.point.x, 0, _hit.point.z);
                    float distance = Vector3.Distance(lastHitPoint, newHitPoint) * 2;
                    int cubeCount = (int) ( distance  / 2);
                    Debug.Log("Cube count " + cubeCount);
                    Debug.Log("Distance " + Vector3.Distance(lastHitPoint, newHitPoint) );
                    Vector3 newLastHitpoint = lastHitPoint;

                    for (int i = 1; i <= cubeCount; i++) {
                        float offset =  (float)i / (float)cubeCount;
                        Debug.Log("offset " + offset);
                        Vector3 spawnPos = Vector3.Lerp(lastHitPoint, newHitPoint, offset);
                        GameObject obj = Instantiate(Obsticle, spawnPos, new Quaternion(0, 0, 0, 0));
                        newLastHitpoint = obj.transform.position;
                    }

                    lastHitPoint = newLastHitpoint;



                    _drawer.SetPosition(_drawer.positionCount - 1, _hit.point);

                }



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

            lastHitPoint = new Vector3(-1000, -1000, -1000);
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
        HealthBar.BarValue -= Random.Range(10, 20);
        if (HealthBar.BarValue <= 0)
        {
            //Do stuff when ghost die
            //SceneManager.LoadScene("Game");
            //SceneManager.UnloadSceneAsync("MinGame");
            GameObject.FindObjectOfType<GameManager>().Catch();
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
