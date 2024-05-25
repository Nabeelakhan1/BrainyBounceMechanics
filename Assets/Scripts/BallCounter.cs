using UnityEngine;
using UnityEngine.UI; // Import the UnityEngine.UI namespace to access the Text type

public class BallCounter : MonoBehaviour
{
    public Text ballCounterText;

    // Method to update the ball counter text
    public void UpdateBallCounter(int remainingBalls)
    {
        ballCounterText.text = "Balls: " + remainingBalls;
    }
}
