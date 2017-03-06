﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIRound : MonoBehaviour {
    public Text text;

    public void UpdateRoundText(int round) {
        if (text != null) {
            text.text = round.ToString();
        }
    }
}
