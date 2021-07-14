using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovementHandler _movement;

    private Vector3 _basePosition;

    private void Awake()
    {
        _basePosition = transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerBalk playerBalk))
        {
            _movement.AttachToBalk(playerBalk);
        }
    }

    [ContextMenu(nameof(ResetPosition))]
    private void ResetPosition()
    {
        transform.position = _basePosition;
    }
}
