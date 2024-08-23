using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("Минимальное количество создаваемых объектов.")]
    [SerializeField, Min(0)] private int _newObjectsMin;
    [Tooltip("Максимальное количество создаваемых объектов.")]
    [SerializeField] private int _newObjectsMax;

    private void OnValidate()
    {
        _newObjectsMax = Mathf.Max(_newObjectsMin, _newObjectsMax);
    }

    public T[] Spawn<T>(T original, float offsetRadius = 0f) where T : Object
    {
        int newObjectsCount = Random.Range(_newObjectsMin, _newObjectsMax + 1);
        var newObjects = new T[newObjectsCount];

        for (int i = 0; i < newObjectsCount; ++i)
        {
            Vector3 position = transform.position;

            if (offsetRadius > 0)
                position += Random.onUnitSphere * offsetRadius;

            newObjects[i] = Instantiate(original, position, Quaternion.identity);
        }

        return newObjects;
    }
}
