using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCollectionLibrary
{
    public class Point<T>
    {
        private object v;

        public Point(T data)
        {
            Data = data;
            IsDeleted = false;
        }

        public T Data { get; set; }
        public bool IsDeleted { get; set; }
    }
}
