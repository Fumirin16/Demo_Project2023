using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 警備員のゲームオーバー判定
// 作成者：地引翼

public class GuardsmanCollision : MonoBehaviour
{
    /// <summary>
    ///  値を管理するアセットから値を参照する
    /// </summary>
    [SerializeField] ValueSettingManager _settingManager;

    /// <summary>
    ///  警備員オブジェクト取得
    /// </summary>
    [SerializeField] GameObject _guardsmanObj;

    /// <summary>
    ///  プレイヤーオブジェクト取得
    /// </summary>
    [SerializeField] GameObject _playerObj;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // ゲームオーバーの判定をtrueにする
            _settingManager.gameOver = true;

            // プレイヤーのほうに向かせる
            _guardsmanObj.transform.LookAt(_playerObj.transform.position);
            
            //Debug.Log("ゲームオーバー");
        }
    }
}