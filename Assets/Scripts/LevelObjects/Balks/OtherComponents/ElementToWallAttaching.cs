using UnityEngine;

public class ElementToWallAttaching : MonoBehaviour
{
    [SerializeField] private float _angleOffset = 0f;
    [SerializeField] private float _distance = 3f;
    [SerializeField] private MonoBehaviour _wallBehavior;
    private IWall _wall => (IWall)_wallBehavior;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!(_wallBehavior is IWall) && _wallBehavior)
            Debug.LogError(name + " needs to implement " + nameof(IWall));
    }

    [ContextMenu("CorrectRotation")]
    private void CorrectRotation()
    {
        if (_wall == null)
            _wallBehavior = FindObjectOfType<CylindricWall>();
        if (_wall == null)
            _wallBehavior = FindObjectOfType<StraightWall>();

        Vector3 direction = -_wall.GetNormalVector(transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = lookRotation;
        transform.Rotate(0, _angleOffset, 0);

        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
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

        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
    }
#endif
}