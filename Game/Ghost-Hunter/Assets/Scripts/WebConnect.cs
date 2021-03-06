﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebConnect : MonoBehaviour
{
    float lastCheckTime = 0;
    public float checkInterval;
    public GameObject northWestLocationObject;
    public GameObject southEastLocationObject;

    public GameObject ghostSpawnPrefab;
    // Start is called before the first frame update
    void Update()
    {

        //calles event get request from server at a interval
        if ( Time.time >= lastCheckTime + checkInterval)
        {
            StartCoroutine(GetRequest("https://ghost-hunter-game.herokuapp.com/game/events"));
        }

    }


    //performs the get request for events
    IEnumerator GetRequest(string uri)
    {
        lastCheckTime = Time.time;
        WWW www = new WWW("https://ghost-hunter-game.herokuapp.com/game/events");

        float elapsedTime = 0.0f;

        while (!www.isDone)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= 10.0f) break;

            yield return null;
        }

        if (!www.isDone || !string.IsNullOrEmpty(www.error))
        {
            Debug.LogError(string.Format("www error\n{0}", www.error));
            yield break;
        }

        //converts server response into object array of events so we can query it.
        string response = www.text;
        WebEvent[] newEvents = JsonHelper.getJsonArray<WebEvent>(response);

        List<SpawnedGhost> sceneGhosts = new List<SpawnedGhost>(GameObject.FindObjectsOfType<SpawnedGhost>()); ;
        foreach (WebEvent e in newEvents) {
            bool alreadySpawned = false;
            //compare if ghost is already spawned

            foreach (SpawnedGhost sceneGhost in sceneGhosts) {
                if (sceneGhost.eventid == e._id) {
                    alreadySpawned = true;
                    break;
                }
            }

            //if events arent spawned then spawn them
            if (!alreadySpawned) {
                Vector2 currentLatLong = new Vector2(e.latitude, e.longitude);
                Vector3 pos = LatLon.GetUnityPosition(currentLatLong, northWestLocationObject.GetComponent<LocationMarker>().LatLon, southEastLocationObject.GetComponent<LocationMarker>().LatLon, northWestLocationObject.transform.position, southEastLocationObject.transform.position);
                pos.y = 0;
                GameObject newGhost = Instantiate(ghostSpawnPrefab, pos , Quaternion.identity)as GameObject;
                newGhost.GetComponent<SpawnedGhost>().id = e.ghost_id;
                Destroy(newGhost, e.expireTime);
            }
        }

        //Debug.Log(response);
    
    }


    
}

//class used to hold json event from server
[Serializable]
public class WebEvent
{
    public float expireTime;
    public string _id;
    public float longitude;
    public float latitude;
    public string ghost_id;
    public int rarity;
}


//class to let unity json parser convert to arrays
public class JsonHelper{

    //Usage:
    //YouObject[] objects = JsonHelper.getJsonArray<YouObject> (jsonString);
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }
    //Usage:
    //string jsonString = JsonHelper.arrayToJson<YouObject>(objects);
    public static string arrayToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.array = array;
        return JsonUtility.ToJson(wrapper);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] array;
}
}
