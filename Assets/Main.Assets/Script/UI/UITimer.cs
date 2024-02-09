using UnityEngine;
using UnityEngine.UI;

//�@�쐬�Ғn����
//�@���Ԑ���UI

public class UITimer : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// Slider�I�u�W�F�N�g�ϐ�
    /// </summary>
    [Tooltip("���Ԑ�����UI���A�^�b�`")]
    [SerializeField] Slider _timeSlider;

    // �X�N���v�g�Q�ƕϐ�
    [SerializeField]�@ValueSettingManager _settingManager;
    [SerializeField] OutGameManager _gameManager;

    /// <summary>
    /// ���Ԑ����ϐ�
    /// </summary>
    float _maxTime;

    #endregion ---Fields---

    #region ---Methods---

    void Start()
    {
        _maxTime = _settingManager.GameLimitTime;

        //�X���C�_�[�̍ő�l�̐ݒ�
        _timeSlider.maxValue = _maxTime;
    }

    void Update()
    {
        //�X���C�_�[�̌��ݒl�̐ݒ�
        _timeSlider.value += Time.deltaTime;

        if (_timeSlider.value >= _maxTime)
        {
            GameEndFunc();
        }
    }

    void GameEndFunc()
    {
        if (_settingManager.gameClear)
        {
            _gameManager.GameClear();
        }
        else
        {
            // �Q�[���I�[�o�[�̔����true�ɂ���
            _settingManager.gameOver = true;
        }
    }
    #endregion ---Methods---
}