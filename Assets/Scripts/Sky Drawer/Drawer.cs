using System.Collections.Generic;
using Celestial_Body;
using Data_Loader;
using UnityEngine;
// using ViewRect = System.Tuple<float, float, float, float>;

public class Drawer : MonoBehaviour
{
    [SerializeField] private float raLeft = -40;
    [SerializeField] private float raRight = 40;
    [SerializeField] private float decDown = -30;
    [SerializeField] private float decUp = 30;
    [SerializeField] private float ra = 90;
    [SerializeField] private float dec = 0;
    private List<Star> _stars;
    private Dictionary<string, GameObject> _spheres;

    void Start()
    {
        _stars = DataLoader.LoadV50();
        _spheres = new Dictionary<string, GameObject>();
        Debug.Log($"Loaded {_stars.Count} stars!");
    }

    void DrawStar(Star star)
    {
        var isOut = star.Ra < ra + raLeft || ra + raRight < star.Ra ||
                    star.Dec < dec + decDown || dec + decUp < star.Dec;
        if (isOut)
        {
            if (_spheres.ContainsKey(star.Name))
            {
                var obj = _spheres[star.Name];
                _spheres.Remove(star.Name);
                Destroy(obj);
            }
            return;
        }

        var pos = new Vector3(star.Ra - ra, star.Dec - dec) / 6f - Vector3.forward;

        if (_spheres.ContainsKey(star.Name))
        {
            var obj = _spheres[star.Name];
            obj.transform.position = pos;
        }
        else
        {
            // Debug.Log($"Drawing Star {star}");

            var scale = Mathf.Clamp((5 - star.Mag) / 2000f, 0.0001f, 0.1f);

            var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.name = star.Name;
            obj.transform.parent = gameObject.transform;
            obj.transform.localScale = Vector3.one * scale;
            obj.transform.position = pos;

            _spheres.Add(star.Name, obj);
        }
    }

    void Update()
    {
        foreach (var star in _stars)
        {
            DrawStar(star);
        }
    }
}