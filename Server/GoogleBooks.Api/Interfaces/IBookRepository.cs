using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Interfaces
{
    public interface IBookRepository<T>
    {
        public void Add(T book);

        public void Remove(T book);

        public void SaveChamges();
    }
}
