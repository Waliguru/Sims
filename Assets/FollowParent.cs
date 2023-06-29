using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : MonoBehaviour
{
    private Transform parentTransform;

    private void Start()
    {
        parentTransform = transform.parent;
    }

    private void Update()
    {
        transform.position = parentTransform.position;
        transform.rotation = parentTransform.rotation;
        transform.localScale = parentTransform.localScale;
    }
}
