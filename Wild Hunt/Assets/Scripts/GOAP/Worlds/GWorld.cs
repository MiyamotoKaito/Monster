/// <summary>
/// グローバルワールドステート管理クラス
/// </summary>
public class GWorld
{
    //シングルトンインスタンス
    private static readonly GWorld instanse = new GWorld();
    //グローバルワールドステート
    private static WorldStates _world;
    //静的コンストラクタ
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
    //シングルトンインスタンスの取得
    public static GWorld Instance
    {
        get { return instanse; }
    }
    /// <summary>
    /// グローバルワールドステートの取得
    /// </summary>
    /// <returns></returns>
    public WorldStates GetWorld()
    {
        return _world;
    }
}
