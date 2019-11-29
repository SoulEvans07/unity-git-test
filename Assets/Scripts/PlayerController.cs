using UnityEngine;

public class PlayerController : MonoBehaviour {
    private static float EPS = 0.001f;
    private static float SIZE = 0.64f;
    
    private Transform _transform;
    private Rigidbody2D _rigidbody;

    public Transform _camera;

    public float speed = 10;
    public float angularSpeed = 0.2f;
    public float cameraSpeed = 0.5f;

    private void Awake() {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        
        Move(x, y);
    }

    private bool isVerticalAllowed() {
        return Mathf.Abs(_transform.position.x % SIZE) < EPS;
    }

    private float verticalMultiplier() {
        return isVerticalAllowed() ? 1 : 0;
    }

    private bool isHorizontalAllowed() {
        return Mathf.Abs(_transform.position.y % SIZE) < EPS;
    }

    private float horizontalMultiplier() {
        return isHorizontalAllowed() ? 1 : 0;
    }

    private void Move(float x, float y) {
        // p.y = v.x - (y1 - p.y)
        // p.x = y1 - p.y // p.y vill change

        /*float movement = 0;
        if (Mathf.Abs(x) < Mathf.Abs(y)) {
            movement = y;
        } else {
            movement = x;
        }

        if (isVerticalAllowed() && isHorizontalAllowed()) {
            _transform.position += new Vector3(x * speed, y * speed, 0);
        } else if (isVerticalAllowed() ) {
            _transform.position += new Vector3(0, movement * speed, 0);
        } else if (isHorizontalAllowed()) {
            _transform.position += new Vector3(movement * speed, 0, 0);
        }*/


        _transform.position += _transform.up * y * speed;
        _transform.rotation = Quaternion.Euler(0, 0, _transform.rotation.eulerAngles.z + x * angularSpeed);
        _camera.position = Vector3.Lerp(_camera.position, _transform.position, cameraSpeed);
        _camera.position = new Vector3(_camera.position.x, _camera.position.y, -10);
    }
}