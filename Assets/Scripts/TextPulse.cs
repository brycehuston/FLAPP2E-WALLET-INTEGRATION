using UnityEngine;

public class TextPulse : MonoBehaviour
{
    public float pulseSpeed = 1.0f;
    public float pulseMaxSize = 1.2f;
    public float pulseMinSize = 0.8f;

    private float originalSize;
    private bool increasing = true;
    private bool isPulsing = false; // Declare this variable in the class scope

    void Start()
    {
        originalSize = transform.localScale.x;
    }

    void Update()
    {
        if (!isPulsing) return; // Check if pulsing is enabled

        float scale = transform.localScale.x;

        if (increasing)
        {
            scale += pulseSpeed * Time.deltaTime;
            if (scale >= originalSize * pulseMaxSize)
            {
                increasing = false;
            }
        }
        else
        {
            scale -= pulseSpeed * Time.deltaTime;
            if (scale <= originalSize * pulseMinSize)
            {
                increasing = true;
            }
        }

        transform.localScale = new Vector3(scale, scale, scale);
    }

    public void StartPulsing()
    {
        isPulsing = true;
    }

    public void StopPulsing()
    {
        isPulsing = false;
        transform.localScale = new Vector3(originalSize, originalSize, originalSize); // Reset scale to original size
    }
}
