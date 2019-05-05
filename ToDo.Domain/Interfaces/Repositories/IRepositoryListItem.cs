using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces.Repositories.Base;

namespace ToDo.Domain.Interfaces.Repositories
{
    public interface IRepositoryListItem : IRepositoryBase<ListItem, int>
    {
    }
}
