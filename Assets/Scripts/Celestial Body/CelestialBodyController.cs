using UnityEngine;

namespace Celestial_Body
{
    public class CelestialBodyController : MonoBehaviour
    {
        private Star _star;
        private SpriteRenderer _renderer;

        // Start is called before the first frame update
        void Start()
        {
            transform.localScale = Vector3.one * GetScale() / 100f;
        }

        public void SetStar(Star star)
        {
            _star = star;
        }

        // Update is called once per frame
        void Update()
        {
            if (_star == null)
            {
                gameObject.SetActive(false);
                return;
            }

            gameObject.SetActive(true);
            transform.position = new Vector3(-10, 0, -1) + (Vector3) GetProjection(new EquatorialVector(0, 0), 50);
        }

        private float GetScale()
        {
            return Mathf.Clamp((5 - _star.Mag) / 5f, 0.1f, 1f);
        }

        private Vector2 GetProjection(EquatorialVector viewVector, float fov)
        {
            // Observer is watching circular range
            return new Vector2(_star.Coord.Ra - viewVector.Ra, _star.Coord.Dec - viewVector.Dec) / 5f;
        }
    }
}