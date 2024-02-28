using System;
using System.Threading;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

namespace bancovirtual
{
    class Program
    {
        static decimal[] saldoTarj = { 10000.00m, 15000.00m, 20000.00m, 5000.00m };

        static int verificaciontarjeta = -1;
        static string[] historialTransacciones = new string[100];

        public static void Main()
        {
            Console.Clear();
            string[] opciones = { "Consultar Saldo", "Realizar Depósito", "Realizar Retiro", "Transferir Dinero", "Historial de Transacciones", "Cerrar sesión" };
            int opcionSeleccionada = 0;


        inicio:
            for (int i = 0; i < 8; i++)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("888888b.                                           888     888 d8b         888                      888 ");
                Console.WriteLine("888   88b                                          888     888 Y8P         888                      888 ");
                Console.WriteLine("888  .88P                                          888     888             888                      888 ");
                Console.WriteLine("8888888K.   8888b.  88888b.   .d8888b .d88b.       Y88b   d88P 888 888d888 888888 888  888  8888b.  888 ");
                Console.WriteLine("888   Y88b      88b 888  88b d88P    d88  88b       Y88b d88P  888 888P    888    888  888      88b 888 ");
                Console.WriteLine("888    888 .d888888 888  888 888     888  888        Y88o88P   888 888     888    888  888 .d888888 888 ");
                Console.WriteLine("888   d88P 888  888 888  888 Y88b.   Y88..88P         Y888P    888 888     Y88b.  Y88b 888 888  888 888 ");
                Console.WriteLine("8888888P    Y888888 888  888   Y8888P  Y88P            Y8P     888 888       Y888   Y88888  Y888888 888 ");
                Console.WriteLine("");

                Thread.Sleep(500);
                Console.Clear();
            }

            Console.WriteLine("=== Inicia sesión ===");
            Console.WriteLine();

            string[] codigoTarj = { "2345-3252-1010-5555", "9999-1234-1010-5555", "2664-4694-3252-1232", "1212-4631-3655-0984" };
            string[] pinTarj = { "2144", "1233", "4532", "2334" };

            Console.WriteLine("Digita tu tarjeta");
            string tarjeta = Console.ReadLine()!;

            Console.WriteLine("Digita tu PIN");
            string PIN = Console.ReadLine()!;

            int respuestaValidacion = validaciontarjeta(codigoTarj, tarjeta);
            if (respuestaValidacion != -1)
            {
                verificaciontarjeta = respuestaValidacion;
            }
            else
            {
                Console.WriteLine("Tarjeta no valida");
            }

            bool verifi(string[] codigoTarj, string tarjeta, string PIN, string[] pinTarj)
            {
                for (int i = 0; i < codigoTarj.Length; i++)
                {
                    if (tarjeta == codigoTarj[i] && PIN == pinTarj[i])
                    {
                        verificaciontarjeta = i;
                        return true;
                    }
                }
                return false;
            }

            bool op = verifi(codigoTarj, tarjeta, PIN, pinTarj);

            if (!op)
            {

                Console.WriteLine("Tarjeta o PIN incorrecto.");
                Console.ReadKey();
                goto inicio;
            }



            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.WriteLine("888888b.                                           888     888 d8b         888                      888 ");
                Console.WriteLine("888   88b                                          888     888 Y8P         888                      888 ");
                Console.WriteLine("888  .88P                                          888     888             888                      888 ");
                Console.WriteLine("8888888K.   8888b.  88888b.   .d8888b .d88b.       Y88b   d88P 888 888d888 888888 888  888  8888b.  888 ");
                Console.WriteLine("888   Y88b      88b 888  88b d88P    d88  88b       Y88b d88P  888 888P    888    888  888      88b 888 ");
                Console.WriteLine("888    888 .d888888 888  888 888     888  888        Y88o88P   888 888     888    888  888 .d888888 888 ");
                Console.WriteLine("888   d88P 888  888 888  888 Y88b.   Y88..88P         Y888P    888 888     Y88b.  Y88b 888 888  888 888 ");
                Console.WriteLine("8888888P    Y888888 888  888   Y8888P  Y88P            Y8P     888 888       Y888   Y88888  Y888888 888 ");
                Console.WriteLine("");
                Console.WriteLine("=== Estimado cliente que desea realizar en el dia de hoy ===");
                Console.WriteLine("");

                Console.ResetColor();

                for (int i = 0; i < opciones.Length; i++)
                {
                    if (i == opcionSeleccionada)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"{opciones[i]}");

                    Console.ResetColor();
                }

                ConsoleKeyInfo key = Console.ReadKey();


                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        opcionSeleccionada = (opcionSeleccionada - 1 + opciones.Length) % opciones.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        opcionSeleccionada = (opcionSeleccionada + 1) % opciones.Length;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        EjecutarOpcion(opcionSeleccionada + 1, codigoTarj);
                        break;
                }
            }


            static void EjecutarOpcion(int opcion, string[] codigoTarj)
            {
                switch (opcion)
                {
                    case 1:
                        consultarsaldo();
                        break;
                    case 2:
                        realizardeposito();
                        break;
                    case 3:
                        realizarretiro();
                        break;
                    case 4:
                        transferirdinero(codigoTarj);
                        break;
                    case 5:
                        historialtransacciones();
                        break;
                    case 6:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("¡Que tengas un buen resto del día! ¡Hasta luego!");
                        Console.ReadKey();
                        Main();
                        break;
                }

                Console.WriteLine("\nPresiona cualquier tecla para volver al menú...");
                Console.ReadKey();
            }

            static void consultarsaldo()
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Tu saldo actual es: " + saldoTarj[verificaciontarjeta].ToString("N2"));
                Console.ResetColor();
            }

            static void realizardeposito()
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Ingrese la cantidad que desea depositar:");
                decimal cantidadDeposito;

                while (!decimal.TryParse(Console.ReadLine(), out cantidadDeposito) || cantidadDeposito <= 0)
                {
                    Console.WriteLine("Por favor, ingrese una cantidad válida mayor que cero.");
                }

                saldoTarj[verificaciontarjeta] += cantidadDeposito;
                Console.WriteLine($"Se depositaron {cantidadDeposito:C} en tu cuenta. Saldo actual: {saldoTarj[verificaciontarjeta]:C}");

                Console.ResetColor();
            }

            static void realizarretiro()
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Ingrese la cantidad que desea retirar:");
                decimal cantidadretiro;

                while (!decimal.TryParse(Console.ReadLine(), out cantidadretiro) || cantidadretiro <= 0)
                {
                    Console.WriteLine("Por favor, ingrese una cantidad válida mayor que cero.");
                }

                if (cantidadretiro <= saldoTarj[verificaciontarjeta])
                {
                    saldoTarj[verificaciontarjeta] -= cantidadretiro;
                    Console.WriteLine($"Se retiraron {cantidadretiro:C} en tu cuenta. Saldo actual: {saldoTarj[verificaciontarjeta]:C}");
                }
                else
                {
                    System.Console.WriteLine("SALDO NO SUFICIENTE");
                }
                Console.ResetColor();

            }

            static int validaciontarjeta(string[] codigoTarj, string tarjeta)
            {
                int respuesta = -1;
                for (int i = 0; i < codigoTarj.Length; i++)
                {
                    if (tarjeta == codigoTarj[i])
                    {
                        respuesta = i;
                        break;
                    }
                }
                return respuesta;
            }

            static void transferirdinero(string[] codigoTarj)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Ingrese el número de cuenta destino:");
                string cuentaDestino = Console.ReadLine()!;

                if (VerificarCuentaDestino(cuentaDestino)==true)
                {
                    Console.WriteLine("Ingrese la cantidad que desea transferir:");
                    decimal cantidadTransferencia;

                    while (!decimal.TryParse(Console.ReadLine(), out cantidadTransferencia) || cantidadTransferencia <= 0)
                    {
                        Console.WriteLine("Por favor, ingrese una cantidad válida mayor que cero.");
                    }
                    
                    int indiceCuentaDestino = ObtenerIndiceCuenta(cuentaDestino, codigoTarj);
                    
                    if (saldoTarj[verificaciontarjeta] >= cantidadTransferencia)
                    {
                        saldoTarj[verificaciontarjeta] -= cantidadTransferencia;
                        saldoTarj[indiceCuentaDestino] += cantidadTransferencia;

                        Console.WriteLine($"Se transfirieron {cantidadTransferencia:C} a la cuenta {cuentaDestino}. Saldo actual: {saldoTarj[verificaciontarjeta]:C}");

                        // Registrar la transacción en el historial
                        string transaccion = $"Transferencia a {cuentaDestino}: {cantidadTransferencia:C}";
                        RegistrarTransaccion(transaccion);
                    }
                    else
                    {
                        Console.WriteLine("La cuenta destino no es válida.");
                    }
                    Console.ResetColor();
                }
                else
                {
                    System.Console.WriteLine("Cuenta incorrecta");
                }

                
                static int ObtenerIndiceCuenta(string cuentaDestino, string[] codigoTarj)
                {
                    for (int i = 0; i < codigoTarj.Length; i++)
                    {
                        if (cuentaDestino == codigoTarj[i])
                        {
                            return i;
                        }
                    }
                    return -1;
                }
            }
        }


        static void historialtransacciones()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("=== Historial de Transacciones ===");
            foreach (string transaccion in historialTransacciones)
            {
                if (transaccion != null)
                {
                    Console.WriteLine(transaccion);
                }
            }
            Console.ResetColor();
        }
        static bool VerificarCuentaDestino(string cuentaDestino)
        {
            return true;
        }
        static void RegistrarTransaccion(string transaccion)
        {
            for (int i = 0; i < historialTransacciones.Length; i++)
            {
                if (historialTransacciones[i] == null)
                {
                    historialTransacciones[i] = transaccion;
                    break;
                }
            }
        }
    }
}