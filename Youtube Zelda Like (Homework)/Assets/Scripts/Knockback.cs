using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust = 2f;
    public float knocktime = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D otherRigidbody2D = collision.GetComponent<Rigidbody2D>();

            if (otherRigidbody2D != null)
            {
                StartCoroutine(KnockCo(otherRigidbody2D)); 
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D otherRigidbody2D)
    {
        otherRigidbody2D.GetComponent<Enemy>().currentState = EnemyState.stagger;
        Vector2 difference = otherRigidbody2D.transform.position - transform.position;
        difference = difference.normalized * thrust;
        otherRigidbody2D.AddForce(difference, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knocktime);
        otherRigidbody2D.velocity = Vector2.zero;
        otherRigidbody2D.GetComponent<Enemy>().currentState = EnemyState.idle;
    }
}
