using UnityEngine;

[System.Serializable]
public class GameStateData
{
    public bool[] cellsState;
    public int[] cellsType;
    public int userClicks;
    public int userMatches;
    public int combos;
    public int lastCorrectMatch;
    public Vector2 layout;
}
