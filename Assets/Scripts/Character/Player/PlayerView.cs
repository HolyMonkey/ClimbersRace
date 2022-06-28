using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform _hatsPool;
    [SerializeField] private Transform _trailsPool;
    [SerializeField] private Transform _skinsPool;
    [SerializeField] private Transform _shortsPool;

    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Shop _shop;

    private Transform _currentSkinInActive;
    private Transform _currentShortInActive;
    private Transform _currentTrailActive;
    private Transform _currentHatActive;

    private Transform[] _hats;
    private Transform[] _trails;
    private Transform[] _skins;
    private Transform[] _shorts;

    private const string SAVED_SKIN = "SkinSaveID";
    private const string SAVED_SHORT = "ShortSaveID";
    private const string SAVED_HAT = "HatSaveID";
    private const string SAVED_TRAIL = "TrailSaveID";
    private const int _skinsListNumber = 0;
    private const int _shortsListNumber = 1;

    public int CurrentHat
    {
        get { return PlayerPrefs.GetInt(SAVED_HAT, 0); }
        private set { PlayerPrefs.SetInt(SAVED_HAT, value); }
    }
    public int CurrentSkin
    {
        get { return PlayerPrefs.GetInt(SAVED_SKIN, 0); }
        private set { PlayerPrefs.SetInt(SAVED_SKIN, value); }
    }
    public int CurrentShort
    {
        get { return PlayerPrefs.GetInt(SAVED_SHORT, 0); }
        private set { PlayerPrefs.SetInt(SAVED_SHORT, value); }
    }
    public int CurrentTrail
    {
        get { return PlayerPrefs.GetInt(SAVED_TRAIL, 0); }
        private set { PlayerPrefs.SetInt(SAVED_TRAIL, value); }
    }

    private void Awake()
    {
        _hats = new Transform[_hatsPool.childCount];
        _trails = new Transform[_trailsPool.childCount];
        _skins = new Transform[_skinsPool.childCount];
        _shorts = new Transform[_shortsPool.childCount];

        for (int i = 0; i < _hatsPool.childCount; i++)
        {
            _hats[i] = _hatsPool.GetChild(i);
            _trails[i] = _trailsPool.GetChild(i);
            _skins[i] = _skinsPool.GetChild(i);
            _shorts[i] = _shortsPool.GetChild(i);

            if (_hats[i].gameObject.activeInHierarchy == true)
                _currentHatActive = _hats[i];

            if (_trails[i].gameObject.activeInHierarchy == true)
                _currentTrailActive = _trails[i];

            if (_skins[i].gameObject.activeInHierarchy == true)
                _currentSkinInActive = _skins[i];

            if (_shorts[i].gameObject.activeInHierarchy == true)
                _currentShortInActive = _shorts[i];
        }
    }

    private void Start()
    {
        ChangeHat(CurrentHat);
        ChangeSkin(CurrentSkin);
        ChangeShorts(CurrentShort);
        ChangeTrail(CurrentTrail);
    }

    private void OnEnable()
    {
        if (_shop != null)
        {
            _shop.SoldHat += ChangeHat;
            _shop.SoldSkin += ChangeSkin;
            _shop.SoldShort += ChangeShorts;
            _shop.SoldTrail += ChangeTrail;
        }
    }

    private void OnDisable()
    {
        if (_shop != null)
        {
            _shop.SoldHat -= ChangeHat;
            _shop.SoldSkin -= ChangeSkin;
            _shop.SoldShort -= ChangeShorts;
            _shop.SoldTrail -= ChangeTrail;
        }
    }

    private void ChangeSkin(int itemNumber)
    {
        CurrentSkin = itemNumber;
        _currentSkinInActive = ChangeItem(_currentSkinInActive, _skins, CurrentSkin);
        ChangeMaterial(_skinsListNumber, _currentSkinInActive);
    }

    private void ChangeShorts(int itemNumber)
    {
        CurrentShort = itemNumber;
        _currentShortInActive = ChangeItem(_currentShortInActive, _shorts, CurrentShort);
        ChangeMaterial(_shortsListNumber, _currentShortInActive);
    }

    private void ChangeHat(int itemNumber)
    {
        CurrentHat = itemNumber;
        _currentHatActive = ChangeItem(_currentHatActive, _hats, CurrentHat);
    }

    private void ChangeTrail(int itemNumber)
    {
        CurrentTrail = itemNumber;
        _currentTrailActive = ChangeItem(_currentTrailActive, _trails, CurrentTrail);
    }

    private Transform ChangeItem(Transform itemInActive, Transform[] itemsList, int currentItemSaved)
    {
        itemInActive.gameObject.SetActive(false);
        itemsList[currentItemSaved].gameObject.SetActive(true);
        return itemsList[currentItemSaved];
    }

    private void ChangeMaterial(int numberList, Transform targetMaterial)
    {
        var tempSkins = _skinnedMeshRenderer.materials;
        tempSkins[numberList] = targetMaterial.GetComponent<SkinnedMeshRenderer>().material;
        _skinnedMeshRenderer.materials = tempSkins;
    }
}