// ì¬ÒFnøipü\èj
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ö^xõõÌ®«

public class IncubationGuardsmanController : MonoBehaviour
{
    [SerializeField]
    private ValueSettingManager settingManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Q[I[o[½è»è
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Q[I[o[Ì»èðtrueÉ·é
            settingManager.gameOver = true;
            Debug.Log("Q[I[o[");
        }
    }
}
