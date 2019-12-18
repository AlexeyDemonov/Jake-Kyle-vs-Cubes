using UnityEngine;

public class LazerController : MonoBehaviour
{
    public LineRenderer LazerBeam;
    public Transform Origin;

    Transform _target;

    // Update is called once per frame
    void Update()
    {
        if (LazerBeam.enabled)
        {
            LazerBeam.SetPosition(0, Origin.position);
            LazerBeam.SetPosition(1, _target.position);
        }
    }

    public void DrawLazer(Transform target)
    {
        _target = target;
        LazerBeam.enabled = true;
    }

    public void RemoveLazer()
    {
        LazerBeam.enabled = false;
        _target = null;
    }
}