using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance {  get; private set; }
    public bool SavedState {  get; private set; }
    string gameStatePath;
    public GameStateData GameState {  get; private set; }
    private void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Destroy(Instance.gameObject);
        }
    }
    public void Start()
    {
        gameStatePath = Path.Combine(Application.streamingAssetsPath, "GameState.json");
        if(!Directory.Exists(Application.streamingAssetsPath)) Directory.CreateDirectory(Application.streamingAssetsPath);
        SavedState = LoadState();
        if (!SavedState)
        {
            GameState = new GameStateData();
        }
    }
    public void SetLayoutState(int rows, int cols)
    {
        GameState.layout = new Vector2(rows, cols);
    }
    public void OverrideState(GameStateData gameStateData)
    {
        GameState = gameStateData;
    }
    public void SaveState()
    {
        string content = JsonUtility.ToJson(GameState);
        File.WriteAllText(gameStatePath, content);
    }
    public bool LoadState()
    {
        if(!File.Exists(gameStatePath)) return false;
        string content = File.ReadAllText(gameStatePath);
        GameState = JsonUtility.FromJson<GameStateData>(content);
        return true;
    }
    public void DeleteState()
    {
        if (!File.Exists(gameStatePath)) return;
        SavedState = false;
        File.Delete(gameStatePath);
    }
}
