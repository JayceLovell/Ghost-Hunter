﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    public InputField UsernameText;
    public InputField PasswordText;
    public Text txtVersion;
    private string _userid;

    // Start is called before the first frame update
    void Start()
    {
        _userid = PlayerPrefs.GetString("userid", "not logged in");

        if (_userid != "not logged in") {
            GameObject.Find("GameManager").GetComponent<GameManager>().userid = _userid;
            SceneManager.LoadScene("MainMenu");
        }
        txtVersion.text = "Version: " + Application.version;
    }

    // Update is called once per frame
    public void TryLogIn()
    {
        string username = UsernameText.text;
        string password = PasswordText.text;
        StartCoroutine(GetRequest());
    }

    IEnumerator GetRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", UsernameText.text);
        form.AddField("password", PasswordText.text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://ghost-hunter-game.herokuapp.com/game/login", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string response =  www.downloadHandler.text;
                if (response != "login error" && response != "incorrect password") {
                    string userid = www.downloadHandler.text;
                    GameObject.Find("GameManager").GetComponent<GameManager>().userid = userid;
                    PlayerPrefs.SetString("userid", userid);
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }


    }
}