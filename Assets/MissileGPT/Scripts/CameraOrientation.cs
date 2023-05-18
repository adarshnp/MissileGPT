using UnityEngine;

namespace missilegpt
{
    public class CameraOrientation : MonoBehaviour
    {
        public Transform player;
        public Transform launcher;
        public float InterpolationFactor;
        Vector3 offset;
        public float minZoom;
        public float maxZoom;
        //consider a right triangle made by player/launcher , midpoint between them and camera
        Vector3 midPosition;
        float HalfDistanceOfObjects;//radius
        float cameraDistanceToMidPosition;//altitude
        private void Start()
        {
            midPosition = (player.position + launcher.position) / 2;
            HalfDistanceOfObjects = Vector3.Distance(player.position, launcher.position) / 2f;
            offset = transform.position - midPosition;
            cameraDistanceToMidPosition = Mathf.Abs(Vector3.Distance(transform.position, midPosition));
        }
        private void LateUpdate()
        {
            midPosition = (player.position + launcher.position) / 2;
            offset.Normalize();
            Vector3 newCameraPosition = midPosition + offset * GetNewCameraDistance();
            transform.position = Vector3.Lerp(transform.position, newCameraPosition, InterpolationFactor * Time.deltaTime);
        }
        float GetNewCameraDistance()
        {
            float newHalfDistanceOfObjects = Vector3.Distance(player.position, launcher.position) / 2f;
            float newCameraDistanceToMidPosition = Mathf.Abs((newHalfDistanceOfObjects / HalfDistanceOfObjects) * cameraDistanceToMidPosition);
            newCameraDistanceToMidPosition = Mathf.Clamp(newCameraDistanceToMidPosition, minZoom, maxZoom);
            return newCameraDistanceToMidPosition;
        }
    }
}