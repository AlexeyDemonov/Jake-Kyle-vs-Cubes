using UnityEditor;
using UnityEngine;

public class GameQuitter : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}