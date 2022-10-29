using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinearAlgebra;
public interface ILinearEquationsSystem<T>
{
    IMatrix<T> Coefficients{get;}
    IVector<T> FreeCoefficients{get;}
    void SetSystem(IMatrix<T> coefficients, IVector<T> freeCoefficients);
    SolutionsResult ComputeSolution(out double[] results);
}