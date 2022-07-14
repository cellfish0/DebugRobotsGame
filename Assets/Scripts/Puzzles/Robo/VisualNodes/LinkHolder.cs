using System.Collections.Generic;
using Assets.Scripts.Puzzles.Robo.Environment;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes
{
    public class LinkHolder : RoboBaseBehaviour
    {
        [SerializeField] private List<Link> links;
        public List<Link> Links { get => links; set => links = value; }


        public void Init()
        {
            //Debug.Log(SocketConnector);
            SocketConnector.CreatedLink.AddListener((to, from) => AddLink(from, to));
        }
        public void AddLink(OutSocket from, InSocket to)
        {
            Link item = new Link(from, to);
            if (!links.Contains(item))
            {
                links.Add(item);
            }
        }
    }
}
