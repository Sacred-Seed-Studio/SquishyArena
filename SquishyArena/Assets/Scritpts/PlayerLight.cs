using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLight : MonoBehaviour
{
    public Image lightImage;
    public GameObject lightCanvas;

    Color dimColor;
    Color brightColor;

    bool flashing;

    void Awake()
    {
        dimColor = lightImage.color;
        brightColor = new Color(dimColor.r, dimColor.g, dimColor.b, dimColor.a + (10 / 255f));
    }

    void Start()
    {
        Flash();
    }

    public void TurnOff()
    {
        flashing = false;
        lightCanvas.gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        lightCanvas.gameObject.SetActive(true);
        Flash();
    }

    public void Flash()
    {
        StartCoroutine(StartFlash());
    }

    [Range(0.001f, 0.1f)]
    public float inc = 0.1f;
    IEnumerator StartFlash()
    {
        flashing = true;
        float percent = 0;
        while (flashing)
        {
            percent = 0;
            while (percent < 1)
            {
                lightImage.color = Color.Lerp(dimColor, brightColor, percent);
                percent += inc;
                yield return null;
            }
            percent = 0;
            while (percent < 1)
            {
                lightImage.color = Color.Lerp(brightColor, dimColor, percent);
                percent += inc;
                yield return null;
            }
        }
        yield return null;
    }

}






