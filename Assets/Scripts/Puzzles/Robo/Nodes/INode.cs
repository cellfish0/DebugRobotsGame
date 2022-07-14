namespace Assets.Scripts.Puzzles.Robo.Nodes
{
    public interface INode
    {
        string FuncName { get; }
        string FuncReturns { get; }
        string Text { get; }
    }
}