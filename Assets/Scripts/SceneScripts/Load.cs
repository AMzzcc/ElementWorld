using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Load : MonoBehaviour
{
    //加载的最少秒数
    [SerializeField]
    private float leastSeconds = 4.0f;

    private AsyncOperation async;
    private int number;

    private void Start()
    {
        number = Storage.GetInstance().Level;
        if(number==0)
        {
            Debug.Log("error");
        }
        Loading();
    }
    // Update is called once per frame
    void Update()
    {
        CheckSceneReady();
    }

    public void Loading()
    {
        async = SceneManager.LoadSceneAsync(number+2);
        async.allowSceneActivation = false;
    }

    void CheckSceneReady() {
        //计时
        leastSeconds -= Time.deltaTime;
        leastSeconds = Mathf.Max(-100.0f, leastSeconds);//防止负溢出
        //若计时到且加载完则进入场景
        if (async.progress > 0.80f && leastSeconds <= 0.0f)
        {
            //Debug.Log("场景加载完成");
            async.allowSceneActivation = true;
        }
    }
}
