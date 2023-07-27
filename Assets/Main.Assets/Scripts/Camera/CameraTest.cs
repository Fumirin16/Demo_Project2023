using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

//text�Ƌ�̃I�u�W�F�N�g���g����JoyCon�̓��́iVector2�j���m�F����
//�쐬�ҁF�~�X䝗D
public class CameraTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
    public void OnMove(InputValue inputValue)
    {
        var vec = inputValue.Get<Vector2>();
        Text.text = $"Move:({vec.x:f2}, {vec.y:f2})\n{Text.text}";
    }
}
