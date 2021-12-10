using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartTutorial : MonoBehaviour
{
    [SerializeField] private BalkInput _balkInput;
    [SerializeField] private ParticleSystem _particle;

    private bool _isStarted = false;

    public event UnityAction PlayerMoveStarted;
    public bool IsStarted => _isStarted;

    private void OnEnable()
    {
        _balkInput.PlayerStartMoved += OnStarted;
    }

    private void OnDisable()
    {
        _balkInput.PlayerStartMoved -= OnStarted;
    }

    private void Start()
    {
        _particle.gameObject.SetActive(true);
    }

    private void OnStarted()
    {
        _particle.gameObject.SetActive(false);
        PlayerMoveStarted?.Invoke();
    }
}
