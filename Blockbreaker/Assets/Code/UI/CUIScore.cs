﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CUIScore : MonoBehaviour {
    public Text text;

    public void UpdateScoreText(int score) {
        if (text != null) {
            text.text = score.ToString();
        }
    }
}
