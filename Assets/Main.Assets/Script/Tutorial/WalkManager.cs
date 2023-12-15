using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// �쐬�ҁF�n����
// �����݁i�����j�t�F�[�Y�̐���

public class WalkManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// �����݂����񐔂�\������e�L�X�g�ϐ�
    /// </summary>
    [SerializeField] TextMeshProUGUI _countText;

    /// <summary>
    /// �p�l���I�u�W�F�N�g�擾
    /// </summary>
    [SerializeField] GameObject _walkPanel;

    /// <summary>
    /// AudioManager�Q�Ƃ��邽�߂̕ϐ�
    /// </summary>
    [SerializeField] AudioManager _audioManager;

    /// <summary>
    /// StandStill�Q�Ƃ��邽�߂̕ϐ�
    /// </summary>
     [SerializeField] StandStill _standStill;

    /// <summary>
    /// TutorialManager�Q�Ƃ��邽�߂̕ϐ�
    /// </summary>
    [SerializeField] TutorialManager _tutorialManager;

    /// <summary>
    /// TutorialManager�Q�Ƃ��邽�߂̕ϐ�
    /// </summary>
    public int _clearCount = 3;

    /// <summary>
    /// ������I����������肷��bool
    /// </summary>
    bool isAudioEnd;

    /// <summary>
    /// SE����x�����Đ�������bool
    /// </summary>
    bool SEflag = true;

    #endregion ---Fields---

    #region ---Methods---

    void OnEnable()
    {
        // �{�C�X�Đ�
        _audioManager.PlaySESound(SEData.SE.WalkVoice);
    }

    void Update()
    {
        // �����݂����񐔂�Text�\��
        _countText.text = _standStill.WalkCount.ToString();

        if(_standStill.WalkCount > _clearCount)
        {
            _countText.text = "OK";
        }
        if (SEflag && _standStill.WalkCount > _clearCount)
        {
            _audioManager.PlaySESound(SEData.SE.Correct);
            SEflag = false;
            isAudioEnd = true;
        }
        if (_audioManager.CheckPlaySound(_audioManager.seAudioSource) && isAudioEnd || Input.GetKeyDown(KeyCode.Space))
        {
            _walkPanel.SetActive(false);
            _tutorialManager._phaseCount++;
        }
    }
    #endregion ---Methods---
}
