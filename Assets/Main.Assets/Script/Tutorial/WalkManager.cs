using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class WalkManager : MonoBehaviour
{
    // �J�E���g�̃e�L�X�g
    public TextMeshProUGUI _Text;
    // ������I�������
    private bool isAudioEnd;

    bool SEflag = true;
    // audio�t����
    [SerializeField] AudioManager _audioManager;
    // �p�l�����\���ɂ���
    [SerializeField] GameObject _kakiwakePanel;

    void OnEnable()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
