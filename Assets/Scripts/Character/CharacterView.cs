using UnityEngine;
using MoreMountains.Feedbacks;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private MMFeedbacks _attachBalkFeedbacks;
    [SerializeField] private MMFeedbacks _detachBalkFeedbacks;
    [SerializeField] private MMFeedbacks _dieFeedback;
    [SerializeField] private MMFeedbacks _fallingFeedback;
    [SerializeField] private MMFeedbacks _attackFeedbacks;
    [SerializeField] private ParticleSystem _enemyAttackFX;

    [Header("IK")]
    [SerializeField] private bool _ikActive;
    [SerializeField] [Range(0f, 1f)] private float _rightHandWeight;
    [SerializeField] [Range(0f, 1f)] private float _leftHandWeight;

    private Animator _animator;
    private Transform _cameraTransform;

    private Transform _targetForRightHand;
    private Transform _targetForLeftHand;

    private IWall _testWall;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _cameraTransform = Camera.main.transform;

        _testWall = FindObjectOfType<StraightWall>();
        if (_testWall == null)
            _testWall = FindObjectOfType<CylindricWall>();
    }

    private void OnEnable()
    {
        _character.AttachingBalk += OnAttachingBalk;
        _character.DetachingBalk += OnDetachingBalk;
        _character.Falling += OnFalling;
        _character.Dying += OnDying;
        _character.AttackedEnemy += OnEnemyAttacked;
    }

    private void OnDisable()
    {
        _character.AttachingBalk -= OnAttachingBalk;
        _character.DetachingBalk -= OnDetachingBalk;
        _character.Falling -= OnFalling;
        _character.Dying -= OnDying;
        _character.AttackedEnemy -= OnEnemyAttacked;
    }

    private void OnEnemyAttacked(Vector3 contactPoint)
    {
        _enemyAttackFX.transform.position = contactPoint + _character.Velocity.normalized / 1.5f;
        _enemyAttackFX.Play();
        _attackFeedbacks?.PlayFeedbacks();
    }

    private void Update()
    {
        if (_character.IsAttachingBalk)
        {
            LookAt(_character.CurrentBalk.LookAtPoint);

            UpdateIK(_character.CurrentBalk);
        }
        else if (!_character.IsBonusMove)
            LookRotation(_character.Velocity);
        else
            LookAt(_cameraTransform.position);
    }

    private void OnAttachingBalk(Balk balk)
    {
        _animator.SetBool(IKCharacterAnimatorController.Params.Flying, false);
        _animator.SetBool(IKCharacterAnimatorController.Params.Falling, false);
        _attachBalkFeedbacks?.PlayFeedbacks();
    }

    private void OnDetachingBalk()
    {
        _animator.SetBool(IKCharacterAnimatorController.Params.Flying, true);
        SetIKTarget(null, null);

        _detachBalkFeedbacks?.PlayFeedbacks();
    }

    private void OnFalling()
    {
        _animator.SetBool(IKCharacterAnimatorController.Params.Falling, true);
        _fallingFeedback?.PlayFeedbacks();
    }

    private void OnDying(Character character)
    {
        _animator.SetTrigger(IKCharacterAnimatorController.Params.Die);
        _dieFeedback?.PlayFeedbacks();
    }

    private void LookAt(Vector3 targetPoint)
    {
        targetPoint.y = _character.transform.position.y;
        transform.LookAt(targetPoint);
    }

    private void LookRotation(Vector3 rotateTowards)
    {
        if (rotateTowards.sqrMagnitude < 0.1f)
            return;

        float y =  Mathf.Clamp(rotateTowards.y, 0.4f, 10f) / 2f; 
        Vector3 projectY = -y * _testWall.GetNormalVector(transform.position);
        rotateTowards += projectY;

        rotateTowards.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(rotateTowards, Vector3.up);
        transform.rotation = lookRotation;
    }

    private void UpdateIK(Balk balk)
    {
        if (Vector3.Dot(_character.CurrentBalk.LookAtPoint, transform.right) > 0.2f)
            SetIKTarget(balk.NearPoint, balk.FarPoint);
        else
            SetIKTarget(balk.FarPoint, balk.NearPoint);
    }

    private void SetIKTarget(Transform targetForRightHand, Transform targetForLeftHand)
    {
        _targetForRightHand = targetForRightHand;
        _targetForLeftHand = targetForLeftHand;
    }

    private void OnAnimatorIK()
    {
        if (_animator && _ikActive)
        {
            if (_targetForRightHand != null)
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, _rightHandWeight);
                _animator.SetIKPosition(AvatarIKGoal.RightHand, _targetForRightHand.position);
            }

            if (_targetForLeftHand != null)
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, _leftHandWeight);
                _animator.SetIKPosition(AvatarIKGoal.LeftHand, _targetForLeftHand.position);
            }
        }
    }
}