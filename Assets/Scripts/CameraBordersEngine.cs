using UnityEngine;

namespace PingPongEngine
{
    public class CameraBordersEngine : MonoBehaviour
    {
        [SerializeField] private Ball _ball;
        [SerializeField] private ScoreCounter _scoreCounter;
        private Vector2 _screenSizeHalf;

        public Vector2 ScreenSizeHalf => _screenSizeHalf;

        private void Start()
        {
            var height = Camera.main.orthographicSize - .1f;
            var width = Camera.main.aspect * Camera.main.orthographicSize;

            _screenSizeHalf = new Vector2(width,height); 
        }

        private void Update()
        {
            GameBordersCheck(); 
        }

        public void GameBordersCheck()
        {
            var ballPos = _ball.transform.position;

            PaddleBorderCrossCheck(ballPos);

            SideBorderBounceCheck(ballPos); 
        }

        private void SideBorderBounceCheck(Vector3 ballPos)
        {
            if (ballPos.y > _screenSizeHalf.y || ballPos.y < -_screenSizeHalf.y)
                _ball.OnCameraBorderCollision();
        }

        private void PaddleBorderCrossCheck(Vector3 ballPos)
        {
            if (ballPos.x < -_screenSizeHalf.x) // left
                ScoreUpdate(Side.Left);
            else if (ballPos.x > _screenSizeHalf.x) //right
                ScoreUpdate(Side.Right);
        }

        private void ScoreUpdate(Side side)
        {
            _ball.ResetPosition();
            _scoreCounter.AddScore(side);
        }
    }
}