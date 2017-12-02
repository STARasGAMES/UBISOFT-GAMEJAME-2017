using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour {

    [SerializeField] private float _fadeSpeed = 1.0f;
    Image _image;
    private const float MIN_ALPHA = 0;
    private const float MAX_ALPHA = 1.0f;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public IEnumerator FadeOut(bool resetAlpha = false)
    {
        var alpha = resetAlpha
            ? MAX_ALPHA
            : GetAlpha();

        while (alpha > MIN_ALPHA)
        {
            yield return null;
            alpha -= _fadeSpeed * Time.deltaTime;
            SetAlpha(alpha);
        }

        SetAlpha(MIN_ALPHA);
    }

    public IEnumerator FadeIn(bool resetAlpha = false)
    {
        var alpha = resetAlpha
            ? MIN_ALPHA
            : GetAlpha();

        while (alpha < MAX_ALPHA)
        {
            yield return null;
            alpha += _fadeSpeed * Time.deltaTime;
            SetAlpha(alpha);
        }

        SetAlpha(MAX_ALPHA);
    }

    public void SetAlpha(float alpha)
    {
        Color c = _image.color;
        c = new Color(c.r, c.g, c.b, alpha);
        _image.color = c;
    }

    public float GetAlpha()
    {
        return _image.color.a;
    }
}
