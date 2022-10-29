using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinearAlgebra;

namespace Playground;
public class MatrixSorter
{
    public IMatrix<int>[] Matrices { get; }
    public MatrixSorter(IEnumerable<IMatrix<int>> matrices) : this(matrices.ToArray())
    {
    }
    public MatrixSorter(IMatrix<int>[] matrices){
        this.Matrices = matrices;
    }
    public void Sort(){
        Array.Sort(Matrices,(m1,m2)=>m1.Determinant()-m2.Determinant());   
    }
    public override string ToString()
    {
        var str = new StringBuilder();
        foreach(var m in Matrices)
            str.Append(m.ToString()+'\n');
        return str.ToString();
    }
}