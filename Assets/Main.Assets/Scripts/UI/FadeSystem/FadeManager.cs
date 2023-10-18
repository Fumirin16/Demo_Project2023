using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

public class FadeManager
{
    // �t�F�[�h�A�E�g�̉��o������֐�
    public void FadeOut(Image _fadeImage, float _fadeColor, float fadeSpeed=0.1f,bool fadeType = false, float _defaultValue = 1)
    {
        // �t�F�[�h�A�E�g������p�l����\������
        _fadeImage.gameObject.SetActive(true);

        // �����x�����Z���ďグ��
        _fadeColor += fadeSpeed * Time.deltaTime;

        switch (fadeType)
        {
            case false:
                // �t�F�[�h�A�E�g������p�l���̓����x��ݒ肷��
                _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeColor);
                break;
            case true:
                // �t�F�[�h�A�E�g������p�l���̓����x��ݒ肷��
                _fadeImage.color = new Color(_fadeColor, _fadeColor, _fadeColor, _fadeImage.color.a);
                break;
        }

        // �p�l���̓����x���w�肵�������x�̒l�ɂȂ������̏���
        if (_fadeColor >= _defaultValue)
        {
            // �p�l���̓����x���Œ肷��
            _fadeColor = _defaultValue;

            // �t�F�[�h�A�E�g�̔����L���ɂ���
            FadeVariables.FadeOut = true;
        }
    }

    public void FadeIn(Image _fadeImage, float _fadeColor, float fadeSpeed = 0.1f, bool fadeType = false, float _defaultValue = 0)
    {
        // �t�F�[�h�A�E�g������p�l����\������
        _fadeImage.enabled = true;

        // �����x�����Z���ĉ�����
        _fadeColor -= fadeSpeed * Time.deltaTime;

        switch (fadeType)
        {
            case false:
                // �t�F�[�h�A�E�g������p�l���̓����x��ݒ肷��
                _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeColor);
                break;
            case true:
                // �t�F�[�h�A�E�g������p�l���̓����x��ݒ肷��
                _fadeImage.color = new Color(_fadeColor, _fadeColor, _fadeColor, _fadeImage.color.a);
                break;
        }

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
}
