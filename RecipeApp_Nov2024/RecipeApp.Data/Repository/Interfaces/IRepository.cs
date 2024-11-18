namespace RecipeApp.Data.Repository.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int id);

        T GetByIdAsync(int id);
    }
}
