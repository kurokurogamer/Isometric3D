﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
	void Start()
    {
        SceneCtl.instance.AddSceneAsync("Oka");
    }
}
