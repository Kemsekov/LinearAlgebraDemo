using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinearAlgebra;
public interface IVector<T>
{
    ref T this[int index]{get;}    
    int Length{get;}
    double L2Norm();
    IVector<T> Sum(IVector<T> another);
    IVector<T> Sub(IVector<T> another);
}