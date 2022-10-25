using System;
using System.Linq;
using LinearAlgebra;
using LinearAlgebra.Exceptions;
using Xunit;

namespace Matrix.Tests;

public class SquareMatrixIntTests
{
    [Fact]
    public void Constructor1_Test()
    {
        var m = new SquareMatrixInt(10, 2);
        Assert.Equal(m.Size, 10);
        Assert.Equal(m.Width, 10);
        Assert.Equal(m.Height, 10);
        foreach (var el in m)
        {
            Assert.Equal(el.Value, 2);
        }
    }
    [Fact]
    public void Constructor2_Test()
    {
        var m = new SquareMatrixInt(new int[] { 1, 2, 3, 4 });
        Assert.Equal(m.Size, 2);
        Assert.Equal(m.Width, 2);
        Assert.Equal(m.Height, 2);
        int value = 1;
        foreach (var el in m)
        {
            Assert.Equal(el.Value, value++);
        }
        //ensure that we cannot init matrix with array of wrong size
        Assert.Throws<WrongDataForSquareMatrixException>(
            () => new SquareMatrixInt(new int[] { 1, 2 }));
        
        Assert.Throws<WrongDataForSquareMatrixException>(
              () => new SquareMatrixInt(new int[] { 1, 2, 3, 4, 5 }));
    }
    [Fact]
    public void Determinant_Test(){
        var m = new SquareMatrixInt(new int[] { 1, 2, 3, 4 });
        Assert.Equal(-2,m.Determinant());
        m = new SquareMatrixInt(new int[] { 1, 2, 3, 24, 5, 6, 7, 8, 10 });
        Assert.Equal(77,(int)m);
        m = new SquareMatrixInt(Enumerable.Range(1,16).ToArray());
        Assert.Equal(0,(int)m);
    }
    [Fact]
    public void Add_Test(){
        var m1 = new SquareMatrixInt(Enumerable.Range(1,9).OrderBy(n=>Random.Shared.Next()).ToArray());
        var m2 = new SquareMatrixInt(Enumerable.Range(1,9).OrderBy(n=>Random.Shared.Next()).ToArray());
        var m3 = m1 + m2;
        foreach(var el in m3){
            Assert.Equal(m1[el.X,el.Y]+m2[el.X,el.Y],el.Value);
        }
        m2 = new SquareMatrixInt(Enumerable.Range(1,16).ToArray());
        Assert.Throws<ArgumentException>(()=>(m1+m2));
    }
    [Fact]
    public void Sub_Test(){
        var m1 = new SquareMatrixInt(Enumerable.Range(1,9).OrderBy(n=>Random.Shared.Next()).ToArray());
        var m2 = new SquareMatrixInt(Enumerable.Range(1,9).OrderBy(n=>Random.Shared.Next()).ToArray());
        var m3 = m2-m1;
        foreach(var el in m3){
            Assert.Equal(m2[el.X,el.Y]-m1[el.X,el.Y],el.Value);
        }
        m2 = new SquareMatrixInt(Enumerable.Range(1,16).ToArray());
        Assert.Throws<ArgumentException>(()=>(m1-m2));
    }
    [Fact]
    public void Transpose_Test(){
        var m1 = new SquareMatrixInt(Enumerable.Range(1,9).ToArray());
        var m2 = m1.Transpose();
        Assert.NotEqual(m1,m2);
        Assert.Equal(m1,m2.Transpose());
        foreach(var el in m1.Transpose()){
            Assert.Equal(el.Value,m1[el.Y,el.X]);
        }
    }
    [Fact]
    public void Mul_Test(){
        var m1 = new SquareMatrixInt(new int[]{1,3,2,3});
        var m2 = new SquareMatrixInt(new int[]{4,3,1,2});
        var m3 = m1*m2;
        var expected = new SquareMatrixInt(new int[]{10,21,5,9});
        Assert.Equal(expected,m3);
        m1 = new SquareMatrixInt(Enumerable.Range(1,9).ToArray());
        m2 = new SquareMatrixInt(Enumerable.Range(2,9).ToArray());
        m3 = m1*m2;
        expected = new SquareMatrixInt(new int[]{42,51,60,78,96,114,114,141,168});
        Assert.Equal(expected,m3);
    }
    [Fact]
    public void MulScalar_Test(){
        var m1 = new SquareMatrixInt(Enumerable.Range(1,9).ToArray());
        var m2 = m1*5;
        foreach(var el in m2){
            Assert.Equal(m1[el.X,el.Y]*5,el.Value);
        }
    }
    
}