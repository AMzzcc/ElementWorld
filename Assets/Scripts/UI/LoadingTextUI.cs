using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTextUI : MonoBehaviour
{
    public float animationTime = 3.0f;

    private Text text;
    private float timer = 0.0f;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoTextAnimation());
    }

    IEnumerator DoTextAnimation()
    {
        while (this.enabled)
        {
            text.text = "loading";
            yield return new WaitForSeconds(animationTime / 4.0f);
            text.text = "loading.";
            yield return new WaitForSeconds(animationTime / 4.0f);
            text.text = "loading..";
            yield return new WaitForSeconds(animationTime / 4.0f);
            text.text = "loading...";
            yield return new WaitForSeconds(animationTime / 4.0f);
        }
    }
}
