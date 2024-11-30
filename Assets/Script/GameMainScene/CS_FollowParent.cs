using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_FollowParent : MonoBehaviour
{
    public void MoveParentPos(Vector3 Pos)
    {
        transform.position = Pos;
    }
}
