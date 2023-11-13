using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _108_CSV
{

    internal class Program
    {
        static void Main(string[] args)
        {
            CreadorCSV ocreador = new CreadorCSV();


            //string texto = Console.ReadLine();
            //Console.WriteLine( ocreador.EscaparTexto(texto));

            string filaCSV = "\"m\"\"PEPEP\",antonio,gonzalez,castealans";
            ocreador.ImprimirCampos(ocreador.ObtenerCamposFilaMaquinaEstado(filaCSV));
        }
    }
}
