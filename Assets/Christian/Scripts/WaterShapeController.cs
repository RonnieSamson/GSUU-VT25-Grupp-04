using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaterShapeController : MonoBehaviour
{

  public float spread = 0.006f;
  [SerializeField]
   private float dampening = 0.02f; // Damping factor for the spring motion
   [SerializeField]
   private float springStiffness = 1.0f; // Stiffness of the spring
   private List<WaterSpring> springs = new List<WaterSpring>(); // List of springs


void Awake()
    {
        // Automatically find all WaterSpring components in child objects
        springs.AddRange(GetComponentsInChildren<WaterSpring>());
    }
  void FixedUpdate()
  {
    foreach (WaterSpring spring in springs)
    {
      spring.WaveSpringUpdate(springStiffness, dampening); // Update each spring with the specified stiffness
    }

    SpreadWave();
  }

  // Method to spread the wave effect to neighboring springs
  private void SpreadWave()
  {
    int count = springs.Count;
    float[] left_deltas = new float[count];
    float[] right_deltas = new float[count];
    for(int i = 0; i < count; i++)
    {
      if (i > 0)
      {
        left_deltas[i] = spread * (springs[i].transform.localPosition.y - springs[i - 1].transform.localPosition.y);
        springs[i - 1].velocity += left_deltas[i];
      }
      if (i < count - 1)
      {
        right_deltas[i] = spread * (springs[i].transform.localPosition.y - springs[i + 1].transform.localPosition.y);
        springs[i + 1].velocity += right_deltas[i];
      }
    }
  }

  [ContextMenu("Reset Spring")]
  public void ResetSpring()
  {
    foreach (WaterSpring spring in springs)
    {
      spring.ResetSpring(); // Reset each spring to its initial state
    }
  }

   [CustomEditor(typeof(WaterSpring))]
    public class WaterSpringEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            WaterSpring waterSpring = (WaterSpring)target;
            if (GUILayout.Button("Reset Spring"))
            {
                waterSpring.ResetSpring();
            }
        }
    }
  
}
