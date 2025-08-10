using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class LineRender : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        //public Transform startPointLine;
        public Vector3 startPointLine;
        // Start is called before the first frame update
        void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            //startPointLine = transform.position;
            Debug.Log(startPointLine);
        }
        public void MakeLineRender(Vector3 posLine)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, startPointLine);
            _lineRenderer.SetPosition(1, posLine);

        }
    }
}