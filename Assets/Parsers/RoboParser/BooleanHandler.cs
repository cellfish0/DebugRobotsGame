namespace Assets.Parsers.RoboParser
{
    public class BooleanHandler
    {
        public BooleanHandler()
        {
        }

        public bool IsTrue(object condition)
        {
            if (condition is bool b)
            {
                return b;
            }
            else
            {
                throw new RoboError($"Value \"{condition}\" is not a condition");
            }
        }
    }
}