using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance {  get; private set; }

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
        GameState = new GameStateData();
    }
    public void SetLayoutState(int rows, int cols)
    {
        GameState.layout = new Vector2(rows, cols);
    }
    public void OverrideState(GameStateData gameStateData)
    {
        GameState = gameStateData;
    }
}
