using UnityEngine;

public class ElementDistanceFromWall : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private MonoBehaviour _wallBehavior;
    private IWall _wall => (IWall)_wallBehavior;

    private void OnValidate()
    {
        if (!(_wallBehavior is IWall) && _wallBehavior)
            Debug.LogError(name + " needs to implement " + nameof(IWall));
    }

    [ContextMenu("CorrectDistance")]
    private void CorrectDistance()
    {
        if (_wall == null)
            _wallBehavior = FindObjectOfType<CylindricWall>();
        if (_wall == null)
            _wallBehavior = FindObjectOfType<StraightWall>();

        Vector3 targetPosition = _wall.GetNormalVector(transform.position) * _distance;

        if (_wall is StraightWall)
            targetPosition.x = transform.position.x;

        targetPosition.y = transform.position.y;

        transform.position = targetPosition;
    }
}
