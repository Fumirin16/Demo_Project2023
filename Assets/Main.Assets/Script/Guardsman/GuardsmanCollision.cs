using UnityEngine;

// 警備員とじらじょちゃんの当たり判定
// 作成者：地引翼

public class GuardsmanCollision : MonoBehaviour
{
    /// <summary>
    ///  値を管理するアセットから値を参照する
    /// </summary>
    [Tooltip("ValueSettingManagerをアタッチ")]
    [SerializeField] ValueSettingManager _settingManager;

    /// <summary>
    ///  プレイヤーオブジェクト取得
    /// </summary>
    [Tooltip("playerオブジェクトをアタッチ")]
    [SerializeField] GameObject _playerObj;

    void OnTriggerEnter(Collider other)
    {
        // タグ：Player
        if (other.gameObject.CompareTag("Player"))
        {
            // ゲームオーバーの判定をtrueにする
            _settingManager.gameOver = true;

            // プレイヤーのほうに向かせる
            transform.parent.gameObject.transform.LookAt(_playerObj.transform.position);
        }
    }
}