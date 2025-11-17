using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldState
{
    public string Key;
    public int Value;
}

public class WorldStates
{
    public Dictionary<string, int> _states;
    //変数初期化用のコンストラクタ
    public WorldStates()
    {
        _states = new Dictionary<string, int>();
    }

    public bool HasState(string key)
    {
        return _states.ContainsKey(key);
    }

}
