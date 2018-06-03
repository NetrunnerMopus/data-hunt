namespace model.timing
{
    public interface IStepObserver
    {
        void NotifyStep(string structure, int phase, int step);
    }
}