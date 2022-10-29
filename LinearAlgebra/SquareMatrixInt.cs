using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinearAlgebra.Exceptions;

namespace LinearAlgebra;
/// <summary>
/// Represents square matrix on integer field
/// </summary>
public class SquareMatrixInt : IMatrix<int>, IEnumerable<(int X, int Y, int Value)>
{
    /// <summary>
    /// Matrix size
    /// </summary>
    public int Size { get; }
    public int Width => Size;
    public int Height => Size;
    int[] Data;
    public ref int this[int x, int y]
    {
        get => ref Data[x * Size + y];
    }
    public ref int At(int x, int y) => ref Data[x * Size + y];
    /// <summary>
    /// Creates a new instance of <see cref="SquareMatrixInt"/> with given <paramref name="size"/> and <paramref name="initialValue"/>
    /// </summary>
    /// <param name="size"></param>
    public SquareMatrixInt(int size, int initialValue = 0)
    {
        Size = size;
        Data = new int[size * size];
        if (initialValue != 0)
            Array.Fill(Data, initialValue);
    }
    /// <summary>
    /// Creates a new instance of <see cref="SquareMatrixInt"/> from <see langword="int"/> array.
    /// </summary>
    /// <exception cref="WrongDataForSquareMatrixException">Thrown when <paramref name="data"/> is of wrong size and cannot be used for square matrix.</exception>
    public SquareMatrixInt(int[] data)
    {
        var size = (int)MathF.Sqrt(data.Length);
        if (size * size != data.Length) throw new WrongDataForSquareMatrixException(data.Length);
        Size = (int)size;
        Data = data;
    }

    void ThrowIfWrongSize(int width, int height)
    {
        if (width != Size || height != Size)
            throw new ArgumentException("Cannot add/sub two matrices of different size");
    }

    /// <summary>
    /// Computes sub matrix which size is one smaller than <see cref="Size"/> 
    /// and do not contain elements with 
    /// <paramref name="x"/> as X coordinate or <paramref name="y"/> as Y coordinate. <br/>
    /// In other words returns matrix minor of some coordinates.<br/>
    /// </summary>
    /// <returns>New matrix which is minor of some coordinates</returns>
    public SquareMatrixInt Minor(int x, int y){
        var size = Size-1;
        var m = new SquareMatrixInt(size);
        int count = 0;
        foreach(var el in this){
            if(el.X==x || el.Y==y) continue;
            m[count/size,count%size] = el.Value;
            count++;
        }
        return m;
    }

    /// <summary>
    /// Computes a determinant of matrix
    /// </summary>
    /// <returns>Determinant value</returns>
    public int Determinant()
    {
        if(Size==1) return this[0,0];
        if(Size==2) return Determinant2x2(this);
        if(Size==3) return Determinant3x3(this);
        int result = 0;
        for(int x = 0;x<Size;x++){
            if(x%2!=0)
                result += this[x,0]*Minor(x,0).Determinant();
            else
                result -= this[x,0]*Minor(x,0).Determinant();
        }
        return result;
    }
    // Computes determinant for 3x3 matrix
    int Determinant3x3(IMatrix<int> m)
    {
        int result = 0;
        int temp = 0;
        for (int i = 0; i < Size; i++)
        {
            temp = 1;
            for(int j = 0;j<Size;j++){
                temp*=m[(i+j)%Size,j];
            }
            result+=temp;
            temp = 1;
            for(int j = 0;j<Size;j++){
                temp*=m[(i-j+Size)%Size,j];
            }
            result-=temp;
        }

        return result;
    }
    // Computes determinant of 2x2 matrix
    int Determinant2x2(IMatrix<int> m){
        return m[0,0]*m[1,1]-m[0,1]*m[1,0];
    }
    /// <summary>
    /// Adds two square matrices
    /// </summary>
    /// <returns>New matrix which is sum of two matrices</returns>
    public SquareMatrixInt Add(IMatrix<int> m)
    {
        ThrowIfWrongSize(m.Width, m.Height);
        var result = new SquareMatrixInt(Size);
        for (int i = 0; i < Data.Length; i++)
        {
            result.Data[i] = Data[i] + m[i / Size, i % Size];
        }
        return result;
    }
    IMatrix<int> IMatrix<int>.Add(IMatrix<int> m) => Add(m);
    /// <summary>
    /// Subs two square matrices
    /// </summary>
    /// <returns>New matrix which is sub of two matrices</returns>
    public SquareMatrixInt Sub(IMatrix<int> m)
    {
        ThrowIfWrongSize(m.Width, m.Height);
        var result = new SquareMatrixInt(Size);
        for (int i = 0; i < Data.Length; i++)
        {
            result.Data[i] = Data[i] - m[i / Size, i % Size];
        }
        return result;
    }
    IMatrix<int> IMatrix<int>.Sub(IMatrix<int> m) => Sub(m);
    void Swap(ref int a, ref int b)
    {
        a = a + b;
        b = a - b;
        a = a - b;
    }
    /// <summary>
    /// Compute matrix transpose
    /// </summary>
    /// <returns>New transposed matrix</returns>
    public SquareMatrixInt Transpose()
    {
        var result = new SquareMatrixInt(Size);
        Array.Copy(Data, result.Data, Data.Length);
        for (int x = 0; x < Size; x++)
            for (int y = x; y < Size; y++)
            {
                if (x == y) continue;
                Swap(ref result.At(x, y), ref result.At(y, x));
            }
        return result;
    }
    IMatrix<int> IMatrix<int>.Transpose() => Transpose();
    /// <summary>
    /// Mul matrix with scalar
    /// </summary>
    /// <returns>New matrix which is mul of matrix with scalar</returns>
    public SquareMatrixInt Mul(int scalar)
    {
        var result = new SquareMatrixInt(Size);
        Array.Copy(Data, result.Data, Data.Length);
        for (int i = 0; i < Data.Length; i++)
            result.Data[i] *= scalar;

        return result;
    }
    IMatrix<int> IMatrix<int>.Mul(int scalar) => Mul(scalar);

    /// <summary>
    /// Multiply matrix with a matrix
    /// </summary>
    /// <returns>New matrix which is a multiple of matrix with another matrix</returns>
    public SquareMatrixInt Mul(IMatrix<int> m)
    {
        if (Size != m.Width || Size != m.Height)
        {
            throw new ArgumentException("SquareMatrixInt cannot be multiplied with not square matrix or matrix of different size");
        }
        var result = new SquareMatrixInt(Size);
        int toInsert = 0;
        for (int x = 0; x < Size; x++)
            for (int y = 0; y < Size; y++)
            {
                toInsert = 0;
                for (int i = 0; i < Size; i++)
                {
                    toInsert += this[i, y] * m[x, i];
                }
                result[x, y] = toInsert;
            }
        return result;
    }
    IMatrix<int> IMatrix<int>.Mul(IMatrix<int> m) => Mul(m);
    /// <summary>
    /// Multiplies matrix and a vector.
    /// </summary>
    /// <returns>New vector as result of multiplication</returns>
    public IVector<int> Mul(IVector<int> vector)
    {
        if(vector.Length!=Size)
            throw new ArgumentException($"Wrong vector size. Cannot multiply {vector.Length} vector with {Size} square matrix");
        var result = new int[vector.Length];
        int temp;
        for(int i = 0;i<vector.Length;i++){
            temp = 0;
            for(int x = 0;x<vector.Length;x++){
                temp += this[x,i]*vector[x];
            }
            result[i] = temp;
        }
        return new IntVector(result);
    }
    /// <returns>
    /// <see langword="true"/> if matrix <paramref name="m"/> 
    /// is element-vice equal to <see langword="this"/> matrix, 
    /// else <see langword="false"/>.
    /// </returns>
    public bool Equals(IMatrix<int> m)
    {
        if (m.Width != Size || m.Height != Size) return false;
        for (int x = 0; x < Size; x++)
            for (int y = 0; y < Size; y++)
            {
                if (this[x, y] != m[x, y]) return false;
            }
        return true;
    }
    public override bool Equals(object? obj)
    {
        if(obj is IMatrix<int> m)
            return Equals(m);
        return false;
    }
    public override int GetHashCode()
    {
        //our matrix defined by it's data so we just take data's hash code
        return Data.GetHashCode();
    }

    public static SquareMatrixInt operator +(SquareMatrixInt m1, SquareMatrixInt m2)
        => m1.Add(m2);
    public static SquareMatrixInt operator -(SquareMatrixInt m1, SquareMatrixInt m2)
        => m1.Sub(m2);
    public static SquareMatrixInt operator *(SquareMatrixInt m1, SquareMatrixInt m2)
        => m1.Mul(m2);
    public static SquareMatrixInt operator *(SquareMatrixInt m1, int scalar)
        => m1.Mul(scalar);
    public static IVector<int> operator *(SquareMatrixInt m1, IVector<int> vector)
        => m1.Mul(vector);
    public static SquareMatrixInt operator !(SquareMatrixInt m)
        => m.Transpose();

    public static explicit operator int(SquareMatrixInt m)
        => m.Determinant();

    public static bool operator >(SquareMatrixInt m1, SquareMatrixInt m2)
    {
        return m1.Determinant() > m2.Determinant();
    }
    public static bool operator <(SquareMatrixInt m1, SquareMatrixInt m2)
    {
        return m1.Determinant() < m2.Determinant();
    }
    public static bool operator <=(SquareMatrixInt m1, SquareMatrixInt m2)
    {
        return m1.Determinant() <= m2.Determinant();
    }
    public static bool operator >=(SquareMatrixInt m1, SquareMatrixInt m2)
    {
        return m1.Determinant() >= m2.Determinant();
    }
    public static bool operator ==(SquareMatrixInt m1, SquareMatrixInt m2) => m1.Equals(m2);
    public static bool operator !=(SquareMatrixInt m1, SquareMatrixInt m2) => !m1.Equals(m2);
    /// <summary>
    /// Enumerates matrix values
    /// </summary>
    /// <returns>
    /// Enumerator with <paramref name="x"/> and <paramref name="y"/> 
    /// coordinates of element and it's <paramref name="value"/>
    /// </returns>
    public IEnumerator<(int X, int Y, int Value)> GetEnumerator()
    {
        for (int x = 0; x < Size; x++)
            for (int y = 0; y < Size; y++)
                yield return (x, y, this[x, y]);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public override string ToString()
    {
        var builder = new StringBuilder();
        for(int y = 0;y<Size;y++, builder.Append('\n'))
        for(int x = 0;x<Size;x++)
            builder.Append($"{this[x,y]}\t");
        return builder.ToString();
    }


}