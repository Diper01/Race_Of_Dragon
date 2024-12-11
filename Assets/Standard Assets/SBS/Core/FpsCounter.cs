using UnityEngine;
using System.Collections;

[AddComponentMenu("SBS/Core/FpsCounter")]
public class FpsCounter : MonoBehaviour
{
    // Attach this to any object to make a frames/second indicator.
    //
    // It calculates frames/second over each updateInterval,
    // so the display does not keep changing wildly.
    //
    // It is also fairly accurate at very low FPS counts (<10).
    // We do this not by simply counting frames per interval, but
    // by accumulating FPS for each frame. This way we end up with
    // corstartRect overall FPS even if the interval renders something like
    // 5.5 frames.

//    public Rect startRect = new Rect(10, 10, 75, 50); // The rect the window is initially displayed at.
//    public Color color = Color.white;
    public float frequency = 0.5F; // The update frequency of the fps
    public int nbDecimal = 1; // How many decimal do you want to display

    private float accum = 0f; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private string sFPS = ""; // The fps formatted into a string.
    private GUIStyle style; // The style the text will be displayed at, based en defaultSkin.label.
    protected bool showGUI = false;

    void Awake()
    {
        useGUILayout = false;
    }

    void Start()
    {
//        StartCoroutine(FPS());
    }

    void Update()
    {
        accum += Time.timeScale / Time.deltaTime;
        ++frames;
    }


    void OnGUI()
    {
       
    }

    #region Messages

    void SetGUIVisibility(bool visibility)
    {
        showGUI = visibility;
    }

    #endregion

}