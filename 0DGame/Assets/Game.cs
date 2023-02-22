using DG.Tweening;
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
        _textPointRecord.text = "Рекорд: " + Progress.Instance.PlayerInfo.Point.ToString();

<<<<<<< HEAD
        LeanTween.scale(_recordText, new Vector3(1, 1, 1), 3f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(_guideText, new Vector3(1, 1, 1), 3f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);
=======
        _recordText.transform.DOScale(new Vector3(1, 1, 1), 3f).SetDelay(0.07f).SetEase(Ease.OutElastic);
        _guideText.transform.DOScale(new Vector3(1, 1, 1), 2.3f).SetDelay(0.32f).SetEase(Ease.OutElastic).OnComplete(() =>
        {
            _guideText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.8f).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
        });
>>>>>>> parent of 21bc703 (Merge branch 'master' of https://github.com/Hitsar/0DGame)

        //ShowAdv();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _ > 0)
        {
            _startMenu.SetActive(false);
            StartCoroutine(RandomColor());
            _--;
        }

        if (Input.GetMouseButtonDown(0) && _spriteRenderer.color == Color.green)
        {
            Point();
        }
        else if (Input.GetMouseButtonDown(0) && _spriteRenderer.color != Color.green)
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        _health--;
        _textHealth.text = _health.ToString();

        _healthText.transform.DOScale(new Vector3(1, 1, 1), 0.6f).SetEase(Ease.OutElastic);
        _healthText.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        if (_health <= 0)
        {
<<<<<<< HEAD
            LeanTween.scale(_restartButton, new Vector3(1, 1, 1), 0.9f).setEase(LeanTweenType.easeOutQuint);
=======
            _restartButton.transform.DOLocalMoveY(0, 0.9f).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                _restartButton.transform.DOScale(new Vector3(1, 1, 1), 0.9f).SetEase(Ease.OutElastic);
            });
>>>>>>> parent of 21bc703 (Merge branch 'master' of https://github.com/Hitsar/0DGame)
            _spriteRenderer.color = Color.white;

            //  ShowAdv();
            if (Progress.Instance.PlayerInfo.Point < _point)
            {
                Progress.Instance.PlayerInfo.Point = _point;
                //       SetToLeaderboard(Progress.Instance.PlayerInfo.Point);
                //       Progress.Instance.Save();
            }
            _diedMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void Point()
    {
        _point++;
        _textPoint.text = _point.ToString();

        _pointText.transform.DOScale(new Vector3(1, 1, 1), 0.6f).SetEase(Ease.OutElastic);
        _pointText.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
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