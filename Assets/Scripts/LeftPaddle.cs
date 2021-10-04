using PingPongEngine;
using UnityEngine;

public class LeftPaddle : Paddle
{
    // Need to refactor this

    private void Update()
    {
        UserControl();
        _paddlePosition = transform;
        _upPaddlePoint = CalculateUpPaddlePoint(_paddlePosition);
        _downPaddlePoint = CalculateDownPaddlePoint(_paddlePosition);
        _bouncePoint = CalculateBouncePoint(_paddlePosition);
        CheckForColission(_upPaddlePoint, _downPaddlePoint, _bouncePoint, _paddlePosition);
    }

    private protected override float CalculateBouncePoint(Transform paddle)
    {
        var positionX = paddle.transform.position.x;

        var bouncePoint = positionX + BOUNCE_POINT_OFFSET + _ball.BallWidth / 2;
        return bouncePoint;
    }
}