using System;

class SquareMatrix
{
    private int[,] matrix;

    public SquareMatrix(int size)
    {
        matrix = new int[size, size];
    }

    public int Size
    {
        get { return matrix.GetLength(0); }
    }

    public int this[int row, int col]
    {
        get { return matrix[row, col]; }
        set { matrix[row, col] = value; }
    }

    public SquareMatrix Transpose()
    {
        SquareMatrix result = new SquareMatrix(Size);

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                result[i, j] = matrix[j, i];
            }
        }

        return result;
    }

    public int Trace()
    {
        int trace = 0;

        for (int i = 0; i < Size; i++)
        {
            trace += matrix[i, i];
        }

        return trace;
    }

    public void ConvertToDiagonal(Action<SquareMatrix> convertDelegate)
    {
        convertDelegate(this);
    }

    public override string ToString()
    {
        string result = "";

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                result += matrix[i, j] + " ";
            }
            result += "\n";
        }

        return result;
    }
}

class MatrixCalculator
{
    private SquareMatrix matrix;

    public MatrixCalculator(int size)
    {
        matrix = new SquareMatrix(size);
    }

    public void Start()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Заполнить матрицу");
            Console.WriteLine("2. Транспонировать матрицу");
            Console.WriteLine("3. Найти след матрицы");
            Console.WriteLine("4. Диагональный вид");
            Console.WriteLine("5. Вывести матрицу");
            Console.WriteLine("0. Выйти");

            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    FillMatrix();
                    break;
                case "2":
                    TransposeMatrix();
                    break;
                case "3":
                    CalculateTrace();
                    break;
                case "4":
                    ConvertToDiagonal();
                    break;
                case "5":
                    PrintMatrix();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.\n");
                    break;
            }
        }
    }

    private void FillMatrix()
    {
        Console.WriteLine("Заполнение матрицы:");
        for (int i = 0; i < matrix.Size; i++)
        {
            for (int j = 0; j < matrix.Size; j++)
            {
                Console.Write($"Введите элемены слева направо, сверху вниз [{i}, {j}]: ");
                matrix[i, j] = int.Parse(Console.ReadLine());
            }
        }
        Console.WriteLine();
    }

    private void TransposeMatrix()
    {
        SquareMatrix transposedMatrix = matrix.Transpose();
        Console.WriteLine("Транспонированная матрица:");
        Console.WriteLine(transposedMatrix);
    }

    private void CalculateTrace()
    {
        int trace = matrix.Trace();
        Console.WriteLine($"След матрицы: {trace}\n");
    }

    private void ConvertToDiagonal()
    {
        Action<SquareMatrix> convertDelegate = delegate (SquareMatrix m) {
            for (int i = 0; i < m.Size; ++i)
            {
                for (int j = 0; j < m.Size; ++j)
                {
                    if (i != j)
                        m[i, j] = 0;
                }
            }
        };

        matrix.ConvertToDiagonal(convertDelegate);
        Console.WriteLine("Диагональный вид.\n");
    }

    private void PrintMatrix()
    {
        Console.WriteLine("Матрица:");
        Console.WriteLine(matrix);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите размер матрицы: ");
        int size = int.Parse(Console.ReadLine());

        MatrixCalculator calculator = new MatrixCalculator(size);
        calculator.Start();
    }
}