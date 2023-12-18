using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _108_CSV
{
    public class CreadorCSV
    {
        private char simboloSeparador =',';
        
        private char[] simbolosEspeciales = new char[] { '\n', '\r' };

        private bool ContieneSimbolos(string texto)
        {
            foreach (var letra in texto)
            {
                foreach (var simbolo in simbolosEspeciales)
                {
                    if (letra.Equals(simbolo))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public void ImprimirCampos(string[] campos)
        {
            for (int i = 0; i < campos.Length; i++)
            {
                Console.Write(campos[i]);


                if(i !=  campos.Length - 1)
                {
                    Console.Write(" , ");
                }
                else
                {
                    Console.WriteLine();
                }
                
            }
        }
        enum EstadoCSV{
            SinComillas,
            Comillas,
            ComillasDobles
        }
        public string[] ObtenerCamposFilaMaquinaEstado(string fila)
        {
            List<string> lstCampos = new List<string>();
            string textoActual = "";

            EstadoCSV estadoActual = EstadoCSV.SinComillas;

            foreach (char letra in fila)
            {
                if(estadoActual == EstadoCSV.SinComillas)
                {
                    if(letra == '"')
                    {
                        estadoActual = EstadoCSV.Comillas;
                    }
                    else if(letra == simboloSeparador)
                    {
                        lstCampos.Add(textoActual);
                        textoActual = "";
                    }
                    else
                    {
                        textoActual += letra;
                    }


                }else if(estadoActual == EstadoCSV.Comillas)
                {

                    if(letra == '"')
                    {
                        estadoActual = EstadoCSV.ComillasDobles;
                    }
                    else
                    {
                        textoActual += letra;
                    }



                }else if(estadoActual == EstadoCSV.ComillasDobles)
                {
                    if(letra == '"')
                    {
 
                        textoActual += "\"";
                        estadoActual = EstadoCSV.Comillas;

                    }
                    else if(letra == simboloSeparador)
                    {
                        lstCampos.Add(textoActual);
                        textoActual = "";
                        estadoActual = EstadoCSV.SinComillas;
                    }
                    else
                    {
                        estadoActual = EstadoCSV.SinComillas;
                    }
                }

            }

            lstCampos.Add(textoActual);

            return lstCampos.ToArray();
        }
        // Funcionando 
        public string[] ObtenerCamposFila(string fila)
        {
            

            List<string> lstCampos = new List<string>() { "" };

            int indiceActual = 0;

            // Banderas
            bool tieneComillas = false;
            bool tieneComillasComillas = false;
            bool empiezaTexto = true;
            



            foreach (char letra in fila)
            {
                // Cuando el texto empieza
                if (empiezaTexto)
                {

                    lstCampos[indiceActual] = "";
                    empiezaTexto = false;
                    tieneComillasComillas = false;

                    // Revisamos si la palabra empieza con comillas 
                    tieneComillas = letra == '"';
                    if (!tieneComillas)
                    {
                        lstCampos[indiceActual] += letra;
                    }
                    continue;
                }


                if (tieneComillasComillas)
                {

                    if(letra == '"')
                    {
                        tieneComillasComillas = false;
                        lstCampos[indiceActual] += "\"";

                    }else if(letra == simboloSeparador)
                    {
                        lstCampos.Add("");
                        indiceActual++;
                        empiezaTexto = true;
                    }
                    else
                    {
                        tieneComillasComillas = false;
                    }

                }
                // Cuando el texto empezo con comillas 
                else if (tieneComillas)
                {
                    if (letra == '"')
                    {
                        tieneComillasComillas = true;
                    }
                    else
                    {
                        lstCampos[indiceActual] += letra;
                    }
                }
                // Cuando el texto empieza sin comillas
                else
                {
                    // Si la letra actual es el simbolo seperador significa que llegamos al final del campo
                    if(letra == simboloSeparador)
                    {
                        lstCampos.Add("");
                        indiceActual++;
                        empiezaTexto = true;
                    }
                    // Si aun no llegamos al final 
                    else
                    {
                        lstCampos[indiceActual] += letra;
                    }
                }                
            }





            return lstCampos.ToArray();

        }
        public string EscaparTexto(string text)
        {
            string texto = "";


            texto = text.Replace('\n', ' ').Replace('\r',' ');
            // Si el texto tiene como contenido el texto separador o tiene una comilla doble
            // Ingresamos comillas simples al inicio y final
            if (text.IndexOf(simboloSeparador) != -1 || texto.IndexOf('"') != 1)
            {
                texto = string.Concat("\"", texto.Replace("\"","\"\""), "\"");

                
            }
            



            return texto;
        }

    }
}
