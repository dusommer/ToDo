using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Infra.Transaction
{
    public class UnitOfWork : IUnitOfWork
    {
        //private readonly HBSISContexto _contexto;

        //public UnitOfWork(HBSISContexto contexto)
        //{
        //    _contexto = contexto;
        //}

        public void Commit()
        {
            //_contexto.SaveChanges();
        }
    }
}
