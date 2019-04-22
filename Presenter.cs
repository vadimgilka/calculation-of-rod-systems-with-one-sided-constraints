using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Runtime.Serialization;
using MathNet.Numerics.LinearAlgebra;

namespace Diplom
{
    /// <summary>
    /// Реализует взаимодействие между Model и Form
    /// </summary>
    internal class Presenter
    {
        private readonly MainForm _view;
        private readonly SchemeModel _model;

        public Presenter(MainForm view, SchemeModel model)
        {
            _view = view;
            _model = model;
        }

        public List<Node> Nodes => _model.Nodes;
        public List<Core> Cores => _model.Cores;
        public List<Node> OffsetNodes { get; set; }
        public List<Core> OffsetCores { get; set; }

        private bool ValidateCore(int start, int end, int id)
        {
            if (start == 0 || end == 0)
            {
                _view.ErrorMsg("Узлы и стержни должны нумероваться начиная с 1", "Ошибка при создании стержня");
                return false;
            }

    

            if (_model.Nodes.Count + _model.Cores.Count > 5000)
            {
                _view.ErrorMsg("На схему нельзя добавить более 5000 элементов", "Ошибка");
                return false;
            }
            if (!CheckSameCoreNotExist(start, end, id))
                return false;
            if (_model.Nodes.Count >= start && _model.Nodes.Count >= end)
            {
                if (start != end) return true;
                _view.ErrorMsg("Начало и конец стержня не могут быть в одном узле", "Ошибка при создании стержня");
                return false;
            }
            _view.ErrorMsg("Начальный и конечный узел должны существовать", "Ошибка при создании стержня");
            return false;
        }

        public bool AddCore(int start, int end, double e, double f, double j)
        {
            if (!ValidateCore(start, end, -1) || !CheckSameCoreNotExist(start, end, -1)) return false;
            //От индексов начала и конца вычитается 1, т.к. номера идут с 1, а не с 0
            int id = _model.AddCore(new Core(0, _model.Nodes[start - 1], _model.Nodes[end - 1], e, f, j));
            _view.DisplayCoreInGrid(id, start, end, e, f, j);
            return true;
        }

        private bool CheckSameCoreNotExist(int start, int end, int id)
        {
            foreach (Core c in _model.Cores)
            {
                if (c.Start.Id == start && c.End.Id == end && c.Id != id || c.Start.Id == end && c.End.Id == start && c.Id != id)
                {
                    _view.ErrorMsg("Стержень между заданными узлами уже существует", "Ошибка");
                    return false;
                }
            }
            return true;
        }

        private bool ValidateNode(double x, double y, NodeType nt, int id)
        {
            if (Math.Abs(x) > 50000 || Math.Abs(y) > 50000)
            {
                _view.ErrorMsg("Модули координат узлов по обеим осям не должны превышать 50000", "Ошибка");
                return false;
            }
            if (_model.Nodes.Count + _model.Cores.Count > 5000)
            {
                _view.ErrorMsg("На схему нельзя добавить более 5000 элементов", "Ошибка");
                return false;
            }
            if (!CheckSameNodeNotExists(x, y, nt, id))
                return false;
            return true;
        }

        internal bool ChangeNode(int id, double x, double y, NodeType type, bool fixX, bool fixY, bool fixA, double px, double py, double pa, double re)
        {
            throw new NotImplementedException();
        }

        private bool CheckSameNodeNotExists(double x, double y, NodeType nt, int id)
        {
            foreach (Node n in _model.Nodes)
            {
                if (Math.Abs(n.X - x) < 0.00001 && Math.Abs(n.Y - y) < 0.00001 && n.Type == nt && n.Id != id)
                {
                    _view.ErrorMsg("Узел с заданными координатами и типом уже существует", "Ошибка");
                    return false;
                }
            }

            return true;
        }

        public bool AddNode(double x, double y, NodeType type, bool fixX, bool fixY, bool fixA, double px, double py,
            double pa, string re)
        {
            if (!ValidateNode(x, y, type, _model.Nodes.Count + 1)) return false;
            Node n = new Node(0, x, y, type, fixX, fixY, fixA, px, py, pa,re);
            _model.AddNode(n);
            _view.DisplayNodeInGrid(n.Id, x, y, type, fixX, fixY, fixA, px, py, pa,re);
            return true;
        }

        public void AddNodeOnLoad(Node n)
        {
            if (!ValidateNode(n.X, n.Y, n.Type, _model.Nodes.Count + 1)) return;
            _model.AddNode(n);
            _view.DisplayNodeInGrid(n.Id, n.X, n.Y, n.Type, n.FixX, n.FixY, n.FixA, n.Px, n.Py, n.Pa, n.Re);
        }

        public bool ChangeNode(int id, double x, double y, NodeType type, bool fixX, bool fixY, bool fixA, double px,
            double py, double pa, string re)
        {
            if (!ValidateNode(x, y, type, id)) return false;
            _model.ChangeNode(id, new Node(0, x, y, type, fixX, fixY, fixA, px, py, pa,re));
            return true;
        }

        public bool RemoveNode(int id)
        {
            for (var i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].Id == id)
                {
                    if (Nodes[i].Cores.Count > 0)
                    {
                        _view.ErrorMsg(
                            "Невозможно удалить узел, к которому закреплен стержень. Открепите или удалите стержень.",
                            "Ошибка");
                        return false;
                    }
                }
            }

            _model.RemoveNode(id);
            return true;
        }

        public void RemoveCore(int id)
        {
            _model.RemoveCore(id);
        }

        public bool ChangeCore(int start, int end, double e, double f, double j, int id)
        {
            if (!ValidateCore(start, end, id)) return false;
            _model.ChangeCore(id, new Core(0, Nodes[start - 1], _model.Nodes[end - 1], e, f, j));
            return true;
        }

        public void oneWayCalculation()
        {

        }


        public void Calculate()
        {
            OffsetNodes = new List<Node>();
            OffsetCores = new List<Core>();
            Matrix<double> classic = null;
            Matrix<double> lu = null;
            double condNum = 0;
            int[] fixedNodes = null;
            _model.Calculate(ref classic, ref lu, ref condNum, ref fixedNodes);
            //if (condNum != 0 && (condNum > 1E+12 || condNum < 0.00001))
            //{
            //    _view.ErrorMsg(
            //        "Схема некорректна. Число обусловленности: " + condNum +
            //        "\nЗначение числа обусловленности находится вне допустимого диапазона", "Ошибка при расчете");
            //    return;
            //}

            _view.DisplayResult(classic, lu, condNum, fixedNodes);
            foreach (var node in Nodes)
            {
                OffsetNodes.Add(new Node(node.Id, node.X, node.Y, node.Type, node.FixX, node.FixX, node.FixA, node.Px,
                    node.Py, node.Pa, node.Re));
            }

            foreach (var core in Cores)
            {
                OffsetCores.Add(new Core(core.Id, OffsetNodes[core.Start.Id - 1], OffsetNodes[core.End.Id - 1], core.E,
                    core.F, core.J));
            }

            int vectorIndex = 0;
            var i = 0;

            int measurementsCount = 2;
            // В рамах помимо смещений по осям присутствуют углы поворота
            if (!_view.Truss)
                measurementsCount = 3;

            for (; i < Nodes.Count * measurementsCount; i++)
            {
                //Для ферм все просто
                if (_view.Truss)
                {
                    if (i % 2 == 0 && !fixedNodes.Contains(i))
                    {
                        OffsetNodes[i / 2].X += classic[vectorIndex, 0];
                        vectorIndex++;
                    }

                    else if (i % 2 == 1 && !fixedNodes.Contains(i))
                    {
                        OffsetNodes[i / 2].Y += classic[vectorIndex, 0];
                        vectorIndex++;
                    }
                }
                //Для рам сложнее
                else
                {
                    //Результат расчета - вектор. В рамах помимо смещений по осям присутствуют углы поворота, их не надо визуализировать.
                    //Вычисляем их позицию в результирующем векторе и игнорируем:
                    if (!fixedNodes.Contains(i) && (i + 1) % 3 == 0)
                    {
                        vectorIndex++;
                    }

                    //Соответственно, определять ось X или Y через i % 2 не получится. 
                    else if ((i - i / 3) % 2 == 0 && !fixedNodes.Contains(i))
                    {
                        OffsetNodes[(i - i / 3) / 2].X += classic[vectorIndex, 0];
                        vectorIndex++;
                    }

                    else if ((i - i / 3) % 2 == 1 && !fixedNodes.Contains(i))
                    {
                        OffsetNodes[(i - i / 3) / 2].Y += classic[vectorIndex, 0];
                        vectorIndex++;
                    }
                }
            }
        }
    }
}