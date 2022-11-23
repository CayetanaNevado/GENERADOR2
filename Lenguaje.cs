//MARIA DE LOS ANGELES CAYETANA NEVADO RODRIGUEZ
using System;
using System.Collections.Generic;
//Requerimiento 1: Construir un metodo para escribir en el arrchivo lenguaje .cs indentando el codigo
//                 {Incrementa un tabulador,} decrementa un tabulador
//Requerimiento 2: Declarar un atributo primeraProduccion de tipo string 
//                 y actualizarlo con la primera produccion de la gramatica
//Requerimiento 3:La primera produccion es puclica y el resto privada 
//Requerimiento 4:El constructor lexico parametrico debe validar que la extension del archivo compilar
//                sea .gen y si no levantar una excepcion.
//Requerimiento 5: Resolver la ambiguedadd de st y snt.
namespace Generador
{
    public class Lenguaje : Sintaxis, IDisposable
    {

        string primeraProduccion;
        public Lenguaje(string nombre) : base(nombre)
        {

            primeraProduccion = "";
        }
        public Lenguaje()
        {
            primeraProduccion = "";
        }
        public void Dispose()
        {
            cerrar();
        }

        public void gramatica()
        {
            cabecera();
            primeraProduccion = getContenido();
            Programa(primeraProduccion);
            cabeceraLenguaje();
            listaProducciones();
            lenguaje.WriteLine("\t}");
            lenguaje.WriteLine("}");
        }
        private void Programa(string produccionPrincipal)
        {
            programa.WriteLine("using System;");
            programa.WriteLine("using System.IO;");
            programa.WriteLine("using System.Collections.Generic;");
            programa.WriteLine();
            programa.WriteLine("namespace Generico");
            programa.WriteLine("{");
            programa.WriteLine("\tpublic class Program");
            programa.WriteLine("\t{");
            programa.WriteLine("\t\tstatic void Main(string[] args)");
            programa.WriteLine("\t\t{");
            programa.WriteLine("\t\t\ttry");
            programa.WriteLine("\t\t\t{");
            programa.WriteLine("\t\t\t\tusing (Lenguaje a = new Lenguaje())");
            programa.WriteLine();
            programa.WriteLine("\t\t\t\t{");
            programa.WriteLine("\t\t\t\t\ta." + produccionPrincipal + "()");
            programa.WriteLine("\t\t\t\t}");
            programa.WriteLine("\t\t\t}");
            programa.WriteLine("\t\t\tcatch (Exception e)");
            programa.WriteLine("\t\t\t{");
            programa.WriteLine("\t\t\t\tConsole.WriteLine(e.Message);");
            programa.WriteLine("\t\t\t}");
            programa.WriteLine("\t\t}");
            programa.WriteLine("\t}");
            programa.WriteLine("}");


        }
        
        private void cabeceraLenguaje()
        {
            lenguaje.WriteLine("using System;");
            lenguaje.WriteLine("using System.Collections.Generic;");

            lenguaje.WriteLine("namespace Generico");
            lenguaje.WriteLine("{");
            lenguaje.WriteLine("\tpublic class Lenguaje : Sintaxis, IDisposable");
            lenguaje.WriteLine("\t{");
            lenguaje.WriteLine("\t\tpublic Lenguaje(string nombre) : base(nombre)");
            lenguaje.WriteLine("\t\t{");
            lenguaje.WriteLine("\t\t}");

            lenguaje.WriteLine("\t\tpublic Lenguaje()");
            lenguaje.WriteLine("\t\t{");
            lenguaje.WriteLine("\t\t}");
            lenguaje.WriteLine("\t\tpublic void Dispose()");
            lenguaje.WriteLine("\t\t{");
            lenguaje.WriteLine("\t\t\tcerrar();");
            lenguaje.WriteLine("\t\t}");
        }
        private void cabecera()
        {
            match("Gramatica");
            match(":");
            match(Tipos.SNT);
            match(Tipos.FinProduccion);
        }
        private void listaProducciones()
        {
            lenguaje.WriteLine("\t\tprivate void " + getContenido() + "()");
            lenguaje.WriteLine("\t\t{");
            match(Tipos.SNT);
            match(Tipos.Produce);
            Simbolos();
            match(Tipos.FinProduccion);
            lenguaje.WriteLine("\t\t}");

            if (!FinArchivo())
            {
                listaProducciones();
            }
        }
        private void Simbolos()
        {
            if (esTipo(getContenido()))
            {
                lenguaje.WriteLine("\t\t\tmatch(Tipos." + getContenido() + ");");
                match(Tipos.SNT);
            }
            else if (getClasificacion() == Tipos.ST)
            {
                lenguaje.WriteLine("\t\t\tmatch(\"" + getContenido() + "\");");
                match(Tipos.ST);
            }
            else if (getClasificacion() == Tipos.SNT)
            {
                lenguaje.WriteLine("\t\t\t" + getContenido() + "();");
                match(Tipos.SNT);
            }
            if (getClasificacion() != Tipos.FinProduccion)
            {
                Simbolos();
            }
        }

        private bool esTipo(string Clasificacion)
        {
            switch (Clasificacion)
            {

                case "Identificador":
                case "Numero":
                case "Caracter":
                case "Asignacion":
                case "Inicializacion":
                case "OperadorLogico":
                case "OperadorRelacional":
                case "OperadorTernario":
                case "OperadorTermino":
                case "OperadorFactor":
                case "IncrementoTermino":
                case "IncrementoFactor":
                case "FinSentencia":
                case "Cadena":
                case "TipoDato":
                case "Zona":
                case "Condicion":
                case "Ciclo":
                    return true;
            }
            return false;
        }

    }
}