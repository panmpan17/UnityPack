using UnityEngine;
using TMPro;
using MPack;


[RequireComponent(typeof(TextMeshPro))]
public class TestTimer : MonoBehaviour
{
    [SerializeField]
    Timer updateTimer, resetTimer;
    [SerializeField]
    FloatLerpTimer floatLerp;
    [SerializeField]
    AnimationCurve curve;
    [SerializeField]
    Vector3LerpTimer positionLerpTimer;
    [SerializeField]
    ColorLerpTimer colorLerpTimer;

    TextMeshPro text;

    private void Awake() {
        text = GetComponent<TextMeshPro>();
    }


    void Update()
    {
        if (positionLerpTimer.Timer.UpdateEnd) {
            transform.position = positionLerpTimer.Value;
            positionLerpTimer.Timer.ReverseMode = !positionLerpTimer.Timer.ReverseMode;
        }
        else transform.position = positionLerpTimer.Value;

        if (colorLerpTimer.Timer.UpdateEnd)
        {
            text.color = colorLerpTimer.Value;
            colorLerpTimer.Timer.ReverseMode = !colorLerpTimer.Timer.ReverseMode;
        }
        else text.color = colorLerpTimer.Value;

        if (resetTimer.Running) {
            if (resetTimer.UpdateEnd) resetTimer.Running = false;
            return;
        }

        if (floatLerp.Timer.UpdateEnd) {
            updateTimer.Reset();
            resetTimer.Reset();
            floatLerp.Timer.Reset();
            text.text = "0";
        }
        else {
            if (updateTimer.UpdateEnd)
            {
                text.text = floatLerp.CurvedValue(curve).ToString();
                updateTimer.Reset();
            }
        }
    }
}
