using UnityEngine;
using Unity.Netcode;

public class Battery : NetworkBehaviour
{
    public NetworkVariable<bool> IsHeld = new NetworkVariable<bool>(false);

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Pickup(BatteryPickUp player)
    {
        IsHeld.Value = true;

        rb.isKinematic = true;
        rb.useGravity = false;

        FollowPlayerClientRpc(player.NetworkObjectId);
    }

    [ClientRpc]
    private void FollowPlayerClientRpc(ulong playerId)
    {
        if (!NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(playerId, out NetworkObject playerObj))
            return;

        Transform holdPoint = playerObj.GetComponent<BatteryPickUp>().transform
            .Find("HoldPoint");

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
