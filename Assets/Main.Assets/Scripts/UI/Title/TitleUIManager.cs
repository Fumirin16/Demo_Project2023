using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// �^�C�g����ʂ̉��o�������L�q�����X�N���v�g
// �쐬�ҁF�R����

public class TitleUIManager : MonoBehaviour
{
    // �t�F�[�h������摜���擾
    [SerializeField] private Image fadePanel;
    // �t�F�[�h������摜���I�u�W�F�N�g�Ƃ��Ď擾
    [SerializeField] private GameObject fadePanelObj;

    [Space(10)]

    // �^�C�g�����S���擾
    [SerializeField] private Image titleImage;

    [Space(10)]

    // �{�^�����I�u�W�F�N�g�Ƃ��Ď擾
    [SerializeField] private GameObject[] buttonObj = new GameObject[2];

    [Space(10)]

    // �t�F�[�h�A�E�g������X�s�[�h��ۑ�����ϐ�
    [SerializeField, Range(0f, 1f)] private float _fadeSpeed;

    [Space(10)]

    // �摜��\������Ԋu�̎��Ԃ�ۑ�����ϐ�
    [SerializeField] private float[] _waitTime = new float[2];

    // �t�F�[�h������摜�̓����x��ۑ�����ϐ�
    private float _fadeAlpha;

    // �^�C�g�����S�̓����x��ۑ�����ϐ�
    private float _titleAlpha;

    // �t�F�[�h�A�E�g�̔�����Ǘ�����ϐ�
    private bool _isFadeOut;

    // �t�F�[�h�C���̔�����Ǘ�����ϐ�
    private bool _isFadeIn;

    // Start is called before the first frame update
    void Start()
    {
        // ������
        // �t�F�[�h�A�E�g/�t�F�[�h�C���̔���𖳌��ɂ���
        _isFadeOut = false;
        _isFadeIn = false;

        // �{�^���̕\���𖳌��ɂ���
        for (int i = 0; i < buttonObj.Length; i++)
        {
            buttonObj[i].SetActive(false);
        }

        // �t�F�[�h����摜�̓����x��ۑ�����
        _fadeAlpha = fadePanel.color.a;
        // �^�C�g�����S�̓����x��ۑ�����
        _titleAlpha = titleImage.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        // �^�C�g����ʂ�UI�̉��o������R���[�`�����Ăяo��
        StartCoroutine("Fade_UI");
    }

    // �^�C�g����ʂ�UI�̉��o������R���[�`��
    private IEnumerator Fade_UI()
    {
        // ��Ԗڂɕ\�������鉉�o����
        if (!_isFadeOut && !_isFadeIn)
        {
            // �t�F�[�h�C��������֐����Ăяo��
            FadeIn(fadePanel);
        }

        // ��Ԗڂɕ\�������鉉�o����
        if (_isFadeIn && !_isFadeOut)
        {
            // �t�F�[�h����摜���\���ɂ���
            fadePanelObj.SetActive(true);

            // ������҂�
            yield return new WaitForSeconds(_waitTime[0]);

            // �t�F�[�h�A�E�g������֐����Ăяo��
            FadeOut(titleImage);
        }

        // �O�Ԗڂɕ\�������鉉�o����
        if (_isFadeIn && _isFadeOut)
        {
            // ������҂�
            yield return new WaitForSeconds(_waitTime[1]);

            // �{�^����\�������鏈��
            for (int i = 0; i < buttonObj.Length; i++)
            {
                buttonObj[i].SetActive(true);
            }
        }
    }

    // �t�F�[�h�A�E�g�̉��o������֐�
    private void FadeOut(Image _fadeImage)
    {
        // �t�F�[�h�A�E�g������p�l����\������
        _fadeImage.enabled = true;

        // �����x�����Z���ďグ��
        _titleAlpha += _fadeSpeed * Time.deltaTime;

        // �t�F�[�h�A�E�g������p�l���̓����x��ݒ肷��
        _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _titleAlpha);

        // �p�l���̓����x���w�肵�������x�̒l�ɂȂ������̏���
        if (_titleAlpha >= 1)
        {
            // �p�l���̓����x���Œ肷��
            _titleAlpha = 1;

            // �t�F�[�h�A�E�g�̔����L���ɂ���
            _isFadeOut = true;
        }
    }

    private void FadeIn(Image _fadeImage)
    {
        // �t�F�[�h�A�E�g������p�l����\������
        _fadeImage.enabled = true;

        // �����x�����Z���ďグ��
        _fadeAlpha -= _fadeSpeed * Time.deltaTime;

        // �t�F�[�h�A�E�g������p�l���̓����x��ݒ肷��
        _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeAlpha);

        // �p�l���̓����x���w�肵�������x�̒l�ɂȂ������̏���
        if (_fadeAlpha <= 0)
        {
            // �p�l���̓����x���Œ肷��
            _fadeAlpha = 0;

            // �t�F�[�h�C���̔����L���ɂ���
            _isFadeIn = true;
        }
    }
}
