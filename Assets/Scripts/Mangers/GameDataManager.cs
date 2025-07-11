using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public GameDataManager Instance {  get; private set; }

    GameStateData gameStateData;
    private void Awake()
    {
        if(Instance==null) Instance = this;
        else
        {
            Destroy(Instance.gameObject);
        }
    }
    public void Start()
    {
        gameStateData = new GameStateData();
    }
    public void SetLayoutState(int rows, int cols)
    {
        gameStateData.layout = new Vector2(rows, cols);
    }
    public void OverrideState(GameStateData gameStateData)
    {
    this.gameStateData = gameStateData;
    }
}
