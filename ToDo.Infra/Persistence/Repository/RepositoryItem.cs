﻿using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces.Repositories;
using ToDo.Infra.Persistence.Repository.Base;

namespace ToDo.Infra.Persistence.Repository
{
    public class RepositoryItem : RepositoryBase<Item, int>, IRepositoryItem
    {
        protected readonly ToDoContext _context;

        public RepositoryItem(ToDoContext context) : base(context)
        {
            _context = context;
        }
    }
}
