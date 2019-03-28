using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnedGhost : MonoBehaviour
{

    public string id;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    void OnMouseDown()
    {
        // this object was clicked - do something
        Destroy(this.gameObject);
        SceneManager.LoadScene("MiniGame");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
