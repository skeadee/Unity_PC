using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    bool ClickOn = false;
    Ray _rayToCatapult;
    float _maxLength = 3f;
    public Transform _zeroPoint;


    public bool touchOn = false;
    bool check = true; // 이 변수는 공이 땅에 닿으면 튕기니까 한번만 호출 되도록 만든 조건

    Rigidbody2D _rb2d;
    SpringJoint2D _spring;
    Vector2 _prev_velocity;


    LineRenderer _linkback, _lineforce;
    bool _isShowLine = true;
    AngryGameMananger Angry;

    void Start()
    {
        _zeroPoint = GameObject.Find("Catapultposition").transform;
        _rayToCatapult = new Ray(_zeroPoint.position, Vector3.zero);
        _rb2d = GetComponent<Rigidbody2D>();
        _spring = GetComponent<SpringJoint2D>();
        _lineforce = GameObject.Find("LineFore").GetComponent<LineRenderer>();
        _linkback = GameObject.Find("LineBack").GetComponent<LineRenderer>();
        Angry = GameObject.Find("AngryGamaManager").GetComponent<AngryGameMananger>();

        StartCoroutine(TouchOn());
    }

    IEnumerator TouchOn()
    {
        yield return new WaitForSeconds(2f);
        touchOn = true;
    }


    void Update()
    {
        if (!touchOn) return;


        if (ClickOn)
        {
            Vector3 mouseWorldPoint =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPoint.z = 0f;

            Vector2 _newVector = mouseWorldPoint - _zeroPoint.position;
            if (_newVector.sqrMagnitude > _maxLength * _maxLength)
            {
                _rayToCatapult.direction = _newVector;
                mouseWorldPoint = _rayToCatapult.GetPoint(_maxLength);
            }


            transform.position = mouseWorldPoint;
        }


        if (_spring != null)
        {
            if (_prev_velocity.sqrMagnitude > _rb2d.velocity.sqrMagnitude)
            {
                Destroy(_spring);
                _rb2d.velocity = _prev_velocity;
                deleteLine();
            }

            if (ClickOn == false) _prev_velocity = _rb2d.velocity;
        }

        updateLine();

    }

    void updateLine()
    {
        if (!_isShowLine) return;

        _linkback.SetPosition(1, transform.position);
        _lineforce.SetPosition(1, transform.position);
    }

    void deleteLine()
    {
        _isShowLine = false;
        _lineforce.gameObject.SetActive(false);
        _linkback.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        if (!touchOn) return;

        ClickOn = true;

    }

    void OnMouseUp()
    {
        if (!touchOn) return;

        ClickOn = false;
        _rb2d.bodyType = RigidbodyType2D.Dynamic;
    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        check = false;
    }

    float t = 5f;

    void FixedUpdate()
    {
        if (Angry.GameMode == 2 || Angry.GameMode == 3) return;

        if(!check)
        {
            t -= Time.deltaTime;
            if (t < 0) Crash(); 
        }
    }

    void Crash()
    {
        if (Angry.BirdCheck != 0)
        {
            Angry.Life--;
            Destroy(Angry.Lifes[Angry.Life]);
            if (Angry.Life == 0) _rb2d.simulated = false;
        }

        _lineforce.gameObject.SetActive(true);
        _linkback.gameObject.SetActive(true);
        Angry.GameMode = 3;
    }

    
}
