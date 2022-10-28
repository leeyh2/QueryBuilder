using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    internal class ProjectRoot
    {
        public static string Root => Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
    }
}
