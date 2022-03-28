using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _effects;
    [SerializeField] private Transform _content;

    private ItemView[] _items;

    private void Start()
    {
        _items = new ItemView[_content.childCount];

        for (int i = 0; i < _content.childCount; i++)
        {
            _items[i] = _content.GetChild(i).GetComponent<ItemView>();
        }
    }

    public void Play(int index)
    {
        for (int i = 0; i < _effects.Length; i++)
        {
            if (_effects[i].isPlaying)
                _effects[i].Stop();

            if (_items[i].IsSold)
                _effects[index].Play();
        }
    }
}
