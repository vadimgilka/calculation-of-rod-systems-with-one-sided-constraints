using System.Collections.Generic;

namespace Diplom
{
    /// <summary>
    /// Описывает тип узла. Hinge - шарнирный, Rigid - жесткое закрепление.
    /// </summary>
    public enum NodeType
    {
        Hinge,
        Rigid
    }
    /// <summary>
    /// Описывает узел
    /// </summary>
    public class Node
    {
        public Node(int id, double x, double y, NodeType t, bool fx, bool fy, bool fa, double px, double py, double pa, string re)
        {
            Id = id;
            X = x;
            Y = y;
            Type = t;
            FixX = fx;
            FixY = fy;
            FixA = fa;
            Px = px;
            Py = py;
            Pa = pa;
            Re = re;
            Cores = new List<Core>();
        }

        public List<Core> Cores { get; }

        public double X { get; set; }

        public double Y { get; set; }

        public NodeType Type { get; set; }

        //Закрепления по осям и углу поворота
        public bool FixX { get; set; }

        public bool FixY { get; set; }

        public bool FixA { get; set; }

        public int Id { get; set; }

        //Нагрузки по осям и углу поворота
        public double Px { get; set; }

        public double Py { get; set; }

        public double Pa { get; set; }

        public string Re { get; set; }
    }
}