using UnityEngine;

// 作成者：山﨑晶 
// ステージの最善にあるゲートと接触したときにゲームクリアにする処理

public class ClaerManager : MonoBehaviour
{
    // 値を参照するために取得する変数
    [SerializeField]
    private ValueSettingManager _setSystem;

    // タグの名前を格納する変数
    private string _playerTag = "Player";

    //  クリア判定と当たった場合
    private void OnTriggerEnter(Collider other)
    {
        // 当たったオブジェクトのタグがPlayerだった場合
        if (other.gameObject.CompareTag(_playerTag))
        {
            //  ゲームクリアの判定をする
            _setSystem.gameClear = true;
        }
    }
}