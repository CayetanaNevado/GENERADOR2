//MARIA DE LOS ANGELES CAYETANA NEVADO RODRIGUEZ
using System;
using System.IO;
using System.Collections.Generic;

namespace Generador
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                using (Lenguaje a = new Lenguaje("c2.gram"))
                
                {
                    a.gramatica();
                    /*
                    while(!a.FinArchivo())
                    {
                        a.NextToken();
                    }
                    */
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
