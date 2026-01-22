using UnityEngine;
using Unity.Netcode;

public class BatteryPickUp : NetworkBehaviour
{
    [SerializeField] private float rayDistance = 3f;
    [SerializeField] private LayerMask pickupLayer;

    [SerializeField] private Transform holdPoint;

    public NetworkVariable<bool> HasBattery = new NetworkVariable<bool>(false);

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.F))//hier new input system ding aan maken OnInteract() ofzo
        {
            TryPickup();
        }
    }

    private void TryPickup()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, pickupLayer))
        {
            Battery battery = hit.collider.GetComponent<Battery>();
            if (battery != null)
            {
                RequestPickupServerRpc(battery.NetworkObject);
            }
        }
    }

    [ServerRpc]
    private void RequestPickupServerRpc(NetworkObjectReference batteryRef)
    {
        if (!batteryRef.TryGet(out NetworkObject batteryNetObj))
            return;

        Battery battery = batteryNetObj.GetComponent<Battery>();

        if (battery.IsHeld.Value)
            return;

        battery.Pickup(this);
        HasBattery.Value = true;
    }
}
