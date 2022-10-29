using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinearAlgebra;
/// <summary>
/// Represents a vector with <see langword="int"/> field
/// </summary>
public class IntVector : IVector<int>
{
    int[] Data;
    public ref int this[int index] => ref Data[index];
    /// <summary>
    /// Count of vector elements
    /// </summary>
    public int Length => Data.Length;
    /// <summary>
    /// Initialize new <see cref="IntVector"/> with <paramref name="data"/> array of type <see langword="int"/>
    /// </summary>
    public IntVector(int[] data)
    {
        Data = data;
    }
    /// <summary>
    /// Initialize new <see cref="IntVector"/> of some <paramref name="length"/> and some <paramref name="initialValue"/>
    /// </summary>
    public IntVector(int length, int initialValue = 0){
        Data = new int[length];
        if(initialValue!=0)
            Array.Fill(Data,initialValue);
    }
    /// <summary>
    /// Computes euclidean norm.
    /// </summary>
    public double L2Norm()
    {
        double result = 0;
        for(int i = 0;i<Data.Length;i++){
            result+=Data[i]*Data[i];
        }
        return Math.Sqrt(result);
    }

    /// <summary>
    /// Sums of current vector and some other vector
    /// </summary>
    /// <returns>New vector which equals to sum of two previous vectors</returns>
    public IntVector Sum(IVector<int> another)
    {
        if(another.Length!=Length)
            throw new ArgumentException($"Cannot sum vectors with different size. {Length} != {another.Length}");
        var result = new IntVector(Length);
        for(int i = 0;i<Length;i++){
            result[i] = this[i]+another[i];
        }
        return result;
    }
    IVector<int> IVector<int>.Sum(IVector<int> another) => Sum(another);
    /// <summary>
    /// Subs of current vector and some other vector
    /// </summary>
    /// <returns>New vector which equals to sub of two previous vectors</returns>
    public IntVector Sub(IVector<int> another)
    {
        if(another.Length!=Length)
            throw new ArgumentException($"Cannot sum vectors with different size. {Length} != {another.Length}");
        var result = new IntVector(Length);
        for(int i = 0;i<Length;i++){
            result[i] = this[i]-another[i];
        }
        return result;
    }
    IVector<int> IVector<int>.Sub(IVector<int> another) => Sub(another);
    public static IntVector operator +(IntVector v1, IVector<int> v2) => v1.Sum(v2);
    public static IntVector operator -(IntVector v1) => new IntVector(v1.Length,0).Sub(v1);
    public static IntVector operator -(IntVector v1, IVector<int> v2) => v1.Sub(v2);
    public static explicit operator double(IntVector v) => v.L2Norm();
}