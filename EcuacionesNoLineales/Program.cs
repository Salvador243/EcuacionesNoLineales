using System;

namespace EcuacionesNoLineales
{
    class Program
    {
        
        /*
         *EL CODIGO USADO ES RECICLADO DEL EJEMPLO DE CANVAS, MAS SIN EMBARGO SE ESTUDIO SU FUNCIONAMIENTO QUE ES EL PROPOSITO DE LA MATERIA Y
         * SE ADECUA A NUESTRAS NECESIDADES SEGUN EL SISTEMA.
         */
        static void Main(string[] args)
        {
            double pivote, factor;

            double[,] jacobiana = new double[15, 3];
            double[,] matriz = new double[3, 4];

            //promedio de muertes menseales del covid
            double[] NOTconfirmed = { 0,0,2,23,58,102,162,93,63,41,60,143,130,66,50 };
            //meses desde el inicio de la pandemia
            double[] month = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};

            for (int i = 0; i < 15; i = i + 1)
            {
                jacobiana[i, 0] = i;
                jacobiana[i, 1] = month[i];
                jacobiana[i, 2] = month[i]* month[i];
				
            }

            //Multiplicación de matrices
            //------------------------------
            for (int i = 0; i < 3; i = i + 1)
                for (int j = 0; j < 3; j = j + 1)
                    for (int k = 0; k < 15; k = k + 1)
                        matriz[i, j] = matriz[i, j] + jacobiana[k, i] * jacobiana[k, j];

            for (int i = 0; i < 3; i = i + 1)
                for (int j = 0; j < 1; j = j + 1)
                    for (int k = 0; k < 15; k = k + 1)
                        matriz[i, 3] = matriz[i, 3] - NOTconfirmed[k] * jacobiana[k, i];
            //------------------------------

                //Eliminación Gaussiana
                //------------------------------
                for (int reng = 0; reng < 3; reng = reng + 1)
                {
                    pivote = matriz[reng, reng];
                    for (int colu = 0; colu < 4; colu = colu + 1)
                        matriz[reng, colu] = matriz[reng, colu] / pivote;
                    for (int reng_elimi = 0; reng_elimi < 3; reng_elimi = reng_elimi + 1)
                    if (reng_elimi != reng)
                    {
                        factor = matriz[reng_elimi, reng];
                        for (int colu_elimi = 0; colu_elimi < 4;
                            colu_elimi = colu_elimi + 1)
                            matriz[reng_elimi, colu_elimi] = matriz[reng_elimi,
                                colu_elimi] - factor * matriz[reng, colu_elimi];
                    }
                }
                //------------------------------

                //Imprime los valores de las variables segun la poscion de la matriz 
                Console.WriteLine("VALOR DE X1: " + (-matriz[0, 3]));
                Console.WriteLine("VALOR DE X2: " + (-matriz[1, 3]));
                Console.WriteLine("VALOR DE X2: " + (-matriz[2, 3]));


         Console.ReadLine();
        }
    }
}