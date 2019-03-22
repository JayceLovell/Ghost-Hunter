using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroySelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyMe());
    }
    IEnumerator destroyMe()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
