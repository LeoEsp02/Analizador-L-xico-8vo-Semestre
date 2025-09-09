using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AnalizadorLexico
{
    public partial class Form1 : Form
    {
        // Define los componentes gráficos de la interfaz
        private TextBox inputBox;  // Caja de texto donde el usuario ingresará el código fuente.
        private Button analyzeButton;  // Botón para iniciar el análisis del código.
        private Button clearButton;  // Botón para limpiar los campos.
        private Button exitButton;  // Botón para salir de la aplicación.
        private ListBox resultBox;  // Caja donde se mostrarán los resultados del análisis.

        // Aquí almacena las palabras reservadas
        private readonly HashSet<string> palabrasReservadas = new HashSet<string>
        {
            "Equipo", "limpia", "inicio", "termina", "escribe", "suma",
            "para", "mientras", "imprimir", "clase", "matematica", "azar",
            "entrada", "hacer_mientras", "seleccionar", "regresar", "variable",
            "operaciones_archivo", "espaciodenombre", "referencia", "ir_a"
        };

        // Este diccionario guarda los identificadores que encuentra en el código
        // asignándoles un número único
        private Dictionary<string, int> identificadoresNumerados = new Dictionary<string, int>();
        private int contadorIdentificadores = 1;  // El contador para numerar los identificadores.

        // Constructor de la clase Form1, donde se inicializan los componentes gráficos
        public Form1()
        {
            InitializeComponent();  // Inicializa los componentes 
            InitCustomComponents();  // Inicializa los componentes (botones, cajas de texto,).
        }

        // Configura el diseño de los componentes gráficos
        private void InitCustomComponents()
        {
            this.Text = "Analizador Léxico";  // Título de la ventana.
            this.Width = 1200;  // Ancho de la ventana.
            this.Height = 700;  // Alto de la ventana.
            
            // Títulos de etiquetas
            Label universidadLabel = new Label
            {
                Text = "Universidad Autónoma de Tamaulipas",
                Top = 10,
                Left = 10,
                Width = 500,
                Height = 25,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };
            this.Controls.Add(universidadLabel);


            Label unidadLabel = new Label
            {
                Text = "Unidad Académica Multidisciplinaria Mante",
                Top = 35,
                Left = 10,
                Width = 500,
                Height = 25,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };
            this.Controls.Add(unidadLabel);


            Label carreraLabel = new Label
            {
                Text = "Ingeniero en Sistemas Computacionales",
                Top = 60,
                Left = 10,
                Width = 500,
                Height = 25,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };
            this.Controls.Add(carreraLabel);


            Label tituloLabel = new Label
            {
                Text = "Analizador Léxico",
                Top = 90,
                Left = 10,
                Width = 500,
                Height = 30,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };
            this.Controls.Add(tituloLabel);


            Label equipoLabel = new Label
            {
                Text = "Equipo: Los pequeñines",
                Top = 125,
                Left = 10,
                Width = 500,
                Height = 25,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };
            this.Controls.Add(equipoLabel);


            Label instruccionLabel = new Label
            {
                Text = "Cadena para analizar:",
                Top = 155,
                Left = 10,
                Width = 500,
                Height = 25,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };
            this.Controls.Add(instruccionLabel);

            

            // Configuración de la caja de texto donde se ingresará el código
            inputBox = new TextBox
            {
                Multiline = false,  // Solo una linea de texto
                Width = 800,  // Ancho de la caja de texto.
                Height = 30,  // Alto de la caja de texto.
                Top = 180,  // Posición desde la parte superior de la ventana.
                Left = 10,  // Posición desde el borde izquierdo de la ventana.
                Font = new System.Drawing.Font("Consolas", 12)  // Fuente para mostrar el texto.
            };

            
            // Imagen superior
            PictureBox imagenArriba = new PictureBox
            {
                Image = Image.FromFile("C:\\Users\\leoes\\Desktop\\AnalizadorLexico\\images\\logo_uat-removebg-preview.png"),  // Ruta de la imagen superior
                SizeMode = PictureBoxSizeMode.StretchImage,  // Ajuste de tamaño
                Width = 250,  // Ancho de la imagen
                Height = 100,  // Alto de la imagen
                Top = 15,  // Posición desde la parte superior
                Left = 900 // Posición desde izquierda
            };
            this.Controls.Add(imagenArriba);  // Añadir la imagen superior al formulario

            // Imagen inferior
            PictureBox imagenAbajo = new PictureBox
            {
                Image = Image.FromFile("C:\\Users\\leoes\\Desktop\\AnalizadorLexico\\images\\logo_uamm-removebg-preview.png"),  // Ruta de la imagen inferior
                SizeMode = PictureBoxSizeMode.StretchImage,  // Ajuste de tamaño
                Width = 250,  // Ancho de la imagen
                Height = 100,  // Alto de la imagen
                Top = 120,  // Posición debajo de la primera imagen
                Left = 900 // Posición desde izquierda
            };
            this.Controls.Add(imagenAbajo);  // Añadir la imagen inferior al formulario
            
            // Configuración del botón para analizar el código
            analyzeButton = new Button
            {
                Text = "Analizar",  // Texto que aparece en el botón.
                Top = 220,  // Posición desde la parte superior de la ventana.
                Left = 10,  // Posición desde el borde izquierdo de la ventana.
                Width = 150,  // Ancho del botón.
                Height = 50  // Alto del botón.
            };
            analyzeButton.Click += AnalizarCodigo;  // Asociamos el evento del clic al método AnalizarCodigo.

            // Configuración del botón para limpiar el contenido
            clearButton = new Button
            {
                Text = "Limpiar",  // Texto en el botón.
                Top = 220,  // Posición desde la parte superior.
                Left = 180,  // Posición desde el borde izquierdo.
                Width = 150,  // Ancho del botón.
                Height = 50  // Alto del botón.
            };
            clearButton.Click += (s, e) =>
            {
                // Limpia los campos de entrada y resultado cuando se hace clic en "Limpiar"
                inputBox.Clear();
                resultBox.Items.Clear();
                identificadoresNumerados.Clear();
                contadorIdentificadores = 1;
            };
            
            // Configuración del botón para salir de la aplicación
            exitButton = new Button
            {
                Text = "Salir",  // Texto en el botón.
                Top = 220,  // Posición desde la parte superior.
                Left = 350,  // Posición desde el borde izquierdo.
                Width = 150,  // Ancho del botón.
                Height = 50  // Alto del botón.
            };
            exitButton.Click += (s, e) => Application.Exit();  // Cierra la aplicación al hacer clic.
            
            //  Botón para mostrar el equipo
            Button equipoButton = new Button
            {
                Text = "Equipo",
                Top = 220,
                Left = 520,
                Width = 150,
                Height = 50
            };
            equipoButton.Click += (s, e) =>
            {
                resultBox.Items.Clear();
                resultBox.Items.Add("Integrantes del equipo de Los Pequenines:");
                resultBox.Items.Add("1. Quiroga Hoy Jorge Alejandro");
                resultBox.Items.Add("2. Ramos Espinoza Leonardo");
                resultBox.Items.Add("3. Rodela Castillo Sebastián");
                resultBox.Items.Add("4. Rojas Olvera Marco Antonio");
                resultBox.Items.Add("5. Soria Ortiz Marco Antonio");
                resultBox.Items.Add("6. Turrubiates Cervantes Daniel");
                resultBox.Items.Add("7. Verlage Aceves Axel Aram");
            };
            

            // Configuración de la lista donde muestra los resultados del análisis
            resultBox = new ListBox
            {
                Top = 290,  // Posición desde la parte superior.
                Left = 10,  // Posición desde el borde izquierdo.
                Width = 1100,  // Ancho de la lista.
                Height = 350,  // Alto de la lista.
                Font = new System.Drawing.Font("Consolas", 12)  // Fuente de la lista.
            };

            // Agrega todos los controles al formulario
            this.Controls.Add(inputBox); // campo de texto donde se ingresa lo que se analizara 
            this.Controls.Add(analyzeButton); // Boton de analizar
            this.Controls.Add(clearButton); // Boton para limpiar
            this.Controls.Add(exitButton); //boton para salir
            this.Controls.Add(resultBox); //campo de texto donde se muestra el resultado del analisis 
            this.Controls.Add(equipoButton); //boton para mostrar el equipo
                
        }

        // Este método se llama cuando se hace clic en el botón "Analizar"
    private void AnalizarCodigo(object sender, EventArgs e)
{
    resultBox.Items.Clear();  // Limpia el resultado previo
    identificadoresNumerados.Clear();  // Limpia los identificadores
    contadorIdentificadores = 1;  // Resetea el contador de identificadores

    string texto = inputBox.Text;  // Obtiene el código ingresado en la caja de texto.
    
    // Variable buffer para ir leyendo los caracteres del código
    string buffer = "";
    string[] lineas = texto.Split(new[] { '\n' }, StringSplitOptions.None);  // Divide el texto en líneas

    foreach (string linea in lineas)
    {
        string trimmedLinea = linea.Trim();  // Elimina los espacios antes y después de la línea
        if (string.IsNullOrWhiteSpace(trimmedLinea)) continue;  // Si la línea está vacía, la salta

        foreach (char c in trimmedLinea)
        {
            if (char.IsLetterOrDigit(c))  // Si el carácter es una letra o un número, lo agrega al buffer.
            {
                buffer += c;
            }
            else
            {
                // Si el carácter no es una letra ni un número, procesa el token que se a acumulado en el buffer
                if (!string.IsNullOrWhiteSpace(buffer))  // Si hay algo en el buffer, analizamos el token.
                {
                    AnalizarToken(buffer);  // Llama a la función que analizará el token.
                    buffer = "";  // Limpia el buffer después de procesarlo.
                }

                // Analiza caracteres especiales (operadores, paréntesis, etc.)
                switch (c)
                {
                    case '=':
                        resultBox.Items.Add("= → Operador de asignación");
                        break;
                    case '+':
                        resultBox.Items.Add("+ → Operador de suma");
                        break;
                    case '-':
                        resultBox.Items.Add("- → Operador de resta");
                        break;
                    case '*':
                        resultBox.Items.Add("* → Operador de multiplicación");
                        break;
                    case '/':
                        resultBox.Items.Add("/ → Operador de división");
                        break;
                    case '(':
                        resultBox.Items.Add("( → Paréntesis que abre");
                        break;
                    case ')':
                        resultBox.Items.Add(") → Paréntesis que cierra");
                        break;
                    case ';':
                        resultBox.Items.Add("; → Fin de instrucción");
                        break;
                    case ' ':
                    case '\n':
                    case '\r':
                    case '\t':
                        break;  // Ignora espacios, saltos de línea y tabulaciones.
                    default:
                        resultBox.Items.Add($"{c} → Símbolo no reconocido");
                        break;
                }
            }
        }

        // Analiza el último token que quede en el buffer
        if (!string.IsNullOrWhiteSpace(buffer))
        {
            AnalizarToken(buffer);  // Llama a la función que procesa el token (palabra o número) que ha quedado en el buffer.
        }

        // Verificación de punto y coma
        if (!trimmedLinea.EndsWith(";") && !trimmedLinea.Contains("(") && !trimmedLinea.Contains(")"))
        {
            resultBox.Items.Add("Advertencia: falta el punto y coma al final.");
        }
    }

    // Verificación de paréntesis balanceados
    int apertura = 0, cierre = 0;
    foreach (char ch in texto)
    {
        if (ch == '(') apertura++;
        if (ch == ')') cierre++;
    }

    if (apertura != cierre)
    {
        resultBox.Items.Add($"⚠ Error: número desigual de paréntesis. Abiertos: {apertura}, Cerrados: {cierre}");
    }
}



        // Método que analiza los tokens (palabras, números, etc.)
// Este método se encarga de clasificar los tokens y agregar una descripción del tipo de token en el cuadro de resultados (resultBox).
        private void AnalizarToken(string token)
        {
            // Primero verifica si el token es una palabra reservada
            if (palabrasReservadas.Contains(token))  // Si el token está en la lista de palabras reservadas
            {
                // Si es una palabra reservada, muestra un mensaje indicando que es una palabra reservada
                resultBox.Items.Add($"{token} → Palabra reservada");
            }
            // Si no es una palabra reservada, verifica si el token es un número
            else if (int.TryParse(token, out _))  // Si el token se puede convertir a un número (int)
            {
                // Si es un número, muestra un mensaje indicando que es un número
                resultBox.Items.Add($"{token} → Número");
            }
            // Si no es ni palabra reservada ni número, verifica si es un identificador ya encontrado
            else if (identificadoresNumerados.ContainsKey(token))  // Si el token ya está en el diccionario de identificadores
            {
                // Si ya es un identificador previamente encontrado, muestras su identificador numerado
                resultBox.Items.Add($"{token} → Identificador {identificadoresNumerados[token]}");
            }
            // Si el token no es ninguna de las anteriores, lo trata como un nuevo identificador
            else  // Si es un nuevo identificador
            {
                // Si es un nuevo identificador, se asigna un número y lo muestra en los resultados
                resultBox.Items.Add($"{token} → Identificador {contadorIdentificadores}");
                // Agrega el identificador al diccionario con su número único
                identificadoresNumerados[token] = contadorIdentificadores;
                // Aumenta el contador para el siguiente identificador
                contadorIdentificadores++;
            }
        }

    }
}
