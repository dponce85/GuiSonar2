using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiSonar2
{
    class Matrix
    {
        private double[] _Data;
        private int[] _Size;
        private int _Numel;

        public Matrix(Matrix size)
        {
            init(size);
        }

        public Matrix(params int[] size)
        {
            init(size);
        }

        private void init(int[] size)
        {
            if (size.Length == 0)
                throw new ArgumentException("Invalid number of dimensions");

            _Size = size;
            _Numel = getNumel(size);
            _Data = new double[_Numel];
        }

        private int getNumel(int[] size)
        {
            int numel = 1;
            for (int i = 0; i < size.Length; i++)
                numel *= size[i];

            if (numel < 0)
                throw new ArgumentException("Size must be an array of positive numbers");

            return numel;
        }

        public int Length
        { 
            get
            {
                return _Size.Max();
            }            
        }

        public int Numel
        {
            get
            {
                return _Numel;
            }
        }

        public int Ndims
        {
            get 
            {
                return _Size.Length;
            }
        }


        private static bool IsScalar(Matrix x)
        {
            // Check for scalars
            return (x.Numel == 1);                
        }

        private static bool CheckDims(Matrix x, Matrix y)
        {
            // Check dims
            if (x.Ndims != y.Ndims)
                return false;

            // Check size
            Matrix dS = (x.Size.Equal(y.Size));
            if (dS.Sum != x.Ndims)
                return false;

            return true;
        }
 







        public void Add(Matrix y)
        {
            // Check for scalars
            if (IsScalar(y))
            {
                double y0 = y[0];
                for (int i = 0; i < this.Numel; i++)
                    this[i] += y0;
            }
            else
            {
                // Check for identical dimensions and sizes
                if (!CheckDims(this, y))
                    throw new ArgumentException("Error using  Add \n Matrix dimensions must agree");

                // Cleared!
                for (int i = 0; i < this.Numel; i++)
                    this[i] += y[i];
            }
        }

        public static Matrix operator +(Matrix x, Matrix y)
        {
            Matrix z;
            if (IsScalar(y))
            { 
                z = x.Clone();
            }
            else if (IsScalar(x))
            {
                z = y.Clone();
            }
            else
            {

            }

            
            z.Add(y);
            return z;
        }








        public void Substract(Matrix y)
        {
            // Check for scalars
            if (IsScalar(y))
            {
                double y0 = y[0];
                for (int i = 0; i < this.Numel; i++)
                    this[i] -= y0;
            }
            else
            {
                // Check for identical dimensions and sizes
                if (!CheckDims(this, y))
                    throw new ArgumentException("Error using  Substract \n Matrix dimensions must agree");

                // Cleared!
                for (int i = 0; i < this.Numel; i++)
                    this[i] -= y[i];
            }
        }




       


        public void Product(Matrix y)
        {
            // Check for scalars
            if (IsScalar(y))
            {
                double y0 = y[0];
                for (int i = 0; i < this.Numel; i++)
                    this[i] *= y0;
            }
            else
            {
                // Check for identical dimensions and sizes
                if (!CheckDims(this, y))
                    throw new ArgumentException("Error using  Product \n Matrix dimensions must agree");

                // Cleared!
                for (int i = 0; i < this.Numel; i++)
                    this[i] *= y[i];
            }
        }






        public void Divide(Matrix y)
        {
            // Check for scalars
            if (IsScalar(y))
            {
                double y0 = y[0];
                for (int i = 0; i < this.Numel; i++)
                    this[i] /= y0;
            }
            else
            {
                // Check for identical dimensions and sizes
                if (!CheckDims(this, y))
                    throw new ArgumentException("Error using  Divide \n Matrix dimensions must agree");

                // Cleared!
                for (int i = 0; i < this.Numel; i++)
                    this[i] /= y[i];
            }
        }






        public void GreaterThan(Matrix y)
        {
            // Check for scalars
            if (IsScalar(y))
            {
                double y0 = y[0];
                for (int i = 0; i < this.Numel; i++)
                    this[i] = (this[i] > y0) ? 1 : 0;
            }
            else
            {
                // Check for identical dimensions and sizes
                if (!CheckDims(this, y))
                    throw new ArgumentException("Error using  GreaterThan \n Matrix dimensions must agree");

                // Cleared!
                for (int i = 0; i < this.Numel; i++)
                    this[i] = (this[i] > y[i]) ? 1 : 0;
            }
        }






        public void GreaterEqualThan(Matrix y)
        {
            // Check for scalars
            if (IsScalar(y))
            {
                double y0 = y[0];
                for (int i = 0; i < this.Numel; i++)
                    this[i] = (this[i] >= y0) ? 1 : 0;
            }
            else
            {
                // Check for identical dimensions and sizes
                if (!CheckDims(this, y))
                    throw new ArgumentException("Error using  GreaterEqualThan \n Matrix dimensions must agree");

                // Cleared!
                for (int i = 0; i < this.Numel; i++)
                    this[i] = (this[i] >= y[i]) ? 1 : 0;
            }
        }







        public void LessThan(Matrix y)
        {
            // Check for scalars
            if (IsScalar(y))
            {
                double y0 = y[0];
                for (int i = 0; i < this.Numel; i++)
                    this[i] = (this[i] < y0) ? 1 : 0;
            }
            else
            {
                // Check for identical dimensions and sizes
                if (!CheckDims(this, y))
                    throw new ArgumentException("Error using  LessThan \n Matrix dimensions must agree");

                // Cleared!
                for (int i = 0; i < this.Numel; i++)
                    this[i] = (this[i] < y[i]) ? 1 : 0;
            }
        }




        public void LessEqualThan(Matrix y)
        {
            // Check for scalars
            if (IsScalar(y))
            {
                double y0 = y[0];
                for (int i = 0; i < this.Numel; i++)
                    this[i] = (this[i] <= y0) ? 1 : 0;
            }
            else
            {
                // Check for identical dimensions and sizes
                if (!CheckDims(this, y))
                    throw new ArgumentException("Error using  LessEqualThan \n Matrix dimensions must agree");

                // Cleared!
                for (int i = 0; i < this.Numel; i++)
                    this[i] = (this[i] <= y[i]) ? 1 : 0;
            }
        }





        public void Equal(Matrix y)
        {
            // Check for scalars
            if (IsScalar(y))
            {
                double y0 = y[0];
                for (int i = 0; i < this.Numel; i++)
                    this[i] = (this[i] == y0) ? 1 : 0;
            }
            else
            {
                // Check for identical dimensions and sizes
                if (!CheckDims(this, y))
                    throw new ArgumentException("Error using  LessEqualThan \n Matrix dimensions must agree");

                // Cleared!
                for (int i = 0; i < this.Numel; i++)
                    this[i] = (this[i] == y[i]) ? 1 : 0;
            }
        }





        public void NotEqual(Matrix y)
        {
            // Check for scalars
            if (IsScalar(y))
            {
                double y0 = y[0];
                for (int i = 0; i < this.Numel; i++)
                    this[i] = (this[i] != y0) ? 1 : 0;
            }
            else
            {
                // Check for identical dimensions and sizes
                if (!CheckDims(this, y))
                    throw new ArgumentException("Error using  LessEqualThan \n Matrix dimensions must agree");

                // Cleared!
                for (int i = 0; i < this.Numel; i++)
                    this[i] = (this[i] != y[i]) ? 1 : 0;
            }
        }



        public double Sum
        {
            get
            {
                double ret = 0;
                for (int i = 0; i < this.Numel; i++)
                    ret += this[i];

                return ret;
            }
        }

        public double Mean
        {
            get
            {
                return this.Sum / this.Numel;
            }
        }



        public Matrix this[Matrix idx]
        {
            get
            {
                Matrix x = new Matrix(idx.Size);
                for (int i = 0; i < idx.Numel; i++)
                    x[i] = this[idx[i]];
                return x;
            }
            set
            {
                Matrix x = new Matrix(idx.Size);
                for (int i = 0; i < idx.Numel; i++)
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
                if ((i < this.Length) && (i >= 0))
                    return _Data[i];
                else throw new ArgumentException("Index exceeds matrix dimensions.");
            }
            set
            {
                if ((i < this.Length) && (i >= 0))
                    _Data[i] = value;
                else throw new ArgumentException("Index exceeds matrix dimensions.");
            }
        }

        public double this[params int[] args]
        {
            get
            {
                for (int i = 0; i < args.Length; i++)
                    if (args[i] > this.Size[i])                        
                        throw new ArgumentException("Index exceeds matrix dimensions.");

                return _Data[indexToAddress(args)];                    
            }
            set
            {
                for (int i = 0; i < args.Length; i++)
                    if (args[i] > this.Size[i])                        
                        throw new ArgumentException("Index exceeds matrix dimensions.");

                _Data[indexToAddress(args)] = value;
            }
        }

        private int indexToAddress(int[] index)
        {
            int address = 0;            
            for (int i = 0; i < index.Length; i++)
            {
                int prod = 1;
                for (int j = 0; j < i - 1; j++)
                    prod = prod * this._Size[j];

                address += index[i] * prod;
            }

            return address;
        }

        private int cumsum(int[] arg)
        { 
            int sum = 0;
            for (int i = 0; i < arg.Length; i++)
                sum += arg[i];

            return sum;
        }

        private int cumprod(int[] arg)
        {
            int prod = arg[0];
            for (int i = 1; i < arg.Length; i++)
                prod *= arg[i];

            return prod;
        }

        public Matrix Size
        {   
            get
            {   
                return this._Size;
            }
        }

        public Matrix Clone()
        {
            Matrix r = new Matrix(this.Size);
            for (int i = 0; i < this.Length; i++)
                r[i] = this[i];

            return r;
        }


        public static implicit operator Matrix(int f)  // int to Matrix conversion operator
        {
            Matrix m = new Matrix(1, 1);  // explicit conversion
            m[0] = f;

            return m;
        }

        public static implicit operator int[](Matrix m)  // Matrix to int[] conversion operator
        {
            if (m._Size.Length > 1)
                throw new ArgumentException("Array dimensions must agree.");

            int[] r = new int[m.Numel];
            for (int i = 0; i < r.Length; i++)
                r[i] = (int)m[i];

            return r;
        }


        public static implicit operator Matrix (int[] f)  // Matrix to int[] conversion operator
        {
            Matrix m = new Matrix(f.Length, 1);  // explicit conversion

            for (int i = 0; i < f.Length; i++)
                m[i] = f[i];

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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}