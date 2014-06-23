﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiSonar2
{
    class Matrix
    {
        private double[] _Data;
        private int _Rows;
        private int _Cols;

        public Matrix(Matrix size)
        {
            _Rows = (int)size[0];
            _Cols = (int)size[1];
            _Data = new double[_Rows * _Cols];
        }

        public Matrix(int rows, int cols)
        {
            _Rows = rows;
            _Cols = cols;
            _Data = new double[_Rows * _Cols];
        }
        public static Matrix size(Matrix x)
        {
            Matrix s = new Matrix(1, 2);
            s[0] = x._Rows;
            s[1] = x._Cols;
            return s;
        }

        public static int length(Matrix x)
        { 
            return (x._Rows > x._Cols) ? x._Rows : x._Cols;
        }

        public static int numel(Matrix x)
        {
            return x._Rows * x._Cols;
        }
        
        public static Matrix operator +(Matrix x, Matrix y)
        {
            Matrix z = new Matrix(size(x));

            if (length(y) == 1)
                for (int i = 0; i < length(x); i++)
                    z[i] = x[i] + y[0];
            
            else if (length(y) == length(x))
                for (int i = 0; i < length(x); i++)
                    z[i] = x[i] + y[i];

            else
                throw new ArgumentException("Error using  + \n Matrix dimensions must agree");

            return z;
        }

        public static Matrix operator -(Matrix x, Matrix y)
        {
            Matrix z = new Matrix(size(x));

            if (length(y) == 1)
                for (int i = 0; i < length(x); i++)
                    z[i] = x[i] - y[0];

            else if (length(y) == length(x))
                for (int i = 0; i < length(x); i++)
                    z[i] = x[i] - y[i];

            else
                throw new ArgumentException("Error using  - \n Matrix dimensions must agree");

            return z;
        }


        public static Matrix operator *(Matrix x, Matrix y)
        {
            Matrix z = new Matrix(size(x));

            if (length(y) == 1)
                for (int i = 0; i < length(x); i++)
                    z[i] = x[i] * y[0];

            else if (length(y) == length(x))
                for (int i = 0; i < length(x); i++)
                    z[i] = x[i] * y[i];

            else
                throw new ArgumentException("Error using  * \n Matrix dimensions must agree");

            return z;
        }


        public static Matrix operator /(Matrix x, Matrix y)
        {
            Matrix z = new Matrix(size(x));

            if (length(y) == 1)
                for (int i = 0; i < length(x); i++)
                    z[i] = x[i] / y[0];

            else if (length(y) == length(x))
                for (int i = 0; i < length(x); i++)
                    z[i] = x[i] / y[i];

            else
                throw new ArgumentException("Error using  / \n Matrix dimensions must agree");

            return z;
        }

        public static Matrix operator >(Matrix x, Matrix y)
        {
            Matrix z = new Matrix(size(x));

            if (length(y) == 1)
                for (int i = 0; i < length(x); i++)
                    z[i] = (x[i] > y[0]) ? 1 : 0;

            else if (length(y) == length(x))
                for (int i = 0; i < length(x); i++)
                    z[i] = (x[i] > y[i]) ? 1 : 0;

            else
                throw new ArgumentException("Error using  > \n Matrix dimensions must agree");

            return z;
        }

        public static Matrix operator >=(Matrix x, Matrix y)
        {
            Matrix z = new Matrix(size(x));

            if (length(y) == 1)
                for (int i = 0; i < length(x); i++)
                    z[i] = (x[i] >= y[0]) ? 1 : 0;

            else if (length(y) == length(x))
                for (int i = 0; i < length(x); i++)
                    z[i] = (x[i] >= y[i]) ? 1 : 0;

            else
                throw new ArgumentException("Error using  >= \n Matrix dimensions must agree");

            return z;
        }


        public static Matrix operator <(Matrix x, Matrix y)
        {
            Matrix z = new Matrix(size(x));

            if (length(y) == 1)
                for (int i = 0; i < length(x); i++)
                    z[i] = (x[i] < y[0]) ? 1 : 0;

            else if (length(y) == length(x))
                for (int i = 0; i < length(x); i++)
                    z[i] = (x[i] < y[i]) ? 1 : 0;

            else
                throw new ArgumentException("Error using  < \n Matrix dimensions must agree");

            return z;
        }

        public static Matrix operator <=(Matrix x, Matrix y)
        {
            Matrix z = new Matrix(size(x));

            if (length(y) == 1)
                for (int i = 0; i < length(x); i++)
                    z[i] = (x[i] <= y[0]) ? 1 : 0;

            else if (length(y) == length(x))
                for (int i = 0; i < length(x); i++)
                    z[i] = (x[i] <= y[i]) ? 1 : 0;

            else
                throw new ArgumentException("Error using  <= \n Matrix dimensions must agree");

            return z;
        }

        public static Matrix operator ==(Matrix x, Matrix y)
        {
            Matrix z = new Matrix(size(x));

            if (length(y) == 1)
                for (int i = 0; i < length(x); i++)
                    z[i] = (x[i] == y[0]) ? 1 : 0;

            else if (length(y) == length(x))
                for (int i = 0; i < length(x); i++)
                    z[i] = (x[i] == y[i]) ? 1 : 0;

            else
                throw new ArgumentException("Error using  == \n Matrix dimensions must agree");

            return z;
        }

        public static Matrix operator !=(Matrix x, Matrix y)
        {
            Matrix z = new Matrix(size(x));

            if (length(y) == 1)
                for (int i = 0; i < length(x); i++)
                    z[i] = (x[i] != y[0]) ? 1 : 0;

            else if (length(y) == length(x))
                for (int i = 0; i < length(x); i++)
                    z[i] = (x[i] != y[i]) ? 1 : 0;

            else
                throw new ArgumentException("Error using  != \n Matrix dimensions must agree");

            return z;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        

        public Matrix this[Matrix idx]
        {
            get
            {
                Matrix x = new Matrix(size(idx));
                for (int i = 0; i < numel(idx); i++)
                    x[i] = this[idx[i]];
                return x;
            }
            set
            {
                Matrix x = new Matrix(size(idx));
                for (int i = 0; i < numel(idx); i++)
                    this[idx[i]] = value[i];
            }
        }


        public double this [double i]
        {   
            get 
            {   
                return this[(int)i];
            }
            set 
            {   
                this[(int)i] = value;
            }
        }

        public double this[int i]
        {
            get
            {
                if ((i < length(this)) && (i >= 0))
                    return _Data[i];
                else throw new ArgumentException("Index exceeds matrix dimensions.");
            }
            set
            {
                if ((i < length(this)) && (i >= 0))
                    _Data[i] = value;
                else throw new ArgumentException("Index exceeds matrix dimensions.");
            }
        }

        public double this[int row, int col]
        {
            get
            {
                if ((row < _Rows) && (row >= 0))
                    if ((col < _Cols) && (col >= 0))
                        return _Data[row * col];
                    else
                        throw new ArgumentException("Index exceeds matrix dimensions.");
                else throw new ArgumentException("Index exceeds matrix dimensions.");
            }
            set
            {
                if ((row < _Rows) && (row >= 0))
                    if ((col < _Cols) && (col >= 0))
                        _Data[row * col] = value;
                    else
                        throw new ArgumentException("Index exceeds matrix dimensions.");
                else throw new ArgumentException("Index exceeds matrix dimensions.");
            }
        }


        public static implicit operator Matrix(int f)  // int to Matrix conversion operator
        {
            Matrix m = new Matrix(1, 1);  // explicit conversion
            m[0] = f;

            return m;
        }

        public static implicit operator Matrix(double f)  // double to Matrix conversion operator
        {
            Matrix m = new Matrix(1, 1);  // explicit conversion
            m[0] = f;

            return m;
        }

        public static implicit operator Matrix(double[] f)  // double[] to Matrix conversion operator
        {
            Matrix m = new Matrix(f.Length, 1);  // explicit conversion
            for (int i = 0; i < f.Length; i++)
                m[i] = f[i];

            return m;
        }

        public static implicit operator Matrix(double[,] f)  // double[,] to Matrix conversion operator
        {
            int dim0 = f.GetLength(0);
            int dim1 = f.GetLength(1);
            Matrix m = new Matrix(dim0, dim1);  // explicit conversion

            int k = 0;
            for (int i = 0; i < dim0; i++)
                for (int j = 0; j < dim1; j++)
                    m[k++] = f[i, j];

            return m;
        }

        public static explicit operator double[](Matrix m)  // Matrix to double[] conversion operator
        {
            return m._Data;
        }

        public Matrix Clone()
        {
            Matrix r = new Matrix(size(this));
            for (int i = 0; i < length(this); i++)
                r[i] = this[i];

            return r;
        }


        double mean(Matrix x)
        {
            double m = 0;

            for (int i = 0; i < length(x); i++)
                m += x[i];

            return (m / length(x));
        }
    }
}
