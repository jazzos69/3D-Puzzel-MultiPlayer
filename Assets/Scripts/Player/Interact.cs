using UnityEngine;

public class Interact : MonoBehaviour

{
    [SerializeField] private float rayDistance = 3f;
    [SerializeField] private LayerMask interactLayer;

    public bool HasBattery;

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, interactLayer))
        {
            if (hit.collider.CompareTag("BatteryHole"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    TryInsertBattery(hit.collider);
                }
            }
        }
    }

    private void TryInsertBattery(Collider target)
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}

