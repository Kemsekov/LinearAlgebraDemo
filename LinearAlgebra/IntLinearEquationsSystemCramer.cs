using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinearAlgebra;
/// <summary>
/// Represents a system of linear equations (SoLE) for systems with int fields.<br/>
/// Can be used to get solutions to SoLE by Cramers.
/// </summary>
public class IntLinearEquationsSystemCramer : ILinearEquationsSystem<int>
{
    public IMatrix<int> Coefficients{get;protected set;}
    public IVector<int> FreeCoefficients{get;protected set;}
    public IntLinearEquationsSystemCramer(IMatrix<int> coefficients, IVector<int> freeCoefficients)
    {
        Coefficients = coefficients;
        FreeCoefficients = freeCoefficients;
        SetSystem(Coefficients,FreeCoefficients);
    }
    void Swap(ref int a, ref int b){
        a=a+b;
        b=a-b;
        a=a-b;
    }
    void SwapCoefficientsVectors(IMatrix<int> coefficients ,IVector<int> vector, int position){
        for(int y = 0;y<coefficients.Height;y++)
            Swap(ref coefficients[position,y],ref vector[y]);
    }

    public SolutionsResult ComputeSolution(out double[] result)
    {
        
        result = new double[Coefficients.Width];
        var det = Coefficients.Determinant();
        if(det==0)
            return SolutionsResult.None;
        
        for(int i = 0;i<result.Length;i++){
            SwapCoefficientsVectors(Coefficients,FreeCoefficients,i);
            var iDet = Coefficients.Determinant();
            result[i] = iDet*1.0/det;
            SwapCoefficientsVectors(Coefficients,FreeCoefficients,i);
        }
        return SolutionsResult.One;
    }

    public void SetSystem(IMatrix<int> coefficients, IVector<int> freeCoefficients)
    {
        Coefficients = coefficients;
        FreeCoefficients = freeCoefficients;
        if(Coefficients.Width!=Coefficients.Height)
            throw new ArgumentException("Cannot use non-square matrix in system of linear equations");
    }
}