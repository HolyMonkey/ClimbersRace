using UnityEngine;

[System.Serializable]
class RangeFloat
{
    [SerializeField] private float _min;
    [SerializeField] private float _max;

    public float RandomValue => Random.Range(_min, _max);
    public float AverageValue => (_min + _max) / 2;
    public float Min => _min;
    public float Max => _max;

    public RangeFloat(float min, float max)
    {
        _min = min;
        _max = max;
    }
}
