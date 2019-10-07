namespace model.choices
{
    public interface IChoice
    {
        bool IsLegal();
        void Make();
    }
}