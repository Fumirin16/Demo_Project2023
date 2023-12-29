using UnityEngine;

// 警備員のゲームオーバー判定
// 作成者：地引翼

public class GuardsmanCollision : MonoBehaviour
{
    /// <summary>
    ///  値を管理するアセットから値を参照する
    /// </summary>
    [SerializeField] ValueSettingManager _settingManager;

    // ゲームオーバー当たり判定
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // ゲームオーバーの判定をtrueにする
            //_settingManager.gameOver = true;

            //Debug.Log("ゲームオーバー");
        }
    }
}