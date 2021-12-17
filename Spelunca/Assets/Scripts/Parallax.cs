using UnityEngine;

/// <summary>
///  This class is a script apply to different background layers to create a depth sensation.
/// </summary>
public class Parallax : MonoBehaviour
{
    /// <value>
    /// <c>camera</c> is a GameObject which contain the player's camera.
    /// </value>
    public GameObject camera;
    /// <value>
    /// <c>parallaxIntensity</c> is a float which indicate the power of the depth effect on the layer where this script is apply.
    /// </value>
    public float parallaxIntensity;
    /// <value>
    /// <c>_length</c> is a float which indicate the x size of the component where this script is apply.
    /// </value>
    private float _length;
    /// <value>
    /// <c>_length</c> is a float which indicate the starting x position of the component where this script is apply.
    /// </value>
    private float _startPos;

    /// <summary>
    /// Function executed at the start of the program.
    /// We initialize the variables used in the FixedUpdate.
    /// </summary>
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    /// <summary>
    /// Function executed a fixed times per second.
    /// Each fixed frame we update the position of the current GameObject to make the depth sensation.
    /// </summary>
    void FixedUpdate()
    {
        float temp = (camera.transform.position.x * (1 - parallaxIntensity));
        float dist = (camera.transform.position.x * parallaxIntensity);

        transform.position = new Vector3(_startPos + dist, camera.transform.position.y, transform.position.z);

        if (temp > _startPos + _length)
            _startPos += _length;
        else if (temp < _startPos - _length)
            _startPos -= _length;
    }
}
