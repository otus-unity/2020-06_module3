using Photon.Pun;
using UnityEngine;

public sealed class Gun : AbstractWeapon
{
    public Transform source;
    public float raycastDistance = 200.0f;
    public int damage = 10;

    public override void Shoot()
    {
        Ray ray = new Ray(source.position, source.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance)) {
            if (hit.collider && hit.collider.TryGetComponent<PhotonView>(out PhotonView view)) {
                view.RPC("DamageRPC", RpcTarget.All, damage);
            }
        }
    }
}
