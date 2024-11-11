using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class LineManager : MonoBehaviour
{
public LineRenderer line;
public Transform pos1;
public Transform pos2;
// Start is called before the first frame update
void Start()
{
line.positionCount = 2;
}

void Update()
{
line.SetPosition(0, pos1.position);
line.SetPosition(1, pos2.position);
}
}