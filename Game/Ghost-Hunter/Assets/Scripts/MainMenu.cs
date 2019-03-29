using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Script for Main Menu Scene
/// </summary>
public class MainMenu : MonoBehaviour
{
    public AudioSource Tick;
    public Text txtVersion;

    private GameManager _gameManager;
    private bool _backgroundMute;
    private bool _sfxMute;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        txtVersion.text = "Version: " + Application.version;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // this code is to go to a scene
            //Application.LoadLevel("YourPreviousLevel");
            Application.Quit();
        }
    }
    public void Begin()
    {
        _gameManager.BackgroundMute = _backgroundMute;
        _gameManager.SfxMute = _sfxMute;
        SceneManager.LoadScene("Game");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ToggleBackGroundMusic(Toggle checkBox)
    {
        _backgroundMute = checkBox.isOn;
        Tick.Play();
    }
    public void ToggleSFXMusic(Toggle checkBox)
    {
        _sfxMute = checkBox.isOn;
        Tick.Play();
    }
}
