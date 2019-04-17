using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SpawnedGhost : MonoBehaviour
{

    public string id;
    public GameObject ghostPrefab;
    public GameObject[] allghosts;//= new Dictionary<string, GameObject>();
                                  // Start is called before the first frame update

    void Start()
    {
        

    }

    void OnMouseDown()
    {
        GameObject.FindObjectOfType<GameManager>().ghost_id = id;
        // this object was clicked - do something
        Destroy(this.gameObject);
        //SceneManager.LoadSceneAsync("MiniGame", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("MiniGame");

    }


    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        Destroy(this.gameObject);
    //        SceneManager.LoadSceneAsync("MiniGame");
    //    }
    //}
}
