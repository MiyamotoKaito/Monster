using GOAP.WorldStates; // WorldStatesへのアクセスに必要
using UnityEngine;

public class SurvivalStats : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField]
    [Tooltip("<color=lime>お腹のすき具合</color>")]
    private float _hunger = 5f;
    [SerializeField]
    [Tooltip("<color=lime>喉の渇き具合</color>")]
    private float _thirst = 7f;

    [Header("必要なゴール")]
    [SerializeField]
    private SubGoalType _eatSubGoal;
    [SerializeField]
    private SubGoalType _drinkSubGoal;

    private GAgent _agent;

    [ReadOnly, SerializeField]
    private bool _isHungerGoalActive = false;
    [ReadOnly, SerializeField]
    private bool _isThirstGoalActive = false;

    // ゴールのインスタンス保持用
    private GSubGoal _eatGoal;
    private GSubGoal _drinkGoal;

    private void Start()
    {
        _agent = GetComponent<GAgent>();
    }

    private void Update()
    {
        // お腹が減る処理（0より大きい時だけ減らす、または減らしてから0で止める）
        if (_hunger > 0)
        {
            _hunger -= Time.deltaTime;
            if (_hunger < 0) _hunger = 0; // 0未満になったら0にする
        }

        // 喉が渇く処理
        if (_thirst > 0)
        {
            _thirst -= Time.deltaTime;
            if (_thirst < 0) _thirst = 0; // 0未満になったら0にする
        }
        // 空腹チェック（0になったら発動）
        if (_hunger <= 0 && !_isHungerGoalActive)
        {
            Debug.Log($"{_eatSubGoal}サブゴール追加");

            // ゴールを追加する前に、WorldStateを「未達成(0)」にリセットする
            WorldStates.Instance.ModifyState(_eatSubGoal.ToString(), 0);

            _eatGoal = new GSubGoal(_eatSubGoal.ToString(), 1, true, null, "Food");
            _agent.AddSubGoal(_eatGoal, 1);
            _isHungerGoalActive = true;
        }

        // 喉の渇きチェック
        if (_thirst <= 0 && !_isThirstGoalActive)
        {
            Debug.Log($"{_drinkSubGoal}サブゴール追加");

            // 【重要】WorldStateをリセット
            WorldStates.Instance.ModifyState(_drinkSubGoal.ToString(), 0);

            _drinkGoal = new GSubGoal(_drinkSubGoal.ToString(), 1, true, null, "Water");
            _agent.AddSubGoal(_drinkGoal, 1);
            _isThirstGoalActive = true;
        }
    }

    /// <summary>
    /// 食事アクション完了時に呼ばれる（Actionスクリプトから呼ぶ想定）
    /// </summary>
    public void OnAte()
    {
        _isHungerGoalActive = false;
        _hunger = 5f; // 値を回復
        Debug.Log("EAT: 食べました。満腹度回復");
    }

    /// <summary>
    /// 水分補給アクション完了時に呼ばれる
    /// </summary>
    public void OnDrank()
    {
        _isThirstGoalActive = false;
        _thirst = 7f; // 値を回復
        Debug.Log("Drink: 飲みました。水分補給完了");
    }
}