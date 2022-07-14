namespace Assets.Scripts.Puzzles.Robo.VisualNodes
{
    [System.Serializable]
    public class Link
    {
        public OutSocket From;
        public InSocket To;

        public Link(OutSocket from, InSocket to)
        {
            From = from;
            To = to;
        }

        public override bool Equals(object obj)
        {
            var link = obj as Link;
            if (link == null) return false;

            return link.From == From && link.To == To;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
