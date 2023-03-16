using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToMouse : MonoBehaviour
{
    [SerializeField] private int _degreeOffset;
    void Update()
    {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + _degreeOffset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
