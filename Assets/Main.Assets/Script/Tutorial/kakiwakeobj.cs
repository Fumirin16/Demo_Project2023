using UnityEngine;

public class kakiwakeobj : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // �^�O�FEnemy
        if (other.gameObject.tag == "Enemy")
        {
            kakiwakeManager.hitolist.Add(other.gameObject);
        }
    }
}