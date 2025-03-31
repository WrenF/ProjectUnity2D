using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Transform Target;
    public float MoveSpeed;
    
    private Vector3 _parallaxStartPosition;
    private Vector3 _targetStartPosition;

    private void Start()
    {
        _parallaxStartPosition = transform.position;
        _targetStartPosition = Target.transform.position;
    }

    private void Update()
    {
        float offset = (Target.transform.position.x - _targetStartPosition.x) * MoveSpeed;
        transform.position = new Vector3(_parallaxStartPosition.x + offset, transform.position.y, transform.position.z);
    }
}
