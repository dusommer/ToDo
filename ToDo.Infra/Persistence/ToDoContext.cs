using System.Data.Entity;

namespace ToDo.Infra.Persistence
{
    public class ToDoContext : DbContext
    {
        public ToDoContext() : base("TODOConnectionString")
        {

        }
    }
}
