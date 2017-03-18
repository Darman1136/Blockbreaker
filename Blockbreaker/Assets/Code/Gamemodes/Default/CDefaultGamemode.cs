using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CDefaultGamemode : MonoBehaviour {
    public static CDefaultGamemode GAMEMODE;
    private String SAVE_FILE_LOCATION;

    private Canvas canvasGameOver;
    private CUIScore textScore;
    private CUIRound textRound;
    private CBlockSpawner bs;
    private CGameInfo gi;
    public CGameInfo GameInfo {
        get {
            return gi;
        }
    }
    private CPlayerInfo pi;
    public CPlayerInfo PlayerInfo {
        get {
            return pi;
        }
    }

    private bool loadedSavegame;

    void Awake() {
        SAVE_FILE_LOCATION = Application.persistentDataPath + "/savegame.bin";

        if (GAMEMODE == null) {
            GAMEMODE = this;
        } else if (GAMEMODE != this) {
            Destroy(gameObject);
        }

        InitializeComponents();
        loadedSavegame = Load();
    }

    void Start() {
        Initialize();
    }

    private void InitializeComponents() {
        canvasGameOver = GameObject.Find("CanvasGameOver").GetComponent<Canvas>();
        textScore = GameObject.Find("TextScoreValue").GetComponent<CUIScore>();
        textRound = GameObject.Find("TextRoundValue").GetComponent<CUIRound>();
        bs = GetComponent<CBlockSpawner>();
    }

    private void Initialize() {
        if (gi == null) {
            gi = new CGameInfo();
        }
        if (pi == null) {
            pi = new CPlayerInfo();
        }

        if (!loadedSavegame) {
            gi.RoundOver = true;
            gi.Field = new CSpawnableObject[gi.FieldHeight][];
        }
        textScore.UpdateScoreText(pi.Points);
        textRound.UpdateRoundText(gi.Round);
    }

    void Update() {
        if (IsRoundOver()) {
            RemoveAllUsedPowerUps();
            UpdateForNextRound();
            if (IsGameOver()) {
                GameOver();
                return;
            }

        }
    }

    void OnDestroy() {
        // currently only support saving when no round's active
        if (IsRoundOver()) {
            Save();
        }
    }

    private void UpdateForNextRound() {
        MoveArraysInArray();
        MoveObjectsOnScreen();

        gi.Round = ++gi.Round;
        CSpawnableObject[] newObjects = bs.SpawnNextRound(gi.Round);
        gi.Field[0] = newObjects;
        textRound.UpdateRoundText(gi.Round);

        if (!HasAdvancedAimLineThisRound()) {
            gi.AdvancedAimLineInRound = 0;
        }
        gi.BallKilledByBorderThisRound = false;
        gi.RoundOver = false;
        gi.RoundInProgress = false;
    }

    private void MoveObjectsOnScreen() {
        foreach (CSpawnableObject[] array in gi.Field) {
            bs.MoveGameObjects(array);
        }
    }

    private void MoveArraysInArray() {
        for (int index = gi.FieldHeight - 2; index >= 0; index--) {
            if (!IsArrayEmpty(gi.Field[index])) {
                gi.Field[index + 1] = gi.Field[index];
            } else {
                gi.Field[index + 1] = null;
            }
            gi.Field[index] = null;
        }
    }

    private bool IsArrayEmpty(CSpawnableObject[] array) {
        if (array != null) {
            foreach (CSpawnableObject go in array) {
                return false;
            }
        }
        return true;
    }

    private bool IsGameOver() {
        if (gi.Field[gi.Field.Length - 1] != null) {
            foreach (CSpawnableObject go in gi.Field[gi.Field.Length - 1]) {
                if (go != null) {
                    if (go.tag.Equals("Box")) {
                        gi.GameOver = true;
                    } else if (go.tag.Equals("PowerUp")) {
                        go.GetComponent<CPowerUp>().DestoryAtEndOfRound = true;
                    }
                }
            }
        }
        return gi.GameOver;
    }

    private void GameOver() {
        canvasGameOver.enabled = true;
    }

    private bool IsRoundOver() {
        return gi.RoundOver;
    }

    public void CheckRoundOver(CBall ball, bool killedByBorder) {
        if (killedByBorder && IsFirstBallToBeKilledByBorder()) {
            GameInfo.SpawnPoint = new Vector2(ball.transform.position.x, CGameInfo.SPAWN_POINT_Y);
        }
        if (GameObject.FindGameObjectsWithTag("PlayerBall").Length - 1 == 0) {
            gi.RoundOver = true;
        }
    }

    private bool IsFirstBallToBeKilledByBorder() {
        if (GameInfo.BallKilledByBorderThisRound) {
            return false;
        }
        GameInfo.BallKilledByBorderThisRound = true;
        return true;
    }

    private void RemoveAllUsedPowerUps() {
        foreach (CSpawnableObject[] gos in gi.Field) {
            if (gos != null) {
                for (int index = 0; index < gos.Length; index++) {
                    CSpawnableObject go = gos[index];
                    if (go != null) {
                        if (go.tag.Equals("PowerUp")) {
                            CPowerUp up = go.GetComponent<CPowerUp>();
                            if (up.DestoryAtEndOfRound) {
                                gos[index] = null;
                                up.KillPowerUp();
                            }
                        }
                    }
                }
            }
        }
    }

    public void addPoints(int points) {
        pi.Points = pi.Points + points;
        textScore.UpdateScoreText(pi.Points);
    }

    /**
    * Improved aiming support for next round.
    */
    public void SetAdvancedAimLine() {
        gi.AdvancedAimLineInRound = gi.Round + 1;
    }

    public bool HasAdvancedAimLineThisRound() {
        return gi.Round == gi.AdvancedAimLineInRound;
    }

    private void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(SAVE_FILE_LOCATION);
        bf.Serialize(file, pi);
        bf.Serialize(file, gi);

        foreach (CSpawnableObject[] soArray in gi.Field) {
            CSerializableSpawnableObject[] serializableFieldLine = new CSerializableSpawnableObject[gi.FieldWidth];
            if (soArray != null) {
                for (int index = 0; index < soArray.Length; index++) {
                    CSpawnableObject so = soArray[index];
                    if (so != null) {
                        serializableFieldLine[index] = so.GetSerializableObject();
                        Destroy(so.gameObject);
                    } else {
                        serializableFieldLine[index] = null;
                    }
                }
            }
            bf.Serialize(file, serializableFieldLine);
        }
        file.Close();
    }

    private bool Load() {
        if (File.Exists(SAVE_FILE_LOCATION)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(SAVE_FILE_LOCATION, FileMode.Open);
            pi = (CPlayerInfo)bf.Deserialize(file);
            gi = (CGameInfo)bf.Deserialize(file);
            gi.Field = new CSpawnableObject[gi.FieldHeight][];

            BuildFieldFromSave(bf, file);

            file.Close();
            return true;
        }
        return false;
    }

    private void BuildFieldFromSave(BinaryFormatter bf, FileStream file) {
        CSerializableSpawnableObject[] serializableFieldLine;
        for (int index = 0; index < gi.FieldHeight; index++) {
            serializableFieldLine = (CSerializableSpawnableObject[])bf.Deserialize(file);
            CSpawnableObject[] respawnedObjects = new CSpawnableObject[serializableFieldLine.Length];

            for (int arrayPosition = 0; arrayPosition < serializableFieldLine.Length; arrayPosition++) {
                CSerializableSpawnableObject sso = serializableFieldLine[arrayPosition];
                CSpawnableObject respawnedObject = null;
                if (sso != null && sso.data.ContainsKey("type")) {
                    switch ((CSpawnableObject.Type)sso.data["type"]) {
                        case CSpawnableObject.Type.CBlock:
                            respawnedObject = bs.SpawnBlockOnLoad(arrayPosition);
                            ((CBlock)respawnedObject).Health = (int)sso.data["health"];
                            break;
                        case CSpawnableObject.Type.CPUAdvancedAimLine:
                            respawnedObject = bs.SpawnCPUAdvancedAimLineOnLoad(arrayPosition);
                            ((CPowerUp)respawnedObject).DestoryAtEndOfRound = (bool)sso.data["destroyAtEndOfRound"];
                            break;
                        case CSpawnableObject.Type.CPUBounce:
                            respawnedObject = bs.SpawnCPUBounceOnLoad(arrayPosition);
                            ((CPowerUp)respawnedObject).DestoryAtEndOfRound = (bool)sso.data["destroyAtEndOfRound"];
                            break;
                        case CSpawnableObject.Type.CPUExtraBall:
                            respawnedObject = bs.SpawnCPUExtraBallnOnLoad(arrayPosition);
                            ((CPowerUp)respawnedObject).DestoryAtEndOfRound = (bool)sso.data["destroyAtEndOfRound"];
                            break;
                        case CSpawnableObject.Type.CPUKillBall:
                            respawnedObject = bs.SpawnCPUKillBallOnLoad(arrayPosition);
                            ((CPowerUp)respawnedObject).DestoryAtEndOfRound = (bool)sso.data["destroyAtEndOfRound"];
                            break;
                        case CSpawnableObject.Type.CPUNewBall:
                            respawnedObject = bs.SpawnCPUNewBallOnLoad(arrayPosition);
                            ((CPowerUp)respawnedObject).DestoryAtEndOfRound = (bool)sso.data["destroyAtEndOfRound"];
                            break;
                    }
                }
                respawnedObjects[arrayPosition] = respawnedObject;
            }
            for (int count = 0; count < index; count++) {
                bs.MoveGameObjects(respawnedObjects);
            }
            gi.Field[index] = respawnedObjects;
        }
    }
}
