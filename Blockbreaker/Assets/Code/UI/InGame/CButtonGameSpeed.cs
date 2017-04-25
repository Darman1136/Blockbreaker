using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CButtonGameSpeed : MonoBehaviour {
    private CDefaultGamemode gamemode;

    void Start() {
        gamemode = CDefaultGamemode.GAMEMODE;
    }

    public void OnPress() {
        gamemode.GameInfo.IsInFastMode = true;
        Time.timeScale = 2f;
    }

    public void OnRelease() {
        gamemode.GameInfo.IsInFastMode = false;
        Time.timeScale = 1f;
    }
}
