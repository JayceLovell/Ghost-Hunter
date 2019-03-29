using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// The Almighty Game Controller
/// </summary>
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    public string userid;

    private bool _sfxMute;
    private bool _backgroundMute;

    public bool SfxMute {
        get {
            return _sfxMute;
        }
        set {
            _sfxMute = value;
        }
    }
    
    public bool BackgroundMute
    {
        get
        {
            return _backgroundMute;
        }
        set
        {
            _backgroundMute = value;
        }
    }

    
    /// <summary>
    /// Awake is always called before any Start functions
    /// Makes GameManager Omnipresent
    /// </summary>
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }

    //Update is called every frame.
    void Update()
    {

    }
}
