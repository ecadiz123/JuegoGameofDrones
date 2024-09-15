using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**

 * the standard input according to the problem statement.
 **/
class Player
{
    //Clase para trabajar con puntos de la matriz
    class Punto
    {
        public int x;
        public int y;

        //metodo de puntos que da la distancia del pto ingresado al pto en si
        public int distancia(Punto aux)
        {
            int diag = ((this.x - aux.x) * (this.x - aux.x)) + ((this.y - aux.y) * (this.y - aux.y));//es int porque los puntos son int
            double diagdouble;//doble para aplicar raiz de formula de distancia

            diagdouble = Math.Sqrt(Convert.ToDouble(diag));//se convierte a double para raiz y se guarda en diagdouble

            //Se redondea manualmente para obtener el valor mas cercano
            //ya que metodo convert redondea a valor par
            Math.Round(diagdouble);

            return Convert.ToInt32(diagdouble);

        }
	//metodo para printear el pto en el formato output del juego
	public void printpto(){
	    
	    Console.WriteLine($"{this.x} {this.y}");
	}
    }

    static void a(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int P = int.Parse(inputs[0]); // number of players in the game (2 to 4 players)
        int ID = int.Parse(inputs[1]); // ID of your player (0, 1, 2, or 3)
        int D = int.Parse(inputs[2]); // number of drones in each team (3 to 11)
        int Z = int.Parse(inputs[3]); // number of zones on the map (4 to 8)
        int X = 0;
        int Y = 0;
        //Arreglo donde se va a guardar centros de areas
        Punto[] CentrosAreas = new Punto[Z];
        for (int i = 0; i < Z; i++)//Z es cantidad de zonas
        {
            //aux para guardar centros
            Punto centroarreglo = new Punto();
            inputs = Console.ReadLine().Split(' ');
            X = int.Parse(inputs[0]); // corresponds to the position of the center of a zone. A zone is a circle with a radius of 100 units.
            Y = int.Parse(inputs[1]);
            centroarreglo.x = X;
            centroarreglo.y = Y;
	    CentrosAreas[i] = centroarreglo;
        }


	//Matriz que guardara posiciones de drones con sus respectivos equipos
	//fila=equipo
	//columna=dron
	//recordar que con valor variable ID es drones propios, es decir Equipos[ID,0] es dron 0 propio
	Punto[,] Equipos = new Punto[P,D];

        // game loop
        while (true)
        {
            for (int i = 0; i < Z; i++)//Z es cantidad de zonas
            {
                int TID = int.Parse(Console.ReadLine()); // ID of the team controlling the zone (0, 1, 2, or 3) or -1 if it is not controlled. The zones are given in the same order as in the initialization.
            }
            for (int i = 0; i < P; i++)//i define con que jugador se esta trabajando en el ciclo ID es propio
            {
                for (int j = 0; j < D; j++)//D es cantidad de drones
                {
                    inputs = Console.ReadLine().Split(' ');
                    int DX = int.Parse(inputs[0]); // The first D lines contain the coordinates of drones of a player with the ID 0, the following D lines those of the drones of player 1, and thus it continues until the last player.
                    int DY = int.Parse(inputs[1]);
		    //se van a guardar drones en matriz equipos
		    Punto aux1 = new Punto();
		    aux1.x= DX;
		    aux1.y= DY;
		    Equipos[i,j] = aux1;
                }
            }

            for (int i = 0; i < D; i++)
            {

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");


                // output a destination point to be reached by one of your drones. The first line corresponds to the first of your drones that you were provided as input, the next to the second, etc.


            }
        }
    }
}
