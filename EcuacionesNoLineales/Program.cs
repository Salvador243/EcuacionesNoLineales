using System;

namespace EcuacionesNoLineales
{
    class Program
    {
        // Sistema de Ecuaciones No Lineales
        static void Main(string[] args)
        {
            //Variables de Entrada 
            double x, y, z, f1, f2, f3, epsilon = 0.0001;

            // pivote y factores variables heredadas de gauss jordan
            double pivote, factor;

            //Se declara la matriz aumentada para utilizar dependiendo del sistema de ecuaciones
            // Si son dos ecuaciones y 2 variables  es una matriz 2 x 3 si es un sistema de 3 ecuaciones con 3 variables es una matriz de 3 x 4
            double[,] matriz = new double[3,4];

            // Paso 1 se darán los valores iniciales a ojo de buen cubero
            x = 0.5;
            y = 0.5;
            z = 0.5;
            // Se calculan las funciones con  los valores inciales de las variables
            f1 = (((y)*Math.Cos(x+y))- (x));
            f2 = (((x)*Math.Exp(x+z))- (y));
            f3 = ((x * z) + (y * z) - (x * y));

            //Mientras las ecuaciones iniciaes sigan siendo mayores a un numero muy aproximado a cero
            while (Math.Abs(f1) > epsilon | Math.Abs(f2) > epsilon | Math.Abs(f3) > epsilon)
            {
                // Se vuelve a calcular las variables ya que estas cambiaran 
                f1 = (((y)*Math.Cos(x+y))- (x));
                f2 = (((x)*Math.Exp(x+z))- (y));
                f3 = ((x * z) + (y * z) - (x * y));

                //Se colocan la matriz jacobi con la columna aumentada multiplicada por  -1

                matriz[0, 0] =  (((-1 * y) * Math.Sin(x + y)) - 1);
                matriz[0, 1] = (((-1 * y) * Math.Sin(x + y)) + (Math.Cos(x + y)));
                matriz[0, 2] = 0;
                matriz[0, 3] = (-f1);
                matriz[1, 0] = ((x * Math.Exp(x + z)) + (Math.Exp(x + z)));
                matriz[1, 1] = -1;
                matriz[1, 2] = (x * Math.Exp(x + z));
                matriz[1, 3] = (-f2);
                matriz[2, 0] = (-y + z);
                matriz[2, 1] = (-x + z);
                matriz[2, 2] = (x + y);
                matriz[2, 3] = (-f3);

                //Eliminación Gaussiana //------------------------------

                for (int reng = 0; reng < 3; reng ++)
                {
                    pivote = matriz[reng, reng];
                    for (int colu = 0; colu < 4; colu ++)
                    {
                        matriz[reng, colu] = matriz[reng, colu] / pivote;
                    }

                    for (int reng_elimi = 0; reng_elimi < 3; reng_elimi ++)
                    {
                        if (reng_elimi != reng)
                        {
                            factor = matriz[reng_elimi, reng];
                            for (int colu_elimi = 0; colu_elimi < 4; colu_elimi = colu_elimi + 1)
                            {
                                matriz[reng_elimi, colu_elimi] = matriz[reng_elimi, colu_elimi]
                                                                 - factor * matriz[reng, colu_elimi];
                            }
                        }
                    }
                }
                //------------------------------ Termina Gauss Jordan ------------------------------

                // Se aplica el incremento de los nuevos valores obtenidos  y se evaluan en la nueva matriz
                x = x + matriz[0, 3];
                y = y + matriz[1, 3];
                z = z + matriz[2, 3];
            }

            //Se Imprimen las Raices
            Console.WriteLine("El valor final de x es: " + x);
            Console.WriteLine("El valor final de y es: " + y);
            Console.WriteLine("El valor final de z es: " + z);
        }
    }
}