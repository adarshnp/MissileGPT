using System.Collections;
using UnityEngine;

namespace missilegpt
{
    public class AOEDomeEffect : MonoBehaviour
    {
        private float scalefactor = 0.1f;
        public float radius { set { StartExplosion(value); } }
        public float lifetime;
        private void Start()
        {
            Destroy(gameObject, lifetime);
        }
        public void StartExplosion(float radius)
        {
            StartCoroutine(Expand(radius));
        }
        IEnumerator Expand(float radius)
        {
            while (transform.localScale.x < (2 * radius))
            {
                transform.localScale += Vector3.one * scalefactor;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}