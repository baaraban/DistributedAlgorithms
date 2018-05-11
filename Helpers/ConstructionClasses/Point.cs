namespace Helpers.ConstructionClasses
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public override string ToString()
        {
            return string.Format("x: {0}; y: {1};", X, Y);
        }
    }
}
