namespace UI.Interfaces
{
    public interface ICrudConsoleService<T>
    {
        void Create();

        void Delete();

        void Update();

        T CreateModel();
    }
}
