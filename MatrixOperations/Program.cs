using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixOperations
{
    class Program
    {
        static void PrintMatrix(Matrix m)
        {
            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                {
                    Console.Write(m.matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            double[,] m = new double[3, 3];
            m[0, 0] = 1;
            m[0, 1] = 1;
            m[0, 2] = -1;
            m[1, 0] = 1;
            m[1, 1] = 2;
            m[1, 2] = 0;
            m[2, 0] = -1;
            m[2, 1] = 0;
            m[2, 2] = 5;

            //int[,] n = new int[3, 4];
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        n[i, j] = i + j;
            //    }
            //}

            Matrix a = new Matrix(m);
            PrintMatrix(a);
            Console.WriteLine(a.IsOrthogonal());
            

            //Matrix inverse = Matrix.GetInverse(a);
            //if (inverse != null)
            //{
            //    PrintMatrix(inverse);
            //}

            //Matrix transpose = Matrix.GetTranspose(a);
            //if (transpose != null)
            //{
            //    PrintMatrix(transpose);
            //}
            Console.Read();
        }
    }
}
