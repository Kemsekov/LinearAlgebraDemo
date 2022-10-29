using LinearAlgebra;

var A = new SquareMatrixInt(new int[]{1,2,3,4});
var B = new SquareMatrixInt(new int[]{5,4,3,2});
var C = new SquareMatrixInt(new int[]{9,1,1,0});
var D = new SquareMatrixInt(new int[]{3,2,1,1});

var result = !A+B*4-!C*5;
System.Console.WriteLine(result);
