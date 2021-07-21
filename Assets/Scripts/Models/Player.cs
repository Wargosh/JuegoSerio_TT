[System.Serializable]
public class Player
{
	// General
	public string _id;              // id de DB
	public string username;         // nombre de usuario.
	public string email;            // correo.
	public string image = "";       // ruta de la imagen si llega una.
	public string timeAgo;          // Ultima conexion del jugador.

	// Progreso del juego
	public int _experience = 0;		// Experiencia.
	public int _level = 1;			// Nivel.
	public int _points = 0;			// Puntos.

	// Estadisticas jugador
	public int _totalGames = 0;     // Total de partidas jugadas en cualquier modo.
	public int _totalProgress = 0;  // Total de progreso del jugador.
	public int _totalReadings = 0;  // Total de veces que el jugador ha leido cuentos.
	public int _totalTime = 0;		// Total de tiempo dentro del juego.
	public int _totalHits = 0;		// # de aciertos.
	public int _totalFails = 0;     // # de fallos.
}
