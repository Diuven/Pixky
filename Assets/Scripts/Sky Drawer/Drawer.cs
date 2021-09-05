using System.Collections.Generic;
using Celestial_Body;
using Data_Loader;
using UnityEngine;
// using ViewRect = System.Tuple<float, float, float, float>;

public class Drawer : MonoBehaviour
{
    [SerializeField] private float ra = 90;
    [SerializeField] private float dec = 0;
    [SerializeField] private GameObject _starPrefab;
    [SerializeField] private GameObject _background;
    private List<Star> _stars;

    void Start()
    {
        _stars = DataLoader.LoadV50();
        Debug.Log($"Loaded {_stars.Count} stars!");

        var parentTrans = !_background ? null : _background.transform;
        
        foreach (var star in _stars)
        {
            var obj = Instantiate(_starPrefab, parentTrans, true);
            obj.name = star.Name;
            var ctrl = obj.GetComponent<CelestialBodyController>();
            ctrl.SetStar(star);
        }
    }

    void Update()
    {
    }
}