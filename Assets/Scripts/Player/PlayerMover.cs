using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Movement))]
public class PlayerMover : MonoBehaviour
{
    public Movement Movement { get; private set; }

    private void Start()
    {
        Movement = GetComponent<Movement>();
    }

    private void FixedUpdate()
    {
        Movement.ClampMexSpeed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBalk playerBalk))
        {
            Movement.SetFlyDirection(0.1f);
            Movement.AttachToHook(playerBalk);
            if (Movement.Rigidbody.velocity.magnitude > Movement.MaxAttachingSpeed)
            {
                StartCoroutine(Movement.SwingHook(0.5f));
            }
        }
    }
}
