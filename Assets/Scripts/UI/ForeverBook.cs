using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeverBook : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
