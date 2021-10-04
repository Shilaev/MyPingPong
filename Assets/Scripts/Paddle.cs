using System;
using UnityEngine;

namespace PingPongEngine
{
    public abstract class Paddle : MonoBehaviour
    {
        [SerializeField] private protected Ball _ball;
        [SerializeField] private protected CameraBordersEngine camera;
        private protected readonly float BOUNCE_POINT_OFFSET = 0.1f;
        private protected float _bouncePoint;
        private protected float _downPaddlePoint;
        private protected float _movementSpeed = 30.0f;
        private protected Transform _paddlePosition;
        private protected float _upPaddlePoint;
        public int Score { get; set; } // For future score mechanics
        private protected abstract float CalculateBouncePoint(Transform paddle);

        private protected void CheckForColission(float upPaddlePoint, float downPaddlePoint, float bouncePoint,
            Transform paddlePosition)
        {
            var ballPos = _ball.transform.position; // Curent ball position
            var isInsideThePaddle = ballPos.y < upPaddlePoint && ballPos.y > downPaddlePoint;
            if (isInsideThePaddle) // is ball inside the paddle?
            {
                if ((bouncePoint < 0) & (ballPos.x < bouncePoint)) _ball.OnPaddleCollision(paddlePosition);
                if ((bouncePoint > 0) & (ballPos.x > bouncePoint)) _ball.OnPaddleCollision(paddlePosition);
            }
        }

        private protected float CalculateUpPaddlePoint(Transform paddle)
        {
            var paddlePositionY = paddle.transform.position.y;
            var paddleScaleY = paddle.transform.localScale.y;

            var upPaddlePoint = paddlePositionY + paddleScaleY / 2.0f;
            return upPaddlePoint;
        }

        private protected float CalculateDownPaddlePoint(Transform paddle)
        {
            var paddlePositionY = paddle.transform.position.y;
            var paddleScaleY = paddle.transform.localScale.y;

            var downPaddlePoint = paddlePositionY - paddleScaleY / 2.0f;
            return downPaddlePoint;
        }

        private protected void UserControl()
        {
            // camera border position + paddle half
            var minY = -camera.ScreenSizeHalf.y + 1f;
            var maxY = camera.ScreenSizeHalf.y - 1f;

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {           
                float yMove = Input.GetAxis("Vertical");
                transform.Translate(new Vector3(0f, yMove * Time.deltaTime * _movementSpeed, 0f));
            }

            // Clamp (limit) movement by camera borders
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minY, maxY));

        }

        public void AddScore()
        {
            Score += 1;
        }
    }
}