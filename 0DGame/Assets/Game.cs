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


    [SerializeField] private int _health;
    [SerializeField] private float timer;

    [SerializeField] private GameObject _diedMenu;
    [SerializeField] private GameObject _startMenu;

    [SerializeField] private GameObject _particle;

    [SerializeField] private TextMeshProUGUI _textPoint;
    [SerializeField] private TextMeshProUGUI _textPointRecord;

    private SpriteRenderer _spriteRenderer;
    private int _point;
    private int _ = 1;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startMenu.SetActive(true);
        _textPointRecord.text = "Рекорд: " + Progress.Instance.PlayerInfo._point.ToString();
#if UNITY_WEBGL
        ShowAdv();
#endif
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && _ > 0)
        {
            _startMenu.SetActive(false);
            Instantiate(_particle, Input.mousePosition, Quaternion.identity);
            StartCoroutine(color());
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
        if (_health <= 0)
        {
            _spriteRenderer.color = Color.white;
#if UNITY_WEBGL
            ShowAdv();
#endif
            if (Progress.Instance.PlayerInfo._point < _point)
            {
                Progress.Instance.PlayerInfo._point = _point;
#if UNITY_WEBGL
                SetToLeaderboard(Progress.Instance.PlayerInfo._point);
                Progress.Instance.Save();
#endif
            }
            _diedMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private void Point()
    {
        _point++;
        _textPoint.text = _point.ToString();
        Instantiate(_particle, Input.mousePosition, Quaternion.identity);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    private IEnumerator color()
    {
        while (true)
        {
            if (Random.Range(0, 10) > 0)
                _spriteRenderer.color = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
            else
                _spriteRenderer.color = new Color(0, 1, 0);

            yield return new WaitForSeconds(timer);
        }
    }
}