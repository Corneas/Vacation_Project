using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TestDir : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField]
    private Vector2 _vector;
    [SerializeField]
    private bool _isGizmos = true;
    [SerializeField]
    private float _speed = 10f;

    private Vector2 _startPos;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _startPos = transform.position;

        Vector3 dir = (Vector3)_vector - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, angle);

    }

    private void Update()
    {
        Debug.DrawLine(transform.position, (Vector2)transform.position + _vector.normalized/*+ _rb.velocity.normalized*/, Color.magenta, 1f);

        //transform.position += (Vector3)_vector.normalized * _speed * Time.deltaTime;

        transform.Translate(Vector3.right * _speed * Time.deltaTime);

        //_rb.velocity = _vector;
    }

    private void OnDrawGizmos()
    {
        if (_isGizmos)
            Gizmos.DrawLine(_startPos, _vector.normalized * 100f);
    }
}