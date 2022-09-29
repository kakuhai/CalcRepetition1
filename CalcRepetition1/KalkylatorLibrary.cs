using System;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace KalkylatorLibrary
{
    public class Kalkylator
    {
        //JsonWriter används för att skapa en loggfil av tidigare operationer.
        JsonWriter writer;
        public Kalkylator()
        {
            StreamWriter logFile = File.CreateText("kalkylatorlogg.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operationer");
            writer.WriteStartArray();
        }
        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Standardvärde är "not-a-number" om en operation, t.ex. en division, kan resultera i ett error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand 1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand 2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            // Switch användes för att utföra uträkningarna.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Addera");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtrahera");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiplicera");
                    break;
                case "d":
                    // Be användaren ange en icke-nollsiffra.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Dividera");
                    break;
                // Returnera text för inkorrekt inmatning.
                default:
                    break;
            }
            writer.WritePropertyName("Resultat");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}
