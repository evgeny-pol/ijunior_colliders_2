using UnityEngine;

public class MassObjectsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField, Min(0f)] private float _offset;
    [SerializeField, Min(1)] private int _countInDimension;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        float offsetFromCenter = (_countInDimension / 2f - 0.5f) * _offset;
        Vector3 spawnCorner = transform.position - new Vector3(offsetFromCenter, 0, offsetFromCenter);

        for (int floor = 0; floor < _countInDimension; ++floor)
        {
            for (int row = 0; row < _countInDimension; ++row)
            {
                for (int column = 0; column < _countInDimension; ++column)
                {
                    Vector3 position = spawnCorner + new Vector3(_offset * column, _offset * floor, _offset * row);
                    Instantiate(_objectToSpawn, position, Quaternion.identity);
                }
            }
        }
    }
}
