namespace model.timing
{
    internal interface IStepObserver
    {
        void NotifyStep(string structure, int phase, int step);
    }
}