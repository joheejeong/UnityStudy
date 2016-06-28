using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    // 적이 접근하는 대상
    // 적들이 스스로 플레이어를찾아야 하므로
    Transform player;

    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <NavMeshAgent> ();
    }


    void Update ()
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            // 이동하고자 하는 목표 좌표를 넘겨주어 이동할 수 있도록 해주는 함수
            nav.SetDestination (player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
