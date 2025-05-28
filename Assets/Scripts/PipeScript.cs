using UnityEngine;

public class PipeScript : MonoBehaviour
{
    [SerializeField] private Transform _topPipe;     // Sleep "Uper Pipe" hiernaartoe in Unity
    [SerializeField] private Transform _bottomPipe;  // Sleep "Lower Pipe" hiernaartoe in Unity

    [SerializeField] private float _baseGap = 3.5f;
    [SerializeField] private float _gapDecreasePer10Points = 0.3f;
    [SerializeField] private float _minGap = 1.5f;
    [SerializeField] private float _maxYOffset = 1f;

    private void Start()
    {
        SetGap();
    }

    private void SetGap()
    {
        // Bepaal score
        int score = Score.Instance != null ? Score.Instance.GetScore() : 0;

        // Bereken nieuwe gap
        float gap = _baseGap - (score / 10) * _gapDecreasePer10Points;
        gap = Mathf.Max(_minGap, gap);

        // Bepaal random positie van het midden van de gap
        float centerY = Random.Range(-_maxYOffset, _maxYOffset);
        float halfGap = gap / 2f;

        // Zet boven- en onderpipe netjes tegenover elkaar
        _topPipe.localPosition = new Vector3(0, centerY + halfGap, 0);
        _bottomPipe.localPosition = new Vector3(0, centerY - halfGap, 0);
    }
}
