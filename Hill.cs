using System.Collections.Generic;
using System.Linq;
using static System.Math;
using System;
namespace Lab_2
{
  public class Hill : ICipher
  {
    private string result;
    private void MatrixMultiply(List<int> Z,int[,] X, int[,] Y, int M, int N = 0)
    {
      if (N == 0) N = M;
      int sum = 0;
      for (int i = 0; i < N; i++)
      {
        for (int j = 0; j < M; j++)
        {
          for (int k = 0; k < M; k++)
          {
            sum += X[i, k] * Y[j,k];
          }
          Z.Add(sum % Algorithm.ABCSize);
          sum = 0;
        }
      }
    }
    public string Encode(string input, string key) {
      result = "";
      int sqrt = (int)Sqrt(key.Length);
      int rows;
      if (input.Length % sqrt == 0)
        rows = input.Length / sqrt;
      else
      {
        rows = (input.Length / sqrt) + 1;
      }
      int[,] X = new int[rows,sqrt];
      int[,] Y = new int[sqrt,sqrt];
      List<int> Z = new List<int>();
      List<string> tmp = new List<string>();
      ConvertToNumbers(ref X, ref Y, rows, sqrt, input, key);
      MatrixMultiply(Z, X, Y, sqrt, rows);
      for (int i = 0; i < Z.Count(); i++) 
         tmp.Add(Algorithm.ABC[Z[i]]);
      for (int i = 0; i < tmp.Count(); i++)
      {
         result += tmp[i];
      }
      Console.WriteLine("Получилась строка: {0}", result);
      return result;
    }
    private void ConvertToNumbers(ref int[,] X, ref int[,] Y, int rows, int sqrt, string input, string key) {
      for (int m = 0, k = 0; m < rows; m++)
      {
        for (int n = 0; n < sqrt; n++)
        {
          if (k >= input.Length)
            X[m, n] = Algorithm.ABCSize - 1;
          else
            X[m, n] = Algorithm.ABC.IndexOf(input[k].ToString());
          k++;
        }
      }
      for (int m = 0, k = 0; m < sqrt; m++)
      {
        for (int n = 0; n < sqrt; n++, k++)
        {
          Y[m, n] = Algorithm.ABC.IndexOf(key[k].ToString());
        }
      }
    }
    private int Det(int[,] matrix, int n) {
      int det=0;
      const int SINGLE_MATRIX = 1;
      if (n == SINGLE_MATRIX)
        return matrix[0, 0];
      else
      {
        for (int i = 0; i < n; i++)
        {
          int res = (int)Pow(-1, 1 + i + 1) * matrix[i,0] * Minor(matrix, n, i);
          det += res;
        }
        return det;
      }
    }
    private int EuclideanAlgorithm(int a, int b, ref int x, ref int y)
    {
      if (a == 0)
      {
        x = 0; y = 1;
        return b;
      }
      int x1=x, y1=y;
      int d = EuclideanAlgorithm(b % a, a, ref x1, ref y1);
      x = y1 - (b / a) * x1;
      y = x1;
      return d;
    }
    private int Minor(int[,] matrix, int n, int row, int column = 0) {
      int detMin=0;
      const int SINGLE_MATRIX = 1;
      const int ZERO_ROW = 0, ZERO_COLUMN = 0, FIRST_ROW = 1, FIRST_COLUMN = 1;
      if (n-1 == SINGLE_MATRIX) {
        switch(row){
          case ZERO_ROW: {
              switch(column) {
                case ZERO_COLUMN: {
                    return matrix[FIRST_ROW, FIRST_COLUMN]; // [строка;столбец]
                  }
                case FIRST_COLUMN: {
                    return matrix[FIRST_ROW, ZERO_COLUMN];
                  }
              }
              break;
            }
          case FIRST_ROW: {
              switch (column)
              {
                case ZERO_COLUMN:
                  {
                    return matrix[ZERO_ROW, FIRST_COLUMN];
                  }
                case FIRST_COLUMN:
                  {
                    return matrix[ZERO_ROW, ZERO_COLUMN];
                  }
              }
              break;
            }
        }
        return Algorithm.ABCSize;
      }    
      else
      {
        int[,] newMatrix = new int[n-1, n-1];
        for (int i = 0, k = 0; i < n; i++)
        {
          if (i == row)
            continue;
          else
          {
            for (int q = 0; q < n; q++)
            {
              if (q == column)
                continue;
              else
              {
                for (int j = 1; j < n; j++)
                  newMatrix[k, j - 1] = matrix[i, j];
                k++;
              }
            }
          }
        }
        n--;
        if (n == SINGLE_MATRIX)
           return newMatrix[0, 0];
        else
        {
           for (int i = 0; i < n; i++) {
            for(int k = 0; k < n; k++)
              detMin += (int)Pow(-1, 1 + i + k + 1) * newMatrix[i, k] * Minor(newMatrix, n, i,k);
          }
           return detMin;
        }
      }
    }
    public string Decode(string input, string key) {
      result = "";
      int sqrt = (int)Sqrt(key.Length);
      int rows;
      if (input.Length % sqrt == 0)
        rows = input.Length / sqrt;
      else
      {
        rows = (input.Length / sqrt) + 1;
      }
      int[,] X = new int[rows, sqrt];
      int[,] Y = new int[sqrt, sqrt];
      List<int> Z = new List<int>();
      List<string> tmp = new List<string>();
      ConvertToNumbers(ref X, ref Y, rows, sqrt, input, key);
      int det = Det(Y, sqrt);
      int x=0, y=0,backDet =0;
      EuclideanAlgorithm(det, Algorithm.ABCSize, ref x, ref y);
      if (det < 0 && x > 0 || det > 0 && x > 0)
        backDet = x;
      else if (det > 0 && x < 0)
        backDet = Algorithm.ABCSize + x;
      else if (det < 0 && x < 0)
        backDet = -x;
      int[,] newKey = new int[sqrt, sqrt];
      int element;
      for (int i = 0; i < sqrt; i++)
      {
        for (int k = 0; k < sqrt; k++)
        {
          element = (int)Pow(-1,(i+k+2))*((Minor(Y, sqrt, i, k)) * backDet % Algorithm.ABCSize);
          if (element < 0)
            newKey[k, i] = Algorithm.ABCSize + (element % Algorithm.ABCSize);
          else
            newKey[k, i] = element % Algorithm.ABCSize;
        }
      }
      MatrixMultiply(Z, X, newKey, sqrt,rows);
      for (int i = 0; i < Z.Count(); i++)
      {
          tmp.Add(Algorithm.ABC[Z[i]]);
      }
      for (int i = 0; i < tmp.Count(); i++)
        result += tmp[i];
      Console.WriteLine("Получилась строка: {0}", result);
      return result;
    }
  }
}
