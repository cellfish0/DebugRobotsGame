using Assets.Scripts.Puzzles.Robo.VisualNodes;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Puzzles.Robo.Environment
{
    public class SocketConnector : MonoBehaviour
    {
        public UnityEvent<InSocket, OutSocket> CreatedLink;


        private OutSocket DraggedSocket;
        private InSocket OverSocket;

        public bool IsDragged { get => DraggedSocket != null; }

        public void SetDraggedSocket(OutSocket socket)
        {
            DraggedSocket = socket;
        }

        public void TryConnect(OutSocket socket)
        {
            if(!Debug.isDebugBuild) return;

            if (OverSocket != null)
            {
                CreateLink(socket, OverSocket);
            }

            DraggedSocket = null;
        }

        public void CreateLink(OutSocket from, InSocket to)
        {
            CreateLinkSilent(from, to);
            Debug.Log($"Connected {from.Parent} to {to.Parent}");
        }

        public void CreateLinkSilent(OutSocket from, InSocket to)
        {

            if (from.AddLink(to))
            {
                CreatedLink.Invoke(to, from);
            }
        }




        //This may not work, if there's an error make a list of oversockets
        public void SetOverSocket(InSocket socket)
        {
            OverSocket = socket;
        }

        public void TryRemoveOverSocket(InSocket socket)
        {
            if (socket == OverSocket)
            {
                OverSocket = null;
            }
        }


    }
}
