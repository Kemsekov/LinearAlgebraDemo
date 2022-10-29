using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinearAlgebra;
/// <summary>
/// Two dimensional matrix 
/// </summary>
public interface IMatrix<T>
{
    int Width{get;}
    int Height{get;}
    ref T this[int x, int y]{get;}
    IMatrix<T> Add(IMatrix<T> m);
    IMatrix<T> Sub(IMatrix<T> m);
    IMatrix<T> Mul(T scalar);
    IVector<T> Mul(IVector<T> vector);
    IMatrix<T> Mul(IMatrix<T> m);
    IMatrix<T> Transpose();
    T Determinant();
    bool Equals(IMatrix<T> m);
}