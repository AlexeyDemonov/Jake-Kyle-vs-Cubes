using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NPCController))]
public class NPCUIController : MonoBehaviour
{
    public Transform UiPositioner;
    public RectTransform UI;
    public Text ScoreText;

    Camera _mainCamera;
    int _currentScore;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _currentScore = 0;
        ScoreText.text = "0";
        GetComponent<NPCController>().NailedTarget += IncreaseScore;
    }

    void IncreaseScore()
    {
        _currentScore++;
        ScoreText.text = _currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UI.position = _mainCamera.WorldToScreenPoint(UiPositioner.position);
    }
}