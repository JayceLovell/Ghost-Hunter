using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{

    public GameObject inventoryPanel;
    public GameObject inventoryList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowInventory() {
        inventoryPanel.SetActive(true);
        for (int i = 0; i < inventoryList.transform.childCount; i++) {
            inventoryList.transform.GetChild(i).GetComponent<Image>().color = new Color32(255, 255, 225, 20);
        }

        StartCoroutine(GetInventory());
    }



    IEnumerator GetInventory()
    {
        string userId = GameObject.FindObjectOfType<GameManager>().userid;
        WWW www = new WWW("https://ghost-hunter-game.herokuapp.com/game/inventory/get?user_id=" + userId);

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

        string response = www.text;
        WebInventory[] newInventory = JsonHelper.getJsonArray<WebInventory>(response);
        Debug.Log(newInventory);
        foreach (WebInventory e in newInventory)
        {
            Debug.Log(e.ghost_id);

            switch (e.ghost_id)
            {
                case "5c8ae14c08dec30017a19b3e":
                    inventoryList.transform.GetChild(3).GetComponent<Image>().color = new Color32(147, 147, 147, 100);
                    break;
                case "5cafdc975f9359001740f15f":
                    inventoryList.transform.GetChild(8).GetComponent<Image>().color = new Color32(147, 147, 147, 100);
                    break;
                case "5cafdca85f9359001740f160":
                    inventoryList.transform.GetChild(2).GetComponent<Image>().color = new Color32(147, 147, 147, 100);
                    break;
                case "5cafdcb85f9359001740f161":
                    inventoryList.transform.GetChild(6).GetComponent<Image>().color = new Color32(147, 147, 147, 100);
                    break;
                case "5cafdcd65f9359001740f167":
                    inventoryList.transform.GetChild(4).GetComponent<Image>().color = new Color32(147, 147, 147, 100);
                    break;
                case "5cafdcf75f9359001740f168":
                    inventoryList.transform.GetChild(0).GetComponent<Image>().color = new Color32(147, 147, 147, 100);
                    break;
                case "5cafdd0b5f9359001740f169":
                    inventoryList.transform.GetChild(9).GetComponent<Image>().color = new Color32(147, 147, 147, 100);
                    break;
                case "5cafdd325f9359001740f16b":
                    inventoryList.transform.GetChild(1).GetComponent<Image>().color = new Color32(147, 147, 147, 100);
                    break;
                case "5cafdd485f9359001740f16c":
                    inventoryList.transform.GetChild(5).GetComponent<Image>().color = new Color32(147, 147, 147, 100);
                    break;
                case "5cafdd6b5f9359001740f16d":
                    inventoryList.transform.GetChild(7).GetComponent<Image>().color = new Color32(147, 147, 147, 100);
                    break;

            }
        }

        //Debug.Log(response);

    }

    public void HideInventory() {
        inventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    public class WebInventory
    {
        public string ghost_id;
    }
}


