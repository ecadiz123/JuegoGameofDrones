using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
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
        public int distancia(int xini, int yini)
        {
            int diag = ((this.x - xini) * (this.x - xini)) + ((this.y - yini) * (this.y - yini));
            double aux;//doble para aplicar raiz de formula de distancia
            aux = Math.Sqrt(Convert.ToDouble(diag));//se convierte a double para raiz y se guarda en aux
            //Se redondea manualmente para obtener el valor mas cercano
            //ya que metodo convert redondea a valor par
            Math.Round(aux);
            return Convert.ToInt32(aux);

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
        //Lista donde se va a guardar centros areas
        List<Punto> CentrosAreas = new List<Punto>();
        for (int i = 0; i < Z; i++)//Z es cantidad de zonas
        {
            //aux para guardar centros
            Punto centrolist = new Punto();
            inputs = Console.ReadLine().Split(' ');
            X = int.Parse(inputs[0]); // corresponds to the position of the center of a zone. A zone is a circle with a radius of 100 units.
            Y = int.Parse(inputs[1]);
            centrolist.x = X;
            centrolist.y = Y;
            CentrosAreas.Add(centrolist);
        }
        //Fila para drones propios
        Queue<Punto> MisDrones = new Queue<Punto>();
        //Se va a hacer Lista con equipos enemigos que contenga fila
        //donde cada fila tendra los drones de estos
        List<Queue<Punto>> EquiposEnemigos = new List<Queue<Punto>>();
        Queue<Punto> DronesEnemigos = new Queue<Punto>();

        // game loop
        while (true)
        {
            for (int i = 0; i < Z; i++)//Z es cantidad de zonas
            {
                int TID = int.Parse(Console.ReadLine()); // ID of the team controlling the zone (0, 1, 2, or 3) or -1 if it is not controlled. The zones are given in the same order as in the initialization.
            }
            for (int i = 0; i < P; i++)//i define con que jugador se esta trabajando en el ciclo
            {
                for (int j = 0; j < D; j++)//D es cantidad de drones
                {
                    inputs = Console.ReadLine().Split(' ');
                    int DX = int.Parse(inputs[0]); // The first D lines contain the coordinates of drones of a player with the ID 0, the following D lines those of the drones of player 1, and thus it continues until the last player.
                    int DY = int.Parse(inputs[1]);
                    //caso donde se trabaja con drones propios, se van a guardar en fila
                    if (i == ID)
                    {
                        //aux para guardar drones
                        Punto auxdron = new Punto();
                        auxdron.x = DX;
                        auxdron.y = DY;
                        MisDrones.Enqueue(auxdron);
                    }
                    else
                    {
                        //Se guardan drones del enemigo en fila
                        Punto auxdron2 = new Punto();
                        auxdron2.x = DX;
                        auxdron2.y = DY;
                        DronesEnemigos.Enqueue(auxdron2);
                    }
                }
                //Guardado de fila de drones enemigos en lista de los equipos
                if (i != ID)
                {
                    EquiposEnemigos.Add(DronesEnemigos);
                    //Ahora se limpia drones enemigos para el siguiente enemigo
                    DronesEnemigos.Clear();
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
