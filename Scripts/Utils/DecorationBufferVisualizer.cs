using UnityEngine;

public class DecorationBufferVisualizer : MonoBehaviour
{
  public RectXZ decorationAreaBuffer; // Set this externally or use from your main code
  public Color bufferColor = Color.red; // Color for the buffer visualization

  private void OnDrawGizmos()
  {
    Gizmos.color = bufferColor;

    // Calculate the center and size for the buffer rectangle
    Vector3 bufferCenter = new Vector3(
        (decorationAreaBuffer.xMin + decorationAreaBuffer.xMax) / 2f * GridConfig.CellSize.x,
        0,
        (decorationAreaBuffer.zMin + decorationAreaBuffer.zMax) / 2f * GridConfig.CellSize.z
    );

    Vector3 bufferSize = new Vector3(
        (decorationAreaBuffer.xMax - decorationAreaBuffer.xMin) * GridConfig.CellSize.x,
        0.1f, // Height of the gizmo for visibility
        (decorationAreaBuffer.zMax - decorationAreaBuffer.zMin) * GridConfig.CellSize.z
    );

    Gizmos.DrawWireCube(bufferCenter, bufferSize); // Draws the buffer area as a wireframe cube
  }
}
