using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAtCamera : MonoBehaviour
{
  private void LateUpdate()
  {
    transform.rotation = Quaternion.Euler(0f, 45f, 0f);
  }
}
