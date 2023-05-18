using UnityEngine;

namespace missilegpt
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private CharacterController character;
        [SerializeField]
        private InputManager input;
        [SerializeField]
        public AgentDataScriptableBase playerData;

        private float horizontal = 0;
        private float vertical = 0;
        private float speed;
        private float rotationSpeed;
        private Vector3 initialPosition;
        private void OnEnable()
        {
            GameManager.onGameRestart += ResetPosition;
            input.OnMoveDirectionKeyPress += Move;
        }
        private void Start()
        {
            speed = playerData.movementSpeed;
            rotationSpeed = playerData.turnSpeed;
            initialPosition = transform.position;
        }
        private void Move()
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            if (vertical == 0) { vertical = 1; }
            float rotationAmount = Mathf.Sign(vertical) * horizontal * rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, rotationAmount, 0f);
            character.Move(transform.forward * vertical * speed * Time.deltaTime);
        }
        private void OnDisable()
        {
            input.OnMoveDirectionKeyPress -= Move;
            GameManager.onGameRestart -= ResetPosition;
        }
        private void ResetPosition()
        {
            transform.position = initialPosition;
        }
    }
}