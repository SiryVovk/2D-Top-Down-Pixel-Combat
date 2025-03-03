using System.Collections;
using UnityEngine;

public class Knocback : MonoBehaviour
{
    public bool IsKnocBacking { get; private set; }

    private Rigidbody2D rb;

    private float knockDuration = 0.2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        IsKnocBacking = false;
    }

    public void GetKnocback(Transform damgePosition, float knocbackTrust)
    {
        Vector2 forceVector = (transform.position - damgePosition.position).normalized * knocbackTrust * rb.mass;
        rb.AddForce(forceVector, ForceMode2D.Impulse);
        IsKnocBacking = true;
        StartCoroutine(KnockingBackRoutine());
    }

    private IEnumerator KnockingBackRoutine()
    {
        yield return new WaitForSeconds(knockDuration);
        rb.linearVelocity = Vector2.zero;
        IsKnocBacking = false;
    }
}
