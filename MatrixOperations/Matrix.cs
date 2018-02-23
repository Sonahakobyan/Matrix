using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixOperations
{
    /// <summary>
    /// New type for matrix
    /// </summary>
    class Matrix
    {
        /// <summary>
        /// Matrix rows count
        /// </summary>
        public readonly int RowCount;

        /// <summary>
        /// Matrix columns count
        /// </summary>
        public readonly int ColumnCount;

        /// <summary>
        /// Collector for matrix elements
        /// </summary>
        public double[,] matrix;

        /// <summary>
        /// Create matrix with given size, elements generated randomly
        /// </summary>
        /// <param name="rowCount">The rows count</param>
        /// <param name="columnCount">The columns count</param>
        public Matrix(int rowCount, int columnCount)
        {
            this.RowCount = rowCount;
            this.ColumnCount = columnCount;
            Random random = new Random();
            this.matrix = new double[RowCount, ColumnCount];

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    this.matrix[i, j] = random.Next(1, 9);
                }
            }
        }

        /// <summary>
        /// Create matrix with given elements
        /// </summary>
        /// <param name="matrix">Collection of elements</param>
        public Matrix(double[,] matrix)
        {
            this.RowCount = matrix.GetUpperBound(0) - matrix.GetLowerBound(0) + 1;
            this.ColumnCount = matrix.GetUpperBound(1) - matrix.GetLowerBound(1) + 1;

            this.matrix = new double[RowCount, ColumnCount];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    this.matrix[i, j] = matrix[i, j];
                }
            }
        }

        /// <summary>
        /// Create an empty matrix
        /// </summary>
        /// <param name="size"> The size of matrix </param>
        public Matrix(int size)
        {
            this.RowCount = size;
            this.ColumnCount = size;
            this.matrix = new double[size, size];
        }

        /// <summary>
        /// Return matrix's element with given indexes
        /// </summary>
        /// <param name="i">The row index</param>
        /// <param name="j">The column index</param>
        /// <returns></returns>
        public double this[int i, int j]
        {
            get
            {
                return matrix[i, j];
            }
            set
            {
                matrix[i, j] = value;
            }
        }

        /// <summary>
        /// Check if a matrix is square
        /// </summary>
        /// <returns></returns>
        private bool IsSquare()
        {
            return this.RowCount == this.ColumnCount;
        }
        
        /// <summary>
        /// Check if a matrix is orthogonal
        /// </summary>
        /// <returns></returns>
        public bool IsOrthogonal()
        {
            if (IsSquare())
            {
                return Multiply(GetTranspose(this), this) == GetIdentityMatrix(this.RowCount);
            }
            return false;
        }

        /// <summary>
        /// Add two matrices
        /// </summary>
        /// <param name="first">The first matrix</param>
        /// <param name="second">The second matrix</param>
        /// <returns>The sum of two matrices</returns>
        public static Matrix Add(Matrix first, Matrix second)
        {
            if (!(first.RowCount == second.RowCount) && (first.ColumnCount == second.ColumnCount))
            {
                Console.WriteLine("The matrices must have the same size!");
                return null;
            }

            int rowCount = first.RowCount;
            int columnCount = first.ColumnCount;
            Matrix sum = new Matrix(rowCount, columnCount);
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    sum.matrix[i, j] = first[i, j] + second[i, j];
                }
            }
            return sum;
        }

        /// <summary>
        /// Multiply two matrices
        /// </summary>
        /// <param name="first">The first matrix</param>
        /// <param name="second">The second matrix</param>
        /// <returns></returns>
        public static Matrix Multiply(Matrix first, Matrix second)
        {
            if (!(first.ColumnCount == second.RowCount))
            {
                Console.WriteLine("The columns count of first matrix must be the same as the rows count of second matrix!");
                return null;
            }

            int rowCount = first.RowCount;
            int columnCount = second.ColumnCount;
            Matrix product = new Matrix(rowCount, columnCount);
            double curElem;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    curElem = 0;
                    for (int k = 0; k < first.ColumnCount; k++)
                    {
                        curElem += (first[i, k] * second[k, j]);
                    }
                    product[i, j] = curElem;
                }
            }
            return product;
        }

        /// <summary>
        /// Multiply matrix with scalar
        /// </summary>
        /// <param name="scalar">The given scalar</param>
        /// <param name="matrix">The given matrix</param>
        /// <returns>The product of two matrices</returns>
        public static Matrix Multiply(int scalar, Matrix matrix)
        {
            int rowCount = matrix.RowCount;
            int columnCount = matrix.ColumnCount;
            Matrix product = new Matrix(rowCount, columnCount);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    product[i, j] = scalar * matrix[i, j];
                }
            }
            return product;
        }

        /// <summary>
        /// Get largest element in a matrix
        /// </summary>
        /// <returns></returns>
        public double Max()
        {
            double max = this[0, 0];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (this[i, j] > max)
                    {
                        max = this[i, j];
                    }
                }
            }
            return max;
        }

        /// <summary>
        /// Get smallest element in a matrix
        /// </summary>
        /// <returns></returns>
        public double Min()
        {
            double min = this[0, 0];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (this[i, j] < min)
                    {
                        min = this[i, j];
                    }
                }
            }
            return min;
        }

        /// <summary>
        /// Return Identity matrix with given size
        /// </summary>
        /// <param name="size"> The size</param>
        /// <returns> The Identity matrix </returns>
        private static Matrix GetIdentityMatrix(int size)
        {
            Matrix identityMatrix = new Matrix(size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    identityMatrix[i, j] = (i == j) ? 1 : 0;
                }
            }
            return identityMatrix;
        }

        /// <summary>
        /// Get the inverse matrix of the given matrix
        /// </summary>
        /// <param name="matrix"> The argument</param>
        /// <returns> Inverse matrix</returns>
        public static Matrix GetInverse(Matrix matrix)
        {
            if (!matrix.IsSquare())
            {
                Console.WriteLine("The matrix must be square!");
                return null;
            }

            Matrix inverse = Matrix.GetIdentityMatrix(matrix.RowCount);
            int size = matrix.RowCount;
            double scalar;
            int i;

            for (int j = 0; j < size; j++)
            {
                if (matrix[j, j] == 0)
                {
                    Console.WriteLine("This matrix is not invertible!");
                    return null;
                }
                else if (matrix[j, j] != 1)
                {
                    scalar = (1 / matrix[j, j]);
                    RowMultiplication(matrix, j, scalar);
                    RowMultiplication(inverse, j, scalar);
                }

                i = 0;
                while (i < size)
                {
                    if (i != j && matrix[i, j] != 0)
                    {
                        scalar = -matrix[i, j];
                        RowAddition(matrix, i, j, scalar);
                        RowAddition(inverse, i, j, scalar);
                    }
                    i++;
                }

            }
            return inverse;
        }

        /// <summary>
        /// Get thevtranspose matrix of the given matrix
        /// </summary>
        /// <param name="matrix"> The argument</param>
        /// <returns> Inverse matrix</returns>
        public static Matrix GetTranspose(Matrix matrix)
        {
            Matrix transpose = new Matrix(matrix.ColumnCount, matrix.RowCount);
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    transpose[i, j] = matrix[j, i];
                }
            }
            return transpose;
        }

        /// <summary>
        /// Rearrangement of two rows
        /// </summary>
        /// <param name="matrix"> The given matrix </param>
        /// <param name="p"> The first row index </param>
        /// <param name="q"> The second row index </param>
        private static void RowRearrangement(Matrix matrix, int p, int q)
        {
            double tmp;
            for (int j = 0; j < matrix.ColumnCount; j++)
            {
                tmp = matrix[p, j];
                matrix[p, j] = matrix[q, j];
                matrix[q, j] = tmp;
            }
        }

        /// <summary>
        /// Multiply all row elements of a matrix to some scalar, not equal to zero;
        /// </summary>
        /// <param name="matrix"> The given martix </param>
        /// <param name="i"> The row index </param>
        /// <param name="scalar"> The given non-zero scalar </param>
        private static void RowMultiplication(Matrix matrix, int i, double scalar)
        {
            if (scalar == 0)
            {
                Console.WriteLine("The scalar can't be 0!");
                return;
            }

            for (int j = 0; j < matrix.ColumnCount; j++)
            {
                matrix[i, j] *= scalar;
            }
        }

        /// <summary>
        /// Addition of two rows of the matrix multiplied by the same scalar
        /// </summary>
        /// <param name="matrix"> The given matrix</param>
        /// <param name="q"> The firs, changing row </param>
        /// <param name="p"> The second, not changing row </param>
        /// <param name="scalar"> The given scalar </param>
        private static void RowAddition(Matrix matrix, int q, int p, double scalar)
        {
            if (q == p)
            {
                Console.WriteLine("The rows' indexes must be different!");
                return;
            }

            for (int j = 0; j < matrix.ColumnCount; j++)
            {
                matrix[q, j] += (scalar * matrix[p, j]);
            }
        }
        
        public static bool operator == (Matrix a, Matrix b)
        {
            if (!(a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount))
            {
                return false;
            }

            for (int i = 0; i < a.RowCount; i++)
            {
                for (int j = 0; j < a.ColumnCount; j++)
                {
                    if (a[i, j] != b[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        public static bool operator !=(Matrix a, Matrix b)
        {
            return !(a == b);
        }
    }
}
