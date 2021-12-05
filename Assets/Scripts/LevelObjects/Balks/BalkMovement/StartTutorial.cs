using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTutorial : MonoBehaviour
{
    [SerializeField] private BalkInput _balkInput;
    [SerializeField] private ParticleSystem _particle;

    private const string FIRST_TIME_OPENING = "FirstTimeOpening";

    private void OnEnable()
    {
        _balkInput.PlayerStartMoved += OnStarted;
    }

    private void OnDisable()
    {
        _balkInput.PlayerStartMoved -= OnStarted;
    }

    private void Awake()
    {
        if (PlayerPrefs.GetInt(FIRST_TIME_OPENING, 1) == 1)
        {
            _particle.gameObject.SetActive(true);

            //PlayerPrefs.SetInt(FIRST_TIME_OPENING, 0);
        }
    }

    private void OnStarted()
    {
        _particle.gameObject.SetActive(false);
    }
}
