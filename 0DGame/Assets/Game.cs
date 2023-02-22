using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowAdv();
    [DllImport("__Internal")]
    private static extern void SetToLeaderboard(int value);


    [SerializeField] private GameObject _recordText, _guideText, _restartButton, _healthText, _pointText;

    [SerializeField] private int _health;
    [SerializeField] private float _timer;

    [SerializeField] private GameObject _diedMenu;
    [SerializeField] private GameObject _startMenu;

    [SerializeField] private TextMeshProUGUI _textPoint;
    [SerializeField] private TextMeshProUGUI _textHealth;
    [SerializeField] private TextMeshProUGUI _textPointRecord;

    private SpriteRenderer _spriteRenderer;
    private int _point;
    private int _ = 1;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startMenu.SetActive(true);
        _textPointRecord.text = "Ðåêîðä: " + Progress.Instance.PlayerInfo.Point.ToString();

        LeanTween.scale(_recordText, new Vector3(1, 1, 1), 3f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(_guideText, new Vector3(1, 1, 1), 3f).setDelay(0.3f).setEase(LeanTweenType.easeOutElastic);

        ShowAdv();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _ > 0)
        {
            _startMenu.SetActive(false);
            StartCoroutine(RandomColor());
            _--;
        }

        if (Input.GetMouseButtonDown(0) && _spriteRenderer.color == Color.green && _ <= 0)
        {
            Point();
        }
        else if (Input.GetMouseButtonDown(0) && _spriteRenderer.color != Color.green && _ <= 0)
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        _health--;
        _textHealth.text = _health.ToString();

        LeanTween.scale(_healthText, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutElastic);
        _healthText.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        if (_health <= 0)
        {
            LeanTween.scale(_restartButton, new Vector3(1, 1, 1), 0.8f).setEase(LeanTweenType.easeOutQuint);
            _spriteRenderer.color = Color.white;

            ShowAdv();
            if (Progress.Instance.PlayerInfo.Point < _point)
            {
                Progress.Instance.PlayerInfo.Point = _point;
                SetToLeaderboard(Progress.Instance.PlayerInfo.Point);
                Progress.Instance.Save();
            }
            _diedMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void Point()
    {
        _point++;
        _textPoint.text = _point.ToString();

        LeanTween.scale(_pointText, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutElastic);
        _pointText.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator RandomColor()
    {
        while (true)
        {
            if (Random.Range(0, 10) > 0)
                _spriteRenderer.color = new Color(Random.Range(0, 1.0f), Random.Range(0, 0.5f), Random.Range(0, 1.0f));
            else
                _spriteRenderer.color = Color.green;

            yield return new WaitForSeconds(_timer);
        }
    }
}
