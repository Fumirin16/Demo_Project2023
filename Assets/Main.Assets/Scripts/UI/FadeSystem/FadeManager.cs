using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static FadeManager;


public class FadeVariables
{
    public static bool FadeOut;
    public static bool FadeIn;

    public static void Initi_Fade()
    {
        FadeOut = false;
        FadeIn = false;
    }
}

[System.Serializable]
public class FadeManager
{
    public  enum FadeType
    {
        Alpha,
        Color,
    }

    public float FadeOutSpeed;
    public float FadeInSpeed;

    // �t�F�[�h�A�E�g�̉��o������֐�
    public void FadeOut(Image _fadeImage, float _fadeColor, FadeType fadeType = FadeType.Alpha, float _defaultValue = 1)
    {
        // �t�F�[�h�A�E�g������p�l����\������
        _fadeImage.gameObject.SetActive(true);

        // �����x�����Z���ďグ��
        _fadeColor += FadeOutSpeed * Time.deltaTime;

        _fadeImage.color = SetColor(fadeType, _fadeImage, _fadeColor);

        // �p�l���̓����x���w�肵�������x�̒l�ɂȂ������̏���
        if (_fadeColor >= _defaultValue)
        {
            // �p�l���̓����x���Œ肷��
            _fadeColor = _defaultValue;

            // �t�F�[�h�A�E�g�̔����L���ɂ���
            FadeVariables.FadeOut = true;
        }
    }

    public void FadeIn(Image _fadeImage, float _fadeColor, FadeType fadeType = FadeType.Alpha, float _defaultValue = 0)
    {
        // �t�F�[�h�A�E�g������p�l����\������
        _fadeImage.enabled = true;

        // �����x�����Z���ĉ�����
        _fadeColor -= FadeInSpeed * Time.deltaTime;

        _fadeImage.color = SetColor(fadeType, _fadeImage, _fadeColor);

        // �p�l���̓����x���w�肵�������x�̒l�ɂȂ������̏���
        if (_fadeColor <= _defaultValue)
        {
            // �p�l���̓����x���Œ肷��
            _fadeColor = _defaultValue;

            // �t�F�[�h�A�E�g������p�l�����\������
            _fadeImage.gameObject.SetActive(false);

            // �t�F�[�h�C���̔����L���ɂ���
            FadeVariables.FadeIn = true;
        }
    }

    private Color SetColor(FadeType fadeType, Image _fadeImage, float _fadeColor)
    {
        switch (fadeType)
        {
            case FadeType.Alpha:
                // �t�F�[�h�A�E�g������p�l���̓����x��ݒ肷��
                return new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeColor);
            case FadeType.Color:
                // �t�F�[�h�A�E�g������p�l���̓����x��ݒ肷��
                return new Color(_fadeColor, _fadeColor, _fadeColor, _fadeImage.color.a);
            default:
                return Color.clear;
        }
    }
}
