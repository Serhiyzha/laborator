using StudentApp.DataAccess.Models;
using StudentApp.DataAccess.Repositories;

namespace StudentApp.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IRepository<IPerson> repository = new JsonFileRepository<IPerson>();
            
            ConsoleMenu menu = new ConsoleMenu(repository);
            
            menu.Run();
        }
    }
}