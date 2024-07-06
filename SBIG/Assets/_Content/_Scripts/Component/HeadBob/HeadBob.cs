using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] float _bobbingSpeed = 0.18f;
    [SerializeField] float _bobbingAmount = 0.2f;
    [SerializeField] float _midpoint = 2.0f;

    private float _timer = 0.0f;

    public void Bob(Vector2 moveDirection)
    {
        float waveslice = 0.0f;
        float horizontal = moveDirection.x;
        float vertical = moveDirection.y;

        Vector3 cSharpConversion = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            //timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(_timer);
            _timer = _timer + _bobbingSpeed;
            if (_timer > Mathf.PI * 2)
            {
                _timer = _timer - (Mathf.PI * 2);
            }
        }
        if (waveslice != 0)
        {
            float translateChange = waveslice * _bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0f, 1.0f);
            translateChange = totalAxes * translateChange;
            cSharpConversion.y = _midpoint + translateChange;
        }
        else
        {
            // cSharpConversion.y = midpoint;
        }

        transform.localPosition = cSharpConversion;
    }
}
