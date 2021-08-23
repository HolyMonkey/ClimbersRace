using AnimatedUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] Level _level;

    private void Awake()
    {
        if (!_level && _level.CurrentLevel != 1)
            gameObject.SetActive(false);
    }
}
