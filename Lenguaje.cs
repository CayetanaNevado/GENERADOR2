//MARIA DE LOS ANGELES CAYETANA NEVADO RODRIGUEZ
using System;
using System.Collections.Generic;

namespace Generador
{
    public class Lenguaje : Sintaxis, IDisposable
    {
        public Lenguaje(string nombre) : base(nombre)
        {
        }
        public Lenguaje()
        {
        }
        public void Dispose()
        {
            cerrar();
        }
        public void gramatica()
        {
            cabecera();
            listaProducciones();
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
            match(Tipos.SNT);
            match(Tipos.Produce);
            match(Tipos.FinProduccion);
            if(!FinArchivo())
            {
                listaProducciones();
            }
        }
    }
}