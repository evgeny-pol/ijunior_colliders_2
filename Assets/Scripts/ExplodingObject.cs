using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Spawner))]
[RequireComponent(typeof(Explosion))]
public class ExplodingObject : MonoBehaviour
{
    [Tooltip("Вероятность создания объектов при взрыве.")]
    [SerializeField, Range(0f, 1f)] private float _newObjectsSpawnProbability = 1f;
    [Tooltip("Коэффициент изменения размера.")]
    [SerializeField, Min(0.1f)] private float _newObjectScaleCoeff = 0.5f;
    [Tooltip("Коэффициент изменения вероятности создания объектов при взрыве.")]
    [SerializeField, Min(0f)] private float _spawnProbabilityCoeff = 0.5f;
    [Tooltip("Коэффициент изменения силы и радиуса взрыва.")]
    [SerializeField, Min(0f)] private float _explosionPowerCoeff = 1.5f;

    private Spawner _spawner;
    private Explosion _explosion;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _explosion = GetComponent<Explosion>();
    }

    private void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    public void Explode()
    {
        IEnumerable<GameObject> newObjects = TrySpawnObjects();

        if (newObjects == null)
            _explosion.Explode();
        else
            _explosion.Explode(newObjects);

        Destroy(gameObject);
    }

    private IEnumerable<GameObject> TrySpawnObjects()
    {
        float randomValue = Random.value;
        Debug.Log($"Spawn probability: {_newObjectsSpawnProbability}, random value: {randomValue}");

        if (randomValue > _newObjectsSpawnProbability)
            return null;

        ExplodingObject[] newObjects = _spawner.Spawn(this, transform.localScale.x * 0.5f);

        foreach (ExplodingObject newObject in newObjects)
            newObject.ApplySpawnedObjectCoeffs();

        return newObjects.Select(obj => obj.gameObject);
    }

    private void ApplySpawnedObjectCoeffs()
    {
        transform.localScale *= _newObjectScaleCoeff;
        _newObjectsSpawnProbability *= _spawnProbabilityCoeff;
        _explosion.ApplyExplosionPowerCoeff(_explosionPowerCoeff);
    }
}
