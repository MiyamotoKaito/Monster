using UnityEngine;

/// <summary>
/// グローバルワールドステート管理クラス
/// </summary>
public class GWorld
{
    private static readonly GWorld instanse = new GWorld();
    private static WorldStates _world;

    static GWorld()
    {
        _world = new WorldStates();
    }
    /// <summary>
    /// 
    /// </summary>
    private GWorld()
    {
    }
    public static GWorld Instance
    {
        get { return instanse; }
    }
    public WorldStates GetWorld()
    {
        return _world;
    }
}
