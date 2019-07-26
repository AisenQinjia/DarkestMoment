using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmForLove : MonoBehaviour
{
    public float zoomInSize;
    public float interpolate;
    bool canZoom = false;

    Camera cam;
    void Start()
    {
        cam = Camera.main;
        if (cam == null) Debug.Log("cant find the camera.");
    }
    void Update()
    {
        if (canZoom)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomInSize, interpolate);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject enemy = GameObject.Find("Boss");
        if (other.gameObject.CompareTag(GameDefine.PlayerTag) && enemy == null)
        {
            AudioManager.Instance.StopClip("bgm");
            AudioManager.Instance.StopAllClips();
            AudioManager.Instance.PlayClip("likethelove");
            canZoom = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
