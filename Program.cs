using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
class Ppal
{
    class Punto
    {
        public int x;
        public int y;

        //metodo de puntos que da la distancia del pto ingresado al pto en si
        public int distancia(int xini, int yini)
        {
            int diag = ((this.x - xini) * (this.x - xini)) + ((this.y - yini) * (this.y - yini));
            double aux;
            aux = Math.Sqrt(Convert.ToDouble(diag));
            //Se redondea manualmente para obtener el valor mas cercano
            //ya que metodo convert redondea a valor par
            Math.Round(aux);
            return Convert.ToInt32(aux);

        }
    }
    static void Main()
    {
        Punto ej = new Punto();
        ej.x = 0;
        ej.y = 0;
        Console.WriteLine(ej.distancia(2, 2));


    }


}