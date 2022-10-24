using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    void MoveToTarget(Vector3 targetPosition);
    void Selected(bool selected);
}
