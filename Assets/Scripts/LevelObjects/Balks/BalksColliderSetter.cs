using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class BalksColliderSetter : MonoBehaviour
{
    private Transform _balk;
    private SphereCollider[] _balksCollider;

    private void Start()
    {
        _balk = GetComponent<Transform>();
        _balksCollider = new SphereCollider[_balk.childCount];
        for (int i = 0; i < _balk.childCount; i++)
        {
            _balksCollider[i] = _balk.GetChild(i).GetComponentInChildren<SphereCollider>();
        }
    }

    public void EnableCollider()
    {
        for (int i = 0; i < _balksCollider.Length; i++)
        {
            var balk = _balksCollider[i].GetComponentInParent<Balk>();
            if (balk.IsAttachingPlayer)
                _balksCollider[i].enabled = true;
        }
    }

    public void DisableCollider()
    {
        for (int i = 0; i < _balksCollider.Length; i++)
        {
            var balk = _balksCollider[i].GetComponentInParent<Balk>();
            if (balk.IsAttachingPlayer)
                _balksCollider[i].enabled = false;
            
        }
    }
}
