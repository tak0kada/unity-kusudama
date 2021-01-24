using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    // ref: https://baba-s.hatenablog.com/entry/2017/12/20/000200
    private const float updateInterval = 0.5f;

    private float accum;
    private int frames;
    private float timeleft = updateInterval;
    private float fps;
private Text text;


private void Awake()
{
    text = GetComponent<Text>();
}

    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        if ( 0 < timeleft )
        {
            return;
        }

        fps = accum / frames;
        timeleft = updateInterval;
        accum = 0;
        frames = 0;
        // Debug.Log("FPS: " + fps.ToString("f2"));
        text.text = "FPS: " + fps.ToString("f2");
    }
}
