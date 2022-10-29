using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LinearAlgebra.Tests;

public class IntLinearEquationsSystemCramerTests
{
    [Fact]
    public void OneSolution_Test(){
        var m = new SquareMatrixInt(new int[]{1,2,1,1,0,2,0,1,1});
        var freeCoefficients = new IntVector(new int[]{5,4,7});
        var l = new IntLinearEquationsSystemCramer(m,freeCoefficients);
        var expectedResult = new double[]{2+1.0/3,2+2.0/3,-2.0/3};
        Assert.Equal(SolutionsResult.One, l.ComputeSolution(out var result));
        Assert.Equal(expectedResult,result);
    }
    [Fact]
    public void NoSolutions_Test(){
        var m = new SquareMatrixInt(new int[]{1, 1, 2, 1, 0, 0, 2, 3, 6});
        var freeCoefficients = new IntVector(new int[]{3,6,12});
        var l = new IntLinearEquationsSystemCramer(m,freeCoefficients);
        Assert.Equal(SolutionsResult.None,l.ComputeSolution(out var result));
    }
}
