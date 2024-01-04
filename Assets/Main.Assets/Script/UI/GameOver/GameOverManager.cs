using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

// 作成者：山﨑晶 
// ゲームオーバーのUI演出処理

public class GameOverManager : MonoBehaviour
{
    #region ---Fields---

    [SerializeField]
    private VideoPlayer _gameOverVideo;

    [SerializeField]
    private float _activTime = 3f;

    [SerializeField]
    private GameObject _oneMoreObj;

    [SerializeField]
    private GameObject _oneMoreSelect;

    private Image _onemoreImage;

    private RectTransform _onemoreScale;

    [SerializeField]
    private RectTransform _onemoreButton;

    [SerializeField]
    private GameObject _toBackObj;

    [SerializeField]
    private GameObject _toBackSelect;

    private Image _toBackImage;

    private RectTransform _toBackScale;

    [SerializeField]
    private RectTransform _toBackButton;

    [SerializeField]
    private AudioManager _audioiSystem;

    [SerializeField]
    private TranstionScenes _transSystem;

    private GameObject _buttonObj;

    private Vector3 _buttonScale = new Vector3(1, 1, 1);

    private float _changeScale = 1.1f;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        _onemoreImage=_oneMoreSelect.GetComponent<Image>();
        _onemoreScale = _oneMoreSelect.GetComponent<RectTransform>();

        _toBackImage=_toBackSelect.GetComponent<Image>();
        _toBackScale = _toBackSelect.GetComponent<RectTransform>();

        _gameOverVideo.Play();

        _onemoreImage.enabled = true;

        _toBackImage.enabled = false;

        _oneMoreObj.SetActive(false);

        _toBackObj.SetActive(false);

        _audioiSystem.PlayBGMSound(BGMData.BGM.OverEnd);

        // 初期に選択状態にするオブジェクトを設定する
        EventSystem.current.SetSelectedGameObject(_oneMoreObj);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _activTime)
        {
            if (!_oneMoreObj.activeSelf || !_toBackObj.activeSelf)
            {
                _oneMoreObj.SetActive(true);
                _toBackObj.SetActive(true);
            }

            // 現在、選択されているボタンの情報を保存する
            _buttonObj = EventSystem.current.currentSelectedGameObject;

            if (_buttonObj == _oneMoreObj)
            {
                _onemoreImage.enabled = true;

                _toBackImage.enabled = false;

                _onemoreButton.transform.localScale = new Vector3(_changeScale, _changeScale, _changeScale);
                _onemoreScale.transform.localScale=new Vector3(_changeScale, _changeScale, _changeScale);

                _toBackButton.transform.localScale = _buttonScale;
                _toBackScale.transform.localScale = _buttonScale;
            }
            if (_buttonObj == _toBackObj)
            {
                _onemoreImage.enabled = false;

                _toBackImage.enabled = true;

                _onemoreButton.transform.localScale = _buttonScale;
                _onemoreScale.transform.localScale = _buttonScale;

                _toBackButton.transform.localScale = new Vector3(_changeScale, _changeScale, _changeScale);
                _toBackScale.transform.localScale = new Vector3(_changeScale, _changeScale, _changeScale);
            }
        }
    }

    public void OnClickButton(int SceneNum)
    {
        _audioiSystem.StopSound(_audioiSystem.bgmAudioSource);

        _transSystem.Trans_Scene(SceneNum);
    }

    #endregion ---Methods---
}