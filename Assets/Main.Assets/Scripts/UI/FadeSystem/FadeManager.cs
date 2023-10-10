using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeVariables
{
    public static bool FadeOut;
    public static bool FadeIn;
}

public class FadeManager : MonoBehaviour
{
    // �t�F�[�h�A�E�g������X�s�[�h��ۑ�����ϐ�
    [SerializeField, Range(0f, 10f)] private float _fadeOutSpeed = 1f;
    [SerializeField, Range(0f, 10f)] private float _fadeInSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // ������
        // �t�F�[�h�A�E�g/�t�F�[�h�C���̔���𖳌��ɂ���
        FadeVariables.FadeOut = false;
        FadeVariables.FadeIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �t�F�[�h�A�E�g�̉��o������֐�
    public void FadeOut(Image _fadeImage,float _fadeAlpha)
    {
        // �t�F�[�h�A�E�g������p�l����\������
        _fadeImage.enabled = true;

        // �����x�����Z���ďグ��
        _fadeAlpha += _fadeOutSpeed * Time.deltaTime;

        // �t�F�[�h�A�E�g������p�l���̓����x��ݒ肷��
        _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeAlpha);

        // �p�l���̓����x���w�肵�������x�̒l�ɂȂ������̏���
        if (_fadeAlpha >= 1)
        {
            // �p�l���̓����x���Œ肷��
            _fadeAlpha = 1;

            // �t�F�[�h�A�E�g�̔����L���ɂ���
            FadeVariables.FadeOut = true;
        }
    }

    public void FadeIn(Image _fadeImage,float _fadeAlpha)
    {
        // �t�F�[�h�A�E�g������p�l����\������
        _fadeImage.enabled = true;

        // �����x�����Z���ďグ��
        _fadeAlpha -= _fadeInSpeed * Time.deltaTime;

        // �t�F�[�h�A�E�g������p�l���̓����x��ݒ肷��
        _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeAlpha);

        // �p�l���̓����x���w�肵�������x�̒l�ɂȂ������̏���
        if (_fadeAlpha <= 0)
        {
            // �p�l���̓����x���Œ肷��
            _fadeAlpha = 0;

            // �t�F�[�h�A�E�g������p�l�����\������
            _fadeImage.enabled = false;

            // �t�F�[�h�C���̔����L���ɂ���
            FadeVariables.FadeIn = true;
        }
    }
}
