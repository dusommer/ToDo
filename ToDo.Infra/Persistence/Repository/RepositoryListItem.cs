using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces.Repositories;
using ToDo.Infra.Persistence.Repository.Base;

namespace ToDo.Infra.Persistence.Repository
{
    public class RepositoryListItem : RepositoryBase<ListItem, int>, IRepositoryListItem
    {
        protected readonly ToDoContext _context;

        public RepositoryListItem(ToDoContext context) : base(context)
        {
            _context = context;
        }
    }
}
