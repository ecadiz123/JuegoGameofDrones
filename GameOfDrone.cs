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


    class Proximidad
    {
		//Clase que se va a usar para ver si hay un dron en la proximidad de la zona, como las zonas son circulares, se va a trabajar con circulo
		public int radio;
		public Punto centro= new Punto();
		//metodo que devuelve si hay drones en la proximidad, 0 es que no hay
		public int puntoAdentro(Punto aux)
		{
			//Se va a trabajar con ecuacion de la circunferencia para ver si punto esta adentro o no. Es int, si hay devuelve 1, esto para después facilitar
			//el guardar cuantos hay en un arreglo mas adelante
			int xh= aux.x-this.centro.x;//(x-h)
			int yk= aux.y-this.centro.y;//(y-k)
						//expresion que evalua si esta dentro o no del circulo
			if ((xh*xh)+(yk*yk)<= (this.radio*this.radio))
			return 1;
			else 
			return 0;

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
		//Arreglo donde se va a guardar zonas
		Proximidad[] areaZona = new Proximidad[Z];
		for (int i = 0; i < Z; i++)//Z es cantidad de zonas
		{
			//aux para guardar centros
			Proximidad auxzona= new Proximidad();
			inputs = Console.ReadLine().Split(' ');
			X = int.Parse(inputs[0]); // corresponds to the position of the center of a zone. A zone is a circle with a radius of 100 units.
			Y = int.Parse(inputs[1]);
			auxzona.centro.x=X;
			auxzona.centro.y=Y;
			auxzona.radio=100;//radio predeterminado zonas.
			areaZona[i]=auxzona;
		}




		//Matriz que guardara posiciones de drones con sus respectivos equipos
		//fila=equipo
		//columna=dron
		//recordar que con valor variable ID es drones propios, es decir Equipos[ID,0] es dron 0 propio
		Punto[,] Equipos = new Punto[P,D];

		// game loop
		while (true)
		{	
			//En cada ronda se actualizara el ID del area. IDAreas[0] = 1, es que el area 0 esta dominada por equipo 1.
			int[] IDAreas = new int[Z];
			for (int i = 0; i < Z; i++)//Z es cantidad de zonas
			{
				int TID = int.Parse(Console.ReadLine()); // ID of the team controlling the zone (0, 1, 2, or 3) or -1 if it is not controlled. The zones are given in the same order as in the initialization.
									//Se guarda ID en arreglo
				IDAreas[i] = TID;

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
		
			//For que recorren matriz de equipos para ver la cantidad de drones en cada zona, se van a guardar en arreglo
			//cantDeDrones[0,1]= cantidad de drones del jugador 0 en la zona 1
			//Es necesario 3 for para recorrer zonas, jugadores y drones respectivamente.
			
			int[] cantDeEnemigos = new int[Z];
			for (int i = 0; i < Z; i++)//for de zonas
			{
				for (int j = 0; j<P; j++)//for de jugadores
				{	
					for(int k = 0; k<D; k++)//for de drones
					{
					    if(j!=ID)
					    {
						cantDeEnemigos[i]+=areaZona[i].puntoAdentro(Equipos[j,k]);
					    }
					}
				}
			}

			for (int i = 0; i < D; i++)//for para actos de cada dron
			{

				//Donde se van a guardar costos. Costo[0] = costo del dron a zona 0
				int[] costostotal = new int[Z];

				//Donde se van a guardar distancias a zonas del dron, Distancia[0]= zona 0
				int[] distancias = new int[Z];
				//for para calcular distancias a zonas
				for (int j = 0; j<Z; j++)
				{
					//i= dron con el que se trabaja, j= area que se visita
					int aux=Equipos[ID,i].distancia(areaZona[j].centro);
					distancias[j] = aux;
				}
				//calculo de costos totales 
				//variable auxiliar para saber cual es el valor del menor costo
				int menorcosto= int.MaxValue;
				for(int j= 0;j<Z;j++)
				{

					//Calculo simple donde se le suma a la distancia la cantidad de enemigos multiplicada por una constante obtenida mediante prueba
					//El calculo se va a hacer de forma distinta para cada cantidad de drones, así escalarlo de manera que funcione bien en todos
					//los casos
				    switch (D)
				    {
					case 3: costostotal[j] = distancias[j] + cantDeEnemigos[j]*300;
					    break;
					case 4:costostotal[j] = distancias[j] + cantDeEnemigos[j]*400;
					    break;
					case 5:costostotal[j] = distancias[j] + cantDeEnemigos[j]*500;
					    break;
					case 6:costostotal[j] = distancias[j] + cantDeEnemigos[j]*600;
					    break;                                                          
					case 7:costostotal[j] = distancias[j] + cantDeEnemigos[j]*700;
					    break;                                                         
					case 8:costostotal[j] = distancias[j] + cantDeEnemigos[j]*800;
					    break;                                                          
					case 9:costostotal[j] = distancias[j] + cantDeEnemigos[j]*900;
					    break;
					case 10:costostotal[j] = distancias[j] + cantDeEnemigos[j]*1000;
					    break;                                                            
					case 11:costostotal[j] = distancias[j] + cantDeEnemigos[j]*1100;
					    break;
				    }
					//if que va guardando el menor
					if (menorcosto>costostotal[j])
					{
						menorcosto=costostotal[j];
					}
				}
				//variable que guarda la zona con menor costo
				//usa metodo de c# que devuelve indice, el cual representa la zona en nuestro caso
				int zonamenor = Array.FindIndex(costostotal, x => x==menorcosto);
				//se imprime zona menor costo
				areaZona[zonamenor].centro.printpto();

			}
		}
    }
}
