using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float ExplosionRadius = 5f;
    [SerializeField] private float Force = 1000f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            KnockBack();
            
        }
    }

    public void KnockBack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(Force, transform.position, ExplosionRadius);
            }
        }
    }
}
