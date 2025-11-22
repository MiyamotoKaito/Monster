using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ワールドステートクラス
/// </summary>
[System.Serializable]
public class WorldState
{
    public string Key;
    public int Value;
}
/// <summary>
/// ワールドステート管理クラス
/// </summary>
public class WorldStates
{
    /// ステートの辞書を返すプロパティ
    public Dictionary<string, int> _States => _states;

    [SerializeField]
    private Dictionary<string, int> _states;
    //変数初期化用のコンストラクタ
    public WorldStates()
    {
        _states = new Dictionary<string, int>();
    }
    /// <summary>
    /// ステートを持っているか判別する
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool HasState(string key)
    {
        return _states.ContainsKey(key);
    }
    /// <summary>
    /// 引数に渡された値を辞書に登録する
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    private void AddState(string key, int value)
    {
        _states.Add(key, value);
    }
    /// <summary>
    /// ステートの値を変更する
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    private void ModifyState(string key, int value)
    {
        //優先度の値を変更する
        if (_states.ContainsKey(key))
        {
            _states[key] += value;
            //優先度が0以下になったら辞書から削除する
            if (_states[key] <= 0)
            {
                RemoveState(key);
            }
        }
        //渡されたステートが登録されていなかったら登録する
        else
        {
            _states.Add(key, value);
        }
    }
    /// <summary>
    /// ステートを削除する
    /// </summary>
    /// <param name="key"></param>
    public void RemoveState(string key)
    {
        if (_states.ContainsKey(key))
        {
            _states.Remove(key);
        }
    }
    /// <summary>
    /// 値を設定する
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetState(string key, int value)
    {
        if (_states.ContainsKey(key))
        {
            _states[key] = value;
        }
        else
        {
            _states.Add(key, value);
        }
    }
    /// <summary>
    /// 辞書を返す
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, int> GetStates()
    {
        return _states;
    }
}
