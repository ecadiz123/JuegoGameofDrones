using System;
using System.Linq;

class Player
{
    class Punto
    {
        public int x;
        public int y;

        public int distancia(Punto aux)
        {
            return (int)Math.Sqrt((x - aux.x) * (x - aux.x) + (y - aux.y) * (y - aux.y));
        }

        public void printpto()
        {
            Console.WriteLine($"{x} {y}");
        }
    }

    class Proximidad
    {
        public int radio;
        public Punto centro = new Punto();

        public bool puntoAdentro(Punto aux)
        {
            int dx = aux.x - centro.x;
            int dy = aux.y - centro.y;
            return dx * dx + dy * dy <= radio * radio;
        }
    }

    static void Main(string[] args)
    {
        string[] inputs = Console.ReadLine().Split(' ');
        int P = int.Parse(inputs[0]); // Número de jugadores
        int ID = int.Parse(inputs[1]); // ID del jugador
        int D = int.Parse(inputs[2]); // Número de drones
        int Z = int.Parse(inputs[3]); // Número de zonas

        Proximidad[] zonas = new Proximidad[Z];
        for (int i = 0; i < Z; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int X = int.Parse(inputs[0]);
            int Y = int.Parse(inputs[1]);

            zonas[i] = new Proximidad { centro = new Punto { x = X, y = Y }, radio = 100 };
        }

        Punto[,] drones = new Punto[P, D];
        int[] zonaOcupacion = new int[Z]; // Ocupación estimada de cada zona

        while (true)
        {
            int[] zonaControladaPor = new int[Z];
            for (int i = 0; i < Z; i++)
            {
                zonaControladaPor[i] = int.Parse(Console.ReadLine());
            }

            for (int i = 0; i < P; i++)
            {
                for (int j = 0; j < D; j++)
                {
                    inputs = Console.ReadLine().Split(' ');
                    int DX = int.Parse(inputs[0]);
                    int DY = int.Parse(inputs[1]);
                    drones[i, j] = new Punto { x = DX, y = DY };
                }
            }

            int[] enemigosEnZona = new int[Z]; // Drones enemigos en cada zona

            // Contar drones enemigos en cada zona
            for (int j = 0; j < Z; j++)
            {
                for (int p = 0; p < P; p++)
                {
                    if (p != ID)
                    {
                        for (int d = 0; d < D; d++)
                        {
                            if (zonas[j].puntoAdentro(drones[p, d]))
                                enemigosEnZona[j]++;
                        }
                    }
                }
            }

            for (int i = 0; i < D; i++) // Para cada dron
            {
                int[] distancias = new int[Z];
                int[] costos = new int[Z];

                for (int j = 0; j < Z; j++)
                {
                    distancias[j] = drones[ID, i].distancia(zonas[j].centro);

                    int enemigos = enemigosEnZona[j];
                    int factorEnemigos = enemigos * 200; // Penalización más baja por enemigos si hay pocos
                    int prioridadZona = zonaControladaPor[j] == -1 ? -800 : (zonaControladaPor[j] == ID ? 200 : -300);

                    int penalizacionExceso = (zonaOcupacion[j] >= 2) ? 1000 : 0; // Penalizar si hay muchos drones

                    costos[j] = distancias[j] + factorEnemigos + prioridadZona + penalizacionExceso;
                }

                // Encontrar la zona con el menor número de enemigos
                var zonasMenosEnemigos = costos
                    .Select((valor, index) => new { valor, index })
                    .Where(z => enemigosEnZona[z.index] < 2) // Preferir zonas con menos de 2 enemigos
                    .OrderBy(z => z.valor)
                    .FirstOrDefault();

                int zonaMenorCosto = zonasMenosEnemigos != null ? zonasMenosEnemigos.index : Array.IndexOf(costos, costos.Min());

                // Si la zona con menos drones está saturada, buscar otra
                if (zonaOcupacion[zonaMenorCosto] >= 2)
                {
                    int nuevaZona = costos
                        .Select((valor, index) => new { valor, index })
                        .Where(z => zonaOcupacion[z.index] < 2)
                        .OrderBy(z => z.valor)
                        .FirstOrDefault()?.index ?? zonaMenorCosto;
                    
                    zonaMenorCosto = nuevaZona;
                }

                // Enviar dron a la zona seleccionada
                zonas[zonaMenorCosto].centro.printpto();
                zonaOcupacion[zonaMenorCosto]++;
            }

            Array.Clear(zonaOcupacion, 0, Z); // Reiniciar ocupación para el siguiente ciclo
        }
    }
}
