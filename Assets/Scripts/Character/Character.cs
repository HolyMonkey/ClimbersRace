using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(SpringJoint))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _pushForce;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _springTougthness;
    [SerializeField] private float _swingReducerPower;
    [SerializeField] private Rigidbody _defaultRigidbody;
    [SerializeField] private Level _level;
    [SerializeField] private ParticleSystem _bonusFlameParticles;
    [SerializeField]
    private MonoBehaviour _moverBehaviour;
    private IMovable _mover => (IMovable)_moverBehaviour;

    private Collider _collider;
    private Rigidbody _rigidbody;
    private SpringJoint _springJoint;

    public Balk CurrentBalk { get; private set; }
    private Vector3 _startDragPosition;

    public event UnityAction<Balk> AttachingBalk;
    public event UnityAction DetachingBalk;
    public event UnityAction Falling;
    public event UnityAction<Character> Dying;
    public event UnityAction<Vector3> AttackedEnemy;
    public event UnityAction BeingAttack;

    public bool IsAttachingBalk => CurrentBalk;
    public Vector3 Velocity => _rigidbody.velocity;
    public bool IsBonusMove { get; private set; } = false;

    private void OnValidate()
    {
        if (_moverBehaviour is IMovable)
            return;

        Debug.LogError(_moverBehaviour.name + " needs to implement " + nameof(IMovable));
        _moverBehaviour = null;
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _springJoint = GetComponent<SpringJoint>();
    }

    private void OnEnable()
    {
        _level.BonusGameStarted += OnBonusStarted;
    }

    private void OnDisable()
    {
        _level.BonusGameStarted -= OnBonusStarted;
    }

    private void Update()
    {
        _mover.UpdateVelocity();
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
    }

    private void OnBonusStarted()
    {
        _collider.enabled = false;
    }

    public void Die()
    {
        _collider.enabled = false;

        if (IsAttachingBalk)
            DetachFromBalk();

        Dying?.Invoke(this);
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
    }

    public void BalkPush(Vector3 direction)
    {
        DetachFromBalk();
        _mover.Move(direction, _pushForce);
    }

    public void BonusPush(BonusWall targetWall, AnimationCurve yCurve)
    {
        DetachFromBalk();
        _rigidbody.isKinematic = true;
        IsBonusMove = true;
        StartCoroutine(BonusGameMoving(targetWall.TargetPoint, yCurve, targetWall.PlayerMoveDuration));
    }

    public void AttachToBalk(Balk balk)
    {
        CurrentBalk = balk;

        SetupJoint(_swingReducerPower, balk.JointRigidbody, balk.JointRigidbody.centerOfMass, _springTougthness);

        balk.AddForce(Velocity);

        AttachingBalk?.Invoke(balk);
    }

    public void DetachFromBalk()
    {
        if (IsAttachingBalk)
        {
            CurrentBalk.DetachCharacter();
            CurrentBalk = null;
        }

        SetupJoint(0, _defaultRigidbody, _springJoint.anchor, 0);

        DetachingBalk?.Invoke();
    }

    public void AttackEnemy(Character enemy, Vector3 contactPoint)
    {
        enemy.Attacked();
        AttackedEnemy?.Invoke(contactPoint);
    }

    private void Attacked()
    {
        Falling?.Invoke();
        DetachFromBalk();
        BeingAttack?.Invoke();
    }

    public void CollideWithTrap()
    {
        Falling?.Invoke();
        _rigidbody.velocity /= 2;
    }

    private void SetupJoint(float drag, Rigidbody connectedBody, Vector3 connectedAnchor, float spring)
    {
        _rigidbody.drag = drag;
        _springJoint.connectedBody = connectedBody;
        _springJoint.connectedAnchor = connectedAnchor;
        _springJoint.spring = spring;
    }

    private IEnumerator BonusGameMoving(Vector3 targetPoint, AnimationCurve yCurve, float duration)
    {
        float time = 0;
        Vector3 startPoint = transform.position;

        _bonusFlameParticles.Play();

        targetPoint.y += 0.55f; //character offset

        while (time < duration)
        {
            time += Time.deltaTime;
            float s = time / duration;

            Vector3 targetPosition = Vector3.Lerp(startPoint, targetPoint, s);
            targetPosition.y = startPoint.y + yCurve.Evaluate(s) * (targetPoint.y - startPoint.y);

            _rigidbody.MovePosition(targetPosition);

            yield return null;
        }

        _bonusFlameParticles.Stop();

        _level.WinGame();
    }
}
