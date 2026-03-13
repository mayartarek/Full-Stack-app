using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Helper
{
    public class PaginationList<T>
    {
        public List<T> List { get; set; } = new List<T>();
        public int Page { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }

    }
}
