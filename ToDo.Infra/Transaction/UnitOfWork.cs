using ToDo.Infra.Persistence;

namespace ToDo.Infra.Transaction
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToDoContext _context;

        public UnitOfWork(ToDoContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
