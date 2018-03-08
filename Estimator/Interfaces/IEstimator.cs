namespace Estimator.Interfaces
{
    interface IEstimator<T> where T: class
    {
        void Estimate(string outputPath);
    }
}
