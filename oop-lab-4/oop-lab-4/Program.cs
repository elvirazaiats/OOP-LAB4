namespace oop_lab_4
{
    internal class Program
    {
        public Program()
        {
        }
    }



}

namespace oop_lab_4
{
    struct Vector3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        // Конструктор за замовчуванням
        public Vector3D(double x, double y, double z)
        {
            try
            {
                if (x < 0 || y < 0 || z < 0)
                {
                    throw new ArgumentException("Координати не можуть бути від'ємними.");
                }

                X = x;
                Y = y;
                Z = z;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка ініціалізації: {ex.Message}");
                X = 0;
                Y = 0;
                Z = 0;
            }
        }

        // Конструктор для ініціалізації всіх координат однаковим значенням
        public Vector3D(double value) : this(value, value, value) { }

        // Конструктор без параметрів
        public Vector3D() : this(0, 0, 0) { }

        // Метод для обробки помилок ділення на нуль
        public double SafeDivide(double denominator)
        {
            try
            {
                return X / denominator;
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Помилка: ділення на нуль");
                return double.NaN; // Повертаємо NaN (Not a Number), щоб позначити помилку
            }
        }

        // Додавання векторів
        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        // Віднімання векторів
        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        // Скалярний добуток
        public static double DotProduct(Vector3D v1, Vector3D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        // Множення вектора на скаляр
        public static Vector3D MultiplyByScalar(Vector3D v, double scalar)
        {
            return new Vector3D(v.X * scalar, v.Y * scalar, v.Z * scalar);
        }

        // Порівняння векторів
        public static bool operator ==(Vector3D v1, Vector3D v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
        }

        public static bool operator !=(Vector3D v1, Vector3D v2)
        {
            return !(v1 == v2);
        }

        // Обчислення довжини вектора
        public readonly double Magnitude()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        // Порівняння довжин векторів
        public static bool CompareMagnitudes(Vector3D v1, Vector3D v2)
        {
            return v1.Magnitude() == v2.Magnitude();
        }

        // Перевизначення ToString()
        public override readonly string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }

    internal class Prog
    {
        // Метод для читання масиву структур з клавіатури
        static Vector3D[] ReadArrayFromConsole(int n)
        {
            Vector3D[] array = new Vector3D[n];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Введіть координати для вектора {i + 1}:");

                // Читання та конвертація координат, з обробкою помилок
                if (double.TryParse(Console.ReadLine(), out double x) &&
                    double.TryParse(Console.ReadLine(), out double y) &&
                    double.TryParse(Console.ReadLine(), out double z))
                {
                    array[i] = new Vector3D(x, y, z);
                }
                else
                {
                    Console.WriteLine("Помилка введення. Використано значення за замовчуванням.");
                    array[i]
     = new Vector3D();
                }
            }
            return array;
        }

        // Метод для виведення вектора на екран
        static void PrintVector(Vector3D vector)
        {
            Console.WriteLine($"Вектор: {vector}");
        }

        // Метод для сортування масиву векторів за довжиною
        static void SortArrayByMagnitude(Vector3D[] array)
        {
            Array.Sort(array, (v1, v2) => v1.Magnitude().CompareTo(v2.Magnitude()));
        }

        // Метод для модифікації вектора (зміна його полів)
        static void ModifyVector(ref Vector3D vector)
        {
            Console.WriteLine("Введіть нові значення для вектора:");
            if (double.TryParse(Console.ReadLine(), out double x))
            {
                vector.X = x;
            }

            if (double.TryParse(Console.ReadLine(), out double y))
            {
                vector.Y = y;
            }

            if (double.TryParse(Console.ReadLine(), out double z))
            {
                vector.Z = z;
            }
        }

        // Метод для знаходження максимального та мінімального значення серед елементів масиву
        static void FindMinMaxMagnitude(Vector3D[] array, out double maxMagnitude, out double minMagnitude)
        {
            maxMagnitude = double.MinValue;
            minMagnitude = double.MaxValue;

            foreach (var vector in array)
            {
                double magnitude = vector.Magnitude();
                if (magnitude > maxMagnitude)
                {
                    maxMagnitude = magnitude;
                }

                if (magnitude < minMagnitude)
                {
                    minMagnitude = magnitude;
                }
            }
        }

        static void Main()
        {
            // Приклад використання методів
            int n = 3;
            Vector3D[] vectors = ReadArrayFromConsole(n);

            Console.WriteLine("Введені вектори:");
            foreach (var vector in vectors)
            {
                PrintVector(vector);
            }

            SortArrayByMagnitude(vectors);

            Console.WriteLine("Відсортовані вектори за довжиною:");
            foreach (var vector in vectors)
            {
                PrintVector(vector);
            }

            Vector3D modifiedVector = vectors[0];
            ModifyVector(ref modifiedVector);

            Console.WriteLine("Модифікований вектор:");
            PrintVector(modifiedVector);

            FindMinMaxMagnitude(vectors, out double maxMag, out double minMag);
            Console.WriteLine($"Максимальна довжина вектора: {maxMag}");
            Console.WriteLine($"Мінімальна довжина вектора: {minMag}");
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }

}

           