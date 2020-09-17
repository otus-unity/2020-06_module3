
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestDemo : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(MakeRequest());
    }

    IEnumerator MakeRequest()
    {
        Debug.Log("Sending request");

        UnityWebRequest req = UnityWebRequest.Get("http://localhost/add.php?a=3&b=5");
        yield return req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError)
            Debug.LogError($"Server error: {req.error}");
        else
            Debug.Log($"Server response: {req.downloadHandler.text}");
    }
}
