using UnityEngine;
namespace missilegpt
{
    public class MissileLauncherMovement : MonoBehaviour
    {
        public PlayerDetection playerDetector;
        public AgentDataScriptableBase turretData;
        [SerializeField]
        private Transform player;
        [SerializeField]
        private float mindistanceFromPlayer = 2f;

        private float speed;
        private float rotationSpeed;
        private Vector3 initialPosition;
        private Vector3 currentPosition;
        private bool canMove = false;
        private void Start()
        {
            currentPosition = transform.position;
            speed = turretData.movementSpeed;
            rotationSpeed = turretData.turnSpeed;
        }
        private void OnEnable()
        {
            GameManager.onGameRestart += ResetPosition;
            playerDetector.OnPlayerDetected += StartMovement;
            playerDetector.OnPlayerTrackingLost += StopMovement;
            initialPosition = transform.position;
        }
        private void StartMovement()
        {
            canMove = true;
        }
        private void StopMovement()
        {
            canMove = false;
        }
        private void Update()
        {
            if (canMove)
            {
                Move(player.position);
                LookAtPlayer(player);
            }
        }
        private void Move(Vector3 playerPosition)
        {
            Vector3 targetPosition = CalculateTargetPosition(playerPosition);

            currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
            currentPosition.y = initialPosition.y;
            transform.position = currentPosition;

        }
        private Vector3 CalculateTargetPosition(Vector3 playerPosition)
        {
            Vector3 direction = playerPosition - transform.position;
            float distance = direction.magnitude;
            direction.Normalize();
            Vector3 targetPosition = (direction * (distance - mindistanceFromPlayer)) + transform.position;
            return targetPosition;
        }
        private void LookAtPlayer(Transform player)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0;
            direction.Normalize();
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        private void ResetPosition()
        {
            currentPosition = initialPosition;
            transform.position = initialPosition;
        }
        private void OnDisable()
        {
            GameManager.onGameRestart -= ResetPosition;
            playerDetector.OnPlayerDetected -= StartMovement;
            playerDetector.OnPlayerTrackingLost -= StopMovement;
        }
    }
}