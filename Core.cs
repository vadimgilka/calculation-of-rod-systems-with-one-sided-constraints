using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    /// <summary>
    /// Описывает стержень
    /// </summary>
    public class Core
    {
        public Core(int id, Node st, Node en, double e, double f, double j)
        {
            Id = id;
            Start = st;
            End = en;
            E = e;
            F = f;
            J = j;
        }

        public Node Start { get; set; }

        public Node End { get; set; }

        //модуль упругости
        public double E { get; set; }
        //площадь
        public double F { get; set; }
        //момент инерции сечения
        public double J { get; set; }

        public int Id { get; set; }
    }
}
