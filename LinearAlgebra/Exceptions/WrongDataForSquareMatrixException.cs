using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinearAlgebra.Exceptions;
public class WrongDataForSquareMatrixException : ArgumentException
{
    public WrongDataForSquareMatrixException(int wrongSize) 
    : base(WrongDataForSquareMatrixException.ExceptionMessage(wrongSize))
    {}
    public static string ExceptionMessage(int wrongSize){
        return $"Given array size {wrongSize} is not correct for square matrix.\nFor square matrix initialization array must be a size of square whole number.";
    }
}