using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [Range(1f, 1000f)]
    public float TurnSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");

        if (Input.GetButton("Fire2") && mouseX != 0f)
        {
            Vector3 mouseTurn = new Vector3(0f, mouseX * TurnSpeed, 0f);
            this.transform.Rotate(mouseTurn);
        }
    }
}