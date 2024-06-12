using UnityEngine;

// 作成者：山﨑晶
// ゲームクリア後に周辺の観客を消す処理

public class ClearAreaManager : MonoBehaviour
{
    // 観客のタグ名を保存する変数
    private string _audienceTag = "Audience";

    // 特定の範囲に当たった場合の処理
    private void OnTriggerEnter(Collider other)
    {
        // 触れているオブジェクトがアクティブ　かつ　観客のタグがついている場合
        if (other.gameObject.activeSelf && other.gameObject.CompareTag(_audienceTag))
        {
            // 表示をオフにする
            other.gameObject.SetActive(false);
        }
    }
}
