from calculator import add, subtract, multiply, divide


def main():
    operations = {'1': add, '2': subtract, '3': multiply, '4': divide}

    print("Simple Python Calculator")
    print("Select operation:")
    print("1. Add")
    print("2. Subtract")
    print("3. Multiply")
    print("4. Divide")

    choice = input("Enter choice (1/2/3/4): ")

    if choice not in operations:
        print("Invalid input")
        return

    try:
        num1 = float(input("Enter first number: "))
        num2 = float(input("Enter second number: "))
        result = operations[choice](num1, num2)
        print(f"Result: {result}")
    except ValueError as e:
        print(f"Error: {e}")


if __name__ == '__main__':
    main()
