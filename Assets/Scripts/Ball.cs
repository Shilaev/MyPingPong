using UnityEngine;

namespace PingPongEngine
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 1;
        private Vector2 _contactPoint;
        private Vector3 _movementDirection = Vector3.right;
        public float BallWidth { get; private set; }

        private void Start()
        {
            BallWidth = transform.localScale.x;
        }

        private void Update()
        {
            MoveBall();
        }

        public void MoveBall()
        {
            transform.Translate(_movementDirection * _movementSpeed * Time.deltaTime);
        }

        public void ResetPosition()
        {
            transform.position = Vector3.zero;
        }

        public void StopBall()
        {
            _movementSpeed = 0;
        }

        // Need to refactor this
        public void OnPaddleCollision(Transform other)
        {
            var direction = transform.position - other.position;
 
            var newX = Bounce45Angle(direction.x);
            var newY = Bounce45Angle(direction.y);
 
            _movementDirection = new Vector2(newX,newY);
        }

        private static float Bounce45Angle(float directionX)
        { 
            if (directionX < 0f)
                return -1f;

            if (directionX > 0f)
                return 1f;

            return 0f;
        }

        public void OnCameraBorderCollision()
        {
            var direction = Vector2.Reflect(_movementDirection, Vector2.down);
            _movementDirection = direction.normalized;
        }
    }
}