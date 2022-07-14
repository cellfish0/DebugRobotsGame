using System.Collections.Generic;
using Assets.Scripts.Puzzles.Robo.Environment;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes
{
    public class LinkRenderer : RoboBaseBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private LineRenderer rendererPrefab;
        [SerializeField] private static float capLength = 0.15f;
        private List<Link> links = new List<Link>();
        public class Link
        {
            public Link(LineRenderer lineRenderer, InSocket _in, OutSocket _out, LinkRenderer parent)
            {
                this._lineRenderer = lineRenderer;
                this._in = _in;
                this._out = _out;

                _in.Moved.AddListener(UpdateInPosition);
                _out.Moved.AddListener(UpdateOutPosition);
            }

            private readonly LineRenderer _lineRenderer;
            private InSocket _in;
            private OutSocket _out;

            public void UpdateInPosition(Vector3 new_pos)
            {
                _lineRenderer.SetPosition(0, new_pos);
                _lineRenderer.SetPosition(1, new_pos + capLength * Vector3.left);
            }

            public void UpdateOutPosition(Vector3 new_pos)
            {
                _lineRenderer.SetPosition(3, new_pos);
                _lineRenderer.SetPosition(2, new_pos + capLength * Vector3.right);
            }
        }
        public void CreateLink(InSocket _in, OutSocket _out)
        {

            LineRenderer lineRenderer = Instantiate(rendererPrefab, transform);
            lineRenderer.transform.SetParent(ProgramStarter.Program.transform);

            Vector3[] positions = GeneratePositions(_in, _out);
            lineRenderer.positionCount = 4;
            lineRenderer.SetPositions(positions);

            links.Add(new Link(lineRenderer, _in, _out, this));
        }



        private Vector3[] GeneratePositions(Socket _in, Socket _out)
        {
            List<Vector3> positions = new List<Vector3>();

            positions.Add(_in.Position);
            positions.Add(_in.Position + Vector3.left * capLength);
            positions.Add(_out.Position + Vector3.right * capLength);
            positions.Add(_out.Position);

            return positions.ToArray();
        }
    }
}
