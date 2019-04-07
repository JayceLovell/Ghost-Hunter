using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
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
        //PlayerPrefs.DeleteAll();
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            //The user authorized use of the location.
           _userid = PlayerPrefs.GetString("userid", "not logged in");

            if (_userid != "not logged in")
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().userid = _userid;
                SceneManager.LoadScene("MainMenu");
            }

            txtVersion.text = "Version: " + Application.version;
        }else
        {
            // We do not have permission to use the location.
            // Ask for permission or proceed without the functionality enabled.
            Permission.RequestUserPermission(Permission.FineLocation);
        }

    }

    /// <summary>
    /// Attemp toLogin with button press
    /// </summary>
    public void TryLogIn()
    {
        string username = UsernameText.text;
        string password = PasswordText.text;
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            StartCoroutine(GetRequest());
        }
        else
        {
            // We do not have permission to use the location.
            // Ask for permission or proceed without the functionality enabled.
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }
    /// <summary>
    /// Carrys user to Register Scene
    /// </summary>
    public void Register()
    {
        SceneManager.LoadSceneAsync("Register",LoadSceneMode.Additive);
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
            }else
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
