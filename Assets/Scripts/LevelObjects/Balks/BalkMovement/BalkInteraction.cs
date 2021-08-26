using UnityEngine;

[RequireComponent(typeof(Balk), typeof(BalkMovement))]
public abstract class BalkInteraction : MonoBehaviour
{
    public Balk Balk { get; private set; }

    protected virtual void Awake()
    {
        Balk = GetComponent<Balk>();
    }
}
