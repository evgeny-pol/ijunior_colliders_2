using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Tooltip("Сила взрыва.")]
    [SerializeField, Min(0f)] private float _explosionForce;
    [Tooltip("Радиус взрыва.")]
    [SerializeField, Min(0f)] private float _explosionRadius;
    [Tooltip("Слои на которых находятся объекты подверженные воздействию взрыва.")]
    [SerializeField] private LayerMask _affectedLayers;
    [SerializeField] private float _upwardsModifier;
    [SerializeField] private ForceMode _forceMode;

    private static readonly Collider[] s_affectedColliders = new Collider[1000];

    public float Force => _explosionForce;

    public float Radius => _explosionRadius;

    public void ApplyExplosionPowerCoeff(float coeff)
    {
        _explosionForce *= coeff;
        _explosionRadius *= coeff;
    }

    public void Explode(IEnumerable<GameObject> objectsToExplode)
    {
        Debug.Log($"Explosion force: {_explosionForce}, radius: {_explosionRadius}");

        foreach (GameObject go in objectsToExplode)
            if (go.TryGetComponent(out Rigidbody rigidbody))
                Explode(rigidbody);
    }

    public void Explode()
    {
        Debug.Log($"Explosion force: {_explosionForce}, radius: {_explosionRadius}");
        int affectedCount = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, s_affectedColliders, _affectedLayers);

        for (int i = 0; i < affectedCount; ++i)
        {
            Rigidbody rigidbody = s_affectedColliders[i].attachedRigidbody;

            if (rigidbody != null)
                Explode(rigidbody);
        }
    }

    private void Explode(Rigidbody rigidbody)
    {
        rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _upwardsModifier, _forceMode);
    }
}
