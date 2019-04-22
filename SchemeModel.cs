using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace Diplom
{
    /// <summary>
    /// Описывает схему
    /// </summary>
    internal class SchemeModel
    {

        public enum SchemeType
        {
            Frame,
            Truss
        }

        public SchemeType SType { get; set; }

        /// <remarks>
        /// В строительной механике принято, что элементы (узлы и стержни) имеют идентификаторы начиная с 1.
        /// Также, принято, что при удалении какого-либо элемента идентификаторы всех последующих уменьшаются на 1.
        /// Соответственно, элементы в модели хранятся таким образом:
        /// [1,2,3,4,5,6,7], удалили элемент 3 - получаем [1,2,3,4,5,6] - элементы 4,5,6,7 перенумеровались на 3,4,5,6
        /// Из-за этого, для доступа к элементу с идентификатором n используется arr[n-1]
        /// </remarks>
        public List<Node> Nodes { get; }
        public List<Core> Cores { get; }

        public SchemeModel(SchemeType sType)
        {
            SType = sType;
            Nodes = new List<Node>();
            Cores = new List<Core>();
        }

        public int AddNode(Node n)
        {
            n.Id = Nodes.Count + 1;
            Nodes.Add(n);
            return n.Id;
        }

        public int AddCore(Core c)
        {
            c.Id = Cores.Count + 1;
            Cores.Add(c);
            c.Start.Cores.Add(c);
            c.End.Cores.Add(c);
            return c.Id;
        }

        public void ChangeNode(int id, Node n)
        {
            Nodes[id - 1].FixX = n.FixX;
            Nodes[id - 1].FixY = n.FixY;
            Nodes[id - 1].FixA = n.FixA;
            Nodes[id - 1].Px = n.Px;
            Nodes[id - 1].Py = n.Py;
            Nodes[id - 1].Pa = n.Pa;
            Nodes[id - 1].Type = n.Type;
            Nodes[id - 1].X = n.X;
            Nodes[id - 1].Y = n.Y;
            Nodes[id - 1].Re = n.Re;
        }

        public void ChangeCore(int id, Core c)
        {
            Cores[id - 1].E = c.E;
            Cores[id - 1].End.Cores.Remove(Cores[id - 1]);
            Cores[id - 1].End = c.End;
            Cores[id - 1].End.Cores.Add(Cores[id - 1]);
            Cores[id - 1].F = c.F;
            Cores[id - 1].J = c.J;
            Cores[id - 1].Start.Cores.Remove(Cores[id - 1]);
            Cores[id - 1].Start = c.Start;
            Cores[id - 1].Start.Cores.Add(Cores[id - 1]);
        }

        public void RemoveNode(int id)
        {
            for (var i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].Id == id)
                {
                    Nodes.RemoveAt(i);
                    i--;
                }
                //перенумеровываем последующие элементы
                else if (Nodes[i].Id > id)
                    Nodes[i].Id--;
            }
        }

        public void RemoveCore(int id)
        {
            for (var i = 0; i < Cores.Count; i++)
            {
                if (Cores[i].Id == id)
                {
                    Cores[i].Start.Cores.Remove(Cores[i]);
                    Cores[i].End.Cores.Remove(Cores[i]);
                    Cores.RemoveAt(i);
                    i--;
                }
                //перенумеровываем последующие элементы
                else if (Cores[i].Id > id)
                    Cores[i].Id--;
            }
        }

        /// <summary>
        /// Рассчитывает схему по КСФ МКЭ, основано на работе А.В. Игнатьева.
        /// !!!В fixedNodes лежат не идентификаторы узлов, а их индексы, т.е. если идентификаторы 1, 2, 3, 4, то индексы 0, 1, 2, 3.
        /// </summary>
        public void Calculate(ref Matrix<double> classic, ref Matrix<double> lu, ref double condNum,
            ref int[] fixedNodes)
        {
            Control.UseMultiThreading();
            var nodesV = new double[Nodes.Count, 2];
            for (var i = 0; i < Nodes.Count; i++)
            {
                nodesV[i, 0] = Nodes[i].X;
                nodesV[i, 1] = Nodes[i].Y;
            }
            
            var n = Cores.Count;
            var e = new double[n];
            var coresV = new int[n, 2];
            var f = new double[n];

            for (var i = 0; i < n; i++)
            {
                coresV[i, 0] = Cores[i].Start.Id - 1;
                coresV[i, 1] = Cores[i].End.Id - 1;


                e[i] = Cores[i].E;
                f[i] = Cores[i].F;
            }

            if (SType == SchemeType.Truss)
            {
                CalculateTruss(out classic, out lu, out condNum, out fixedNodes, coresV, nodesV, n, e, f);
            }
            else if (SType == SchemeType.Frame)
            {
                CalculateFrame(out classic, out lu, out condNum, out fixedNodes, n, coresV, nodesV, e, f);
            }
        }

        public class vector
        {
            private int x;
            private int y;

            public vector(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public static vector operator *(vector w1, vector w2)
            {
                return new vector(w1.x * w2.x, w1.y * w2.y);
            }
        }

        private double  vectMulti(Vector<double> left, Vector<double> right)
        {
            var sizeVector = left.Count;
            var resulta = new string [sizeVector];
            var resulta2 = new string[sizeVector];
            Vector test = new DenseVector(sizeVector);
            double score = 0;
            var test2 = left * right;
            //var resulta = new Vector(sizeVector);
            var res = new double[sizeVector];
            var res2 = new double[sizeVector];
            //var stringString = ""l
            for (int i=0; i<sizeVector;i++)
            {
                resulta[i] = left[i].ToString();
                res[i] = Convert.ToDouble(resulta[i]);
                resulta2[i] = right[i].ToString();
                res2[i] = Convert.ToDouble(resulta2[i]);
            }

            for(int i = 0; i < sizeVector; i++)
            {
                score = score + (res[i] * res2[i]);
            }

            //to strin
 
            return score;

        }


        ///<summary>
        ///
        ///</summary>>

        /// <summary>
        /// Рассчитывает раму по КСФ МКЭ
        /// </summary>
        private void CalculateFrame(out Matrix<double> classic, out Matrix<double> lu, out double condNum, out int[] fixedNodes, int n,
            int[,] coresV, double[,] nodesV, double[] e, double[] f)
        {

            string[] ree;
            var m = new double[n];
            for (var i = 0; i < n; i++)
            {
                m[i] = Cores[i].J;
            }
            var le = DCos(coresV, nodesV);
            var j = 0;
            var w = new double[n, 3];
            for (int i = 0; i < 3 * n; i += 3)
            {
                w[j, 0] = i;
                w[j, 1] = i + 1;
                w[j, 2] = i + 2;
                j = j + 1;
            }
            var s = Matrix<double>.Build.Dense(Cores.Count, 6);
            for (var i = 0; i < Cores.Count; i++)
            {
                s[i, 0] = (Cores[i].Start.Id - 1) * 3;
                s[i, 1] = (Cores[i].Start.Id - 1) * 3 + 1;
                //угол поворота зависит от типа узла
                if (Cores[i].Start.Type == NodeType.Rigid)
                    s[i, 2] = (Cores[i].Start.Id - 1) * 3 + 2;
                else
                    s[i, 2] = (Cores[i].Start.Id - 1) * 3 + 2 + Cores[i].Start.Cores.IndexOf(Cores[i]);
                s[i, 3] = (Cores[i].End.Id - 1) * 3;
                s[i, 4] = (Cores[i].End.Id - 1) * 3 + 1;
                if (Cores[i].End.Type == NodeType.Rigid)
                    s[i, 5] = (Cores[i].End.Id - 1) * 3 + 2;
                else
                    s[i, 5] = (Cores[i].End.Id - 1) * 3 + 2 + Cores[i].End.Cores.IndexOf(Cores[i]);
            }
            var delta = new double[n * 3];
            //epsilion
            var eps = 10;
            j = 0;
            for (var i = 0; i < 3 * n; i += 3)
            {
                var de = DelFrame(le[j, 0], e[j], m[j], f[j]);
                delta[i] = de[0];
                delta[i + 1] = de[1];
                delta[i + 2] = de[2];
                j += 1;
            }


            var del = Matrix<double>.Build.Diagonal(delta);
            var max = s.Enumerate().Max() + 1;
            var dt = Matrix<double>.Build.Dense(3 * n, (int) max);
            for (var i = 0; i < n; i++)
            {
                var dt1 = DtFrame(le.Row(i));
                for (var t = 0; t < 3; t++)
                    for (var k = 0; k < 6; k++)
                        dt[(int) w[i, t], (int) s[i, k]] = dt[(int) w[i, t], (int) s[i, k]] + dt1[t, k];
            }


            //loop while to-do
            var reaction = Matrix<double>.Build.Dense(n+1, 1);
            for (var i = 0; i < n+1; i++)
            {
                //reaction on y axis
                reaction[i, 0] = Int32.Parse(Nodes[i].Re);

            }

            var p01 = Matrix<double>.Build.Dense(dt.Transpose().RowCount, 1);
            for (var i = 0; i < Nodes.Count; i++)
            {
                p01[i * 3, 0] = Nodes[i].Px;
                p01[i * 3 + 1, 0] = Nodes[i].Py;
                p01[i * 3 + 2, 0] = Nodes[i].Pa;
            }


            var fixCount = 0;
            foreach (var t in Nodes)
            {
                if (t.FixX)
                    fixCount++;
                if (t.FixY)
                    fixCount++;
                if (t.FixA)
                    fixCount++;
            }

            //change reaction
            for (var i = 0; i < n + 1; i++)
            {
                if (reaction[i, 0] == 1)
                {
                    reaction[i, 0] = 0;
                }

                else if(reaction[i, 0] == 0)
                {
                    reaction[i, 0] = 1;
                }
            }

            int itr = 1;
            var pressure = itr * (p01 / eps);
            //while(itr <= 10){
            var rRed = new int[fixCount];
          
            j = 0;
            for (var i = 0; i < Nodes.Count; i++)
            {
                //condition to close reaction index
                //To-Do
                if (Nodes[i].FixX)
                    rRed[j++] = i * 3;
                if (Nodes[i].FixY)
                    rRed[j++] = i * 3 + 1;
                if (Nodes[i].FixA)
                    rRed[j++] = i * 3 + 2;
            }
            //change
            condNum = 0;
            var rrr = Matrix<double>.Build.Dense(dt.RowCount, dt.RowCount);
            var rr = rrr.Append(dt).Stack(dt.Append(del));
            lu = rrr.Inverse();
            fixedNodes = rRed;
            classic = rrr.Inverse();

            List<int> skipInd = new List<int>();
            while (itr<=10) {
 
                fixedNodes = rRed;
                var dtR = RedMatrCol(dt, rRed);
                var p1 = RedMatrRow(pressure, rRed);
                var rt = -dtR.Transpose();
                var p2 = Matrix<double>.Build.Dense(dt.RowCount, 1);
                var p = p1.Stack(p2);
                var r = Matrix<double>.Build.Dense(rt.RowCount, rt.RowCount);
                var mog = r.Append(rt).Stack(dtR.Append(del));                
                condNum = mog.ConditionNumber();
                mog.Determinant();
                classic = mog.Solve(p);
                lu = mog.Solve(p);

                //not correct why????
                //далее выполняем проверку
                var z1 = lu.RowCount;
                var x0 = z1 - 3 * n + 1;


                if (itr > 2)
                {
                     pressure = itr * (p01 / eps);
                }
                //перемешение


                //to normal massive
                int digt;
                var amount = new double[n + 1]; //should reduce after cutting of one or more connection
                var r1 = -dt.Transpose();//error also
                var sizeLu = lu.RowCount;


                int index = 0;
                var stress = new int[rt.RowCount];
                int h = 1; int o = 0;
                var indi = new int[n+1];
                int ind = 0;
                var check = new bool[n + 1]; // bool связь
                check[0] = true;
                bool find = false;
                List<int> listInd = new List<int>();



                for (var i = 1; h < p01.RowCount; )

                if (skipInd.Contains(i))
                   {
                        check[i] = true;
                        i++;
                        h *= 3;
                    }

                else {
                    var dddt = r1.Row((i * 3) + 1, 0, r1.ColumnCount);
                    var dddt1 = lu.Column(0, sizeLu - 9, 9) ;

                    var mul = vectMulti(dddt,dddt1);

                    amount[i] = mul - pressure[(i * 3) + 1, 0];
                    //if equal to the direction of reaction then analysis
                    if (reaction[i, 0] == 1)
                    {
                        if (amount[i] > 0)
                        //analysis
                        {
                            check[i] = true;
                            ind = i;
                            indi[i] = i;
                            listInd.Add(i);
                            find = true;
                        }
                        else //отключаем индекс связь
                        {
                            check[i] = false;
                        }
                    }
                    else if (reaction[i, 0] == 0)
                    {
                        if (amount[i] < 0)
                        //analysis
                        {
                            check[i] = true;
                            indi[i] = i;
                            ind = i;
                            listInd.Add(i);
                            find = true;
                        }
                        else //отключаем индекс
                        {
                            check[i] = false;
                        }
                    } 
                    //нет рабочии направление односторонней связи 
                    else
                    {
                        //do nothing
                    }

                    i++;
                    h *= 3;


                }

                var rrrrrr = listInd.ToArray();
                int sizeOT = listInd.Count();
                //compare reaction and get the maximum from model
                int hi2 = 0;

                bool correct = true;
                foreach (bool x in check)
                {
                    if (x != true)
                    {
                        correct = false;
                    }
                }


                if (!correct)
                {
                    for (int hi = 0; hi < amount.Count() - 1; hi++)
                    {

                        double mm;
                        //
                        for (; hi2 < sizeOT; hi2++)
                        {
                            if (hi != indi[hi2] - 1)
                            {
                                if (Math.Abs(amount[hi]) < Math.Abs(amount[hi + 1]))
                                {
                                    //максимум по абсолютному значению
                                    mm = amount[hi + 1];
                                    index = hi + 1;
                                }
                            }
                            else
                                continue;

                        }
                        hi2 = 0;
                    }
                    if (index!=0) { 
                    skipInd.Add(index);
                    //var skip = skipInd.ToArray();
                    }
                }

                if (index != 0)
                {
                    var list = new List<int>(rRed);
                    list.RemoveAt(list.Count() - index);
                    rRed = list.ToArray();
                }
                itr++;
            }

        }
      
   
        /// <summary>
        /// Рассчитывает ферму по КСФ МКЭ
        /// </summary>
        private void CalculateTruss(out Matrix<double> classic, out Matrix<double> lu, out double condNum, out int[] fixedNodes, int[,] coresV,
            double[,] nodesV, int n, double[] e, double[] f)
        {
            var le = DCos(coresV, nodesV);
            var w = Matrix<double>.Build.Dense(n, 2);
            for (var i = 0; i < n; i++)
                w[i, 0] = i;
            var s = LdgdTruss(coresV);
            var b = new double[n];

            for (var i = 0; i < n; i++)
                b[i] = DeltaTruss(le[i, 0], e[i], f[i]);
            var del = Matrix<double>.Build.Diagonal(b);
            var m = s.Enumerate().Max() + 1;
            var dt = Matrix<double>.Build.Dense(n, (int) m);
            for (var i = 0; i < n; i++)
            {
                var dt1 = DtTruss(le.Row(i));
                for (var k = 0; k < 4; k++)
                    dt[(int) w[i, 0], (int) s[i, k]] = dt[(int) w[i, 0], (int) s[i, k]] + dt1[k];
            }
            var p01 = Matrix<double>.Build.Dense(dt.Transpose().RowCount, 1);
            for (var i = 0; i < Nodes.Count; i++)
            {
                p01[i * 2, 0] = Nodes[i].Px;
                p01[i * 2 + 1, 0] = Nodes[i].Py;
            }
            var fixCount = 0;
            foreach (var t in Nodes)
            {
                if (t.FixX)
                    fixCount++;
                if (t.FixY)
                    fixCount++;
            }

            var rRed = new int[fixCount];
            var j = 0;
            for (var i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].FixX)
                    rRed[j++] = i * 2;
                if (Nodes[i].FixY)
                    rRed[j++] = i * 2 + 1;
            }

            fixedNodes = rRed;
            var dtR = RedMatrCol(dt, rRed);

            var p1 = RedMatrRow(p01, rRed);
            var rt = -dtR.Transpose();
            var p2 = Matrix<double>.Build.Dense(dt.RowCount, 1);
            var p = p1.Stack(p2);
            var r = Matrix<double>.Build.Dense(rt.RowCount, rt.RowCount);
            var mog = r.Append(rt).Stack(dtR.Append(del));
            condNum = mog.ConditionNumber();
            classic = mog.Solve(p);
            lu = mog.LU().Solve(p);
        }
        
        public static Matrix<double> DCos(int[,] cores, double[,] nodes)
        {
            var n = cores.GetLength(0);
            var le = Matrix<double>.Build.Sparse(n, 3);
            for (var i = 0; i < n; i++)
            {
                le[i, 0] = Math.Sqrt(Math.Pow(nodes[cores[i, 1], 0] - nodes[cores[i, 0], 0], 2) +
 Math.Pow(nodes[cores[i, 1], 1] - nodes[cores[i, 0], 1], 2));
                le[i, 1] = (nodes[cores[i, 1], 1] - nodes[cores[i, 0], 1]) / le[i, 0];
                le[i, 2] = (nodes[cores[i, 1], 0] - nodes[cores[i, 0], 0]) / le[i, 0];
            }

            return le;
        }
        
        public static Matrix<double> LdgdTruss(int[,] cores)
        {
            
            var n = cores.GetLength(0);
            var s = Matrix<double>.Build.Sparse(n, 4);
            for (var i = 0; i < n; i++)
            {
                s[i, 0] = 2 * cores[i, 0];
                s[i, 1] = 2 * cores[i, 0] + 1;
                s[i, 2] = 2 * cores[i, 1];
                s[i, 3] = 2 * cores[i, 1] + 1;
            }

            return s;
        }

        public static double DeltaTruss(double l, double e, double f)
        {
            return l / (e * f);
        }

        public static double[] DtTruss(Vector<double> l)
        {
            return new[] {l[1], l[2], -l[1], -l[2]};
        }

        public static Matrix<double> RedMatrCol(Matrix<double> matr, int[] rRed)
        {
            var reducedMatrix = matr;
            for (var i = 0; i < rRed.Length; i++) reducedMatrix = reducedMatrix.RemoveColumn(rRed[i] - i);

            return reducedMatrix;
        }

        public static Matrix<double> RedMatrRow(Matrix<double> matr, int[] rRed)
        {
            var reducedMatrix = matr;
            for (var i = 0; i < rRed.Length; i++) reducedMatrix = reducedMatrix.RemoveRow(rRed[i] - i);

            return reducedMatrix;
        }

        public static double[] DelFrame(double l, double e, double i, double f)
        {
            return new[] {l / (e * f), Math.Pow(l, 3) / (12 * e * i), l / (e * i)};
        }

        public static double[,] DtFrame(Vector<double> lei)
        {
            return new[,]
            {
                {lei[2], lei[1], 0, -lei[2], -lei[1], 0},
                {lei[1], -lei[2], lei[0] / 2, -lei[1], lei[2], lei[0] / 2},
                {0, 0, -1, 0, 0, 1}
            };
        }

 
    }
    public class CustomArray<T>
    {
        public T[] GetColumn(T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        public T[] GetRow(T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
    }
}

