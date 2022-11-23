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
        int tabular;
        string primeraProduccion;
        public Lenguaje(string nombre) : base(nombre)
        {
            tabular =0;
            primeraProduccion = "";
        }
        public Lenguaje()
        {
            tabular=0;
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
           tabularCodigo("}");
            tabularCodigo("}");
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
            tabularCodigo("using System;");
            tabularCodigo("using System.Collections.Generic;");

            tabularCodigo("namespace Generico");
            tabularCodigo("{");
            tabularCodigo("public class Lenguaje : Sintaxis, IDisposable");
            tabularCodigo("{");
            tabularCodigo("public Lenguaje(string nombre) : base(nombre)");
            tabularCodigo("{");
            tabularCodigo("}");

            tabularCodigo("public Lenguaje()");
            tabularCodigo("{");
            tabularCodigo("}");
            tabularCodigo("public void Dispose()");
            tabularCodigo("{");
            tabularCodigo("cerrar();");
            tabularCodigo("}");
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
            tabularCodigo("private void " + getContenido() + "()");
            tabularCodigo("{");
            match(Tipos.SNT);
            match(Tipos.Produce);
            Simbolos();
            match(Tipos.FinProduccion);
            tabularCodigo("}");

            if (!FinArchivo())
            {
                listaProducciones();
            }
        }
        private void Simbolos()
        {
            if (esTipo(getContenido()))
            {
                tabularCodigo("match(Tipos." + getContenido() + ");");
                match(Tipos.SNT);
            }
            else if (getClasificacion() == Tipos.ST)
            {
                tabularCodigo("match(\"" + getContenido() + "\");");
                match(Tipos.ST);
            }
            else if (getClasificacion() == Tipos.SNT)
            {
                tabularCodigo(getContenido() + "();");
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

        private void tabularCodigo(string codigo)
        {
            for(int i =0; i<codigo.Length; i++)
            {
                if(codigo[i] == '}')
                {
                    tabular--;
                }
            }
            for(int i =0; i<tabular; i++)
            {
                lenguaje.Write("\t");
            }
             for(int i =0; i<codigo.Length; i++)
            {
                 if(codigo[i] == '{')
                {
                    tabular++;
                }
            }
            lenguaje.WriteLine(codigo);
        }

    }
}