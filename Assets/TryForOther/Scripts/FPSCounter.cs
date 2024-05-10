using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    private float _fps;
    private int _frameCount;
    private float _time;
    public TextMeshProUGUI fpsText;

    private void Start()
    {
        _frameCount = 0;
        _time = 0.0f;
        Application.targetFrameRate = 60; // FPS'yi 60'a sýnýrla
    }

    private void Update()
    {
        _frameCount++;
        _time += Time.deltaTime;

        if (_time >= 0.1f)
        {
            _fps = _frameCount / _time;
            fpsText.text = "FPS: " + _fps.ToString("F1");
            _frameCount = 0;
            _time = 0.0f;
        }
    }
}
