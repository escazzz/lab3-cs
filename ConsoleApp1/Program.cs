/*using System;

struct Vector
{
    public double x, y, z;

    public Vector(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static Vector operator +(Vector a, Vector b)
        => new Vector(a.x + b.x, a.y + b.y, a.z + b.z);

    public static Vector operator *(Vector a, Vector b)
        => new Vector(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);

    public static Vector operator *(Vector a, double k)
        => new Vector(a.x * k, a.y * k, a.z * k);

    public static bool operator ==(Vector a, Vector b)
        => a.Length() == b.Length();

    public static bool operator !=(Vector a, Vector b)
        => !(a == b);

    public static bool operator <(Vector a, Vector b)
        => a.Length() < b.Length();

    public static bool operator >(Vector a, Vector b)
        => a.Length() > b.Length();

    public static bool operator <=(Vector a, Vector b)
        => a.Length() <= b.Length();

    public static bool operator >=(Vector a, Vector b)
        => a.Length() >= b.Length();

    public double Length()
        => Math.Sqrt(x * x + y * y + z * z);

    public override bool Equals(object obj)
        => obj is Vector other && this == other;

    public override int GetHashCode()
        => (x, y, z).GetHashCode();
}

class Lab3_1
{
    static void Main()
    {
        Vector v1 = new Vector(1, 2, 3);
        Vector v2 = new Vector(4, 5, 6);

        Vector v3 = v1 + v2; // Vector addition
        Vector v4 = v1 * v2; // Vector cross product
        Vector v5 = v1 * 2;  // Vector scaling

        bool areEqual = v1 == v2; // Vector length comparison
        bool areNotEqual = v1 != v2;
        bool isLessThan = v1 < v2;
        bool isGreaterThan = v1 > v2;
        bool isLessThanOrEqual = v1 <= v2;
        bool isGreaterThanOrEqual = v1 >= v2;
    }
}*/
/*using System;
using System.Collections.Generic;

public class Car : IEquatable<Car>
{
    public string Name { get; set; }
    public string Engine { get; set; }
    public int MaxSpeed { get; set; }

    public Car(string name, string engine, int maxSpeed)
    {
        Name = name;
        Engine = engine;
        MaxSpeed = maxSpeed;
    }

    public override string ToString()
    {
        return Name;
    }

    public bool Equals(Car other)
    {
        if (other == null)
            return false;

        return Name == other.Name &&
               Engine == other.Engine &&
               MaxSpeed == other.MaxSpeed;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        Car carObj = obj as Car;
        if (carObj == null)
            return false;
        else
            return Equals(carObj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Engine, MaxSpeed);
    }
}

public class CarsCatalog
{
    private List<Car> cars = new List<Car>();

    public void AddCar(Car car)
    {
        cars.Add(car);
    }

    public string this[int index]
    {
        get
        {
            if (index >= 0 && index < cars.Count)
            {
                return $"Name: {cars[index].Name}, Engine: {cars[index].Engine}";
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}


public class Lab3_2
{
    public static void Main()
    {
        var car1 = new Car("Toyota Corolla", "1.8L", 180);
        var car2 = new Car("Ford Mustang", "5.0L V8", 777);
        var car3 = new Car("Honda Accord", "2.4L", 220);
        var catalog = new CarsCatalog();
        catalog.AddCar(car2);
        catalog.AddCar(car1);
        catalog.AddCar(car3);  

        Console.WriteLine(car1);
        Console.WriteLine(catalog[1]);
        Console.WriteLine(catalog[0]);
        Console.WriteLine(catalog[2]);
    }
}*/
using System;

public class Currency
{
    public decimal Value { get; private set; }

    public Currency(decimal value)
    {
        Value = value;
    }
}

public class CurrencyUSD : Currency
{
    public static decimal ToEURRate { get; set; }
    public static decimal ToRUBRate { get; set; }

    public CurrencyUSD(decimal value) : base(value) { }

    public static implicit operator CurrencyEUR(CurrencyUSD usd)
    {
        return new CurrencyEUR(usd.Value * ToEURRate);
    }

    public static implicit operator CurrencyRUB(CurrencyUSD usd)
    {
        return new CurrencyRUB(usd.Value * ToRUBRate);
    }
}

public class CurrencyEUR : Currency
{
    public static decimal ToUSDRate { get; set; }
    public static decimal ToRUBRate { get; set; }

    public CurrencyEUR(decimal value) : base(value) { }

    public static implicit operator CurrencyUSD(CurrencyEUR eur)
    {
        return new CurrencyUSD(eur.Value * ToUSDRate);
    }

    public static implicit operator CurrencyRUB(CurrencyEUR eur)
    {
        return new CurrencyRUB(eur.Value * ToRUBRate);
    }
}

public class CurrencyRUB : Currency
{
    public static decimal ToUSDRate { get; set; }
    public static decimal ToEURRate { get; set; }

    public CurrencyRUB(decimal value) : base(value) { }

    public static implicit operator CurrencyUSD(CurrencyRUB rub)
    {
        return new CurrencyUSD(rub.Value * ToUSDRate);
    }

    public static implicit operator CurrencyEUR(CurrencyRUB rub)
    {
        return new CurrencyEUR(rub.Value * ToEURRate);
    }
}

class Program
{
    static void Main()
    {
        // Define exchange rates
        CurrencyUSD.ToEURRate = 0.92m; // Example rate, 1 USD = 0.92 EUR
        CurrencyUSD.ToRUBRate = 74.15m; // Example rate, 1 USD = 74.15 RUB
        CurrencyEUR.ToUSDRate = 1.09m; // Example rate, 1 EUR = 1.09 USD
        CurrencyEUR.ToRUBRate = 80.50m; // Example rate, 1 EUR = 80.50 RUB
        CurrencyRUB.ToUSDRate = 0.013m; // Example rate, 1 RUB = 0.013 USD
        CurrencyRUB.ToEURRate = 0.012m; // Example rate, 1 RUB = 0.012 EUR

        // Conversion examples
        CurrencyUSD usd = new CurrencyUSD(50); // $50
        CurrencyEUR eur = usd; // Implicit conversion to EUR
        CurrencyRUB rub = eur; // Implicit conversion to RUB

        Console.WriteLine($"USD: {usd.Value}, EUR: {eur.Value}, RUB: {rub.Value}");
    }
}



