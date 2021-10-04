using System;
using UnityEngine;
using UnityEngine.UI;

namespace PingPongEngine
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private LeftPaddle leftPaddle;
        [SerializeField] private RightPaddle rightPaddle;
        [SerializeField] private Ball _ball;
        [SerializeField] private int _maxScore;
        [SerializeField] private Text _leftPaddleScore_ui;
        [SerializeField] private Text _rightPaddleScore_ui;
        [SerializeField] private Text _gameOver;

        private void Start()
        {
            _gameOver.enabled = false;
        }

        public void AddScore(Side side)
        {
            if ((leftPaddle.Score < _maxScore) & (rightPaddle.Score < _maxScore))
            {
                if (side == Side.Left)
                {
                    rightPaddle.AddScore();
                    _rightPaddleScore_ui.text = rightPaddle.Score.ToString();
                    Debug.Log(rightPaddle.Score.ToString());
                }
                else if (side == Side.Right)
                {
                    leftPaddle.AddScore();
                    _leftPaddleScore_ui.text = leftPaddle.Score.ToString();
                    Debug.Log(leftPaddle.Score.ToString());
                }
                else
                {
                    throw new ArgumentException("We try to add score to the unknown side");
                }
            }

            if (leftPaddle.Score >= _maxScore)
            {
                _ball.StopBall();
                _ball.ResetPosition();

                _gameOver.enabled = true;
                _gameOver.text = "LEFT IS WIN";
            }

            if (rightPaddle.Score >= _maxScore)
            {
                _ball.StopBall();
                _ball.ResetPosition();

                _gameOver.enabled = true;
                _gameOver.text = "RIGHT IS WIN";
            }
        }
    }
}