using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// The Almighty Game Controller
/// </summary>
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public GameObject[] allghosts;

    //the userid from the server or preferences
    public string userid;
    //the ghost id used to spawn minigame prefab and for inventory catching
    public string ghost_id;
    public GameObject miniGamePrefab;
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

    public void Catch() {
        StartCoroutine(PostCatch());
    }

    //post request to server to catch ghost
    IEnumerator PostCatch()
    {

        Debug.Log("start catch");

        WWWForm form = new WWWForm();

        form.AddField("ghost_id", ghost_id);
        form.AddField("user_id", PlayerPrefs.GetString("userid"));

        using (UnityWebRequest www = UnityWebRequest.Post("https://ghost-hunter-game.herokuapp.com/game/inventory/catch", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log(response);
                ghost_id = "";
                SceneManager.LoadScene("Game");

            }
        }
    }

    //Update is called every frame.
    void Update()
    {

    }
}
