using UnityEngine;

public class Lightning : MonoBehaviour
{
    public Transform start;
    public Transform end;

    private Transform _target1;
    private Transform _target2;

    public void SetTargets(Transform target1, Transform target2)
    {
        _target1 = target1;
        _target2 = target2;
        
        start.transform.position = _target1.position;
        end.transform.position = _target2.position;
    }

    void Update()
    {
        if (_target1 && _target2)
        {
            start.transform.position = _target1.position;
            end.transform.position = _target2.position;
        }
    }
}