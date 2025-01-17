﻿using System;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows;

namespace MaciLaciMaui
{
    public class DataAccess
    {
        private readonly string directory;
        public DataAccess(string path)
        {
            this.directory = path;
        }

        public int[,] ReadFile(string level)
        {
            try
            {
                int[,] matrix;
                int rows;
                Task<Stream> streamTask = FileSystem.OpenAppPackageFileAsync(level);
                Stream stream = streamTask.Result;
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = reader.ReadLine(); // kezdetben beolvassuk az elso sort (ami a matrix merete) és ezzel tudjuk inicializalni a matrixt

                    if (line != null)
                    {
                        if (int.TryParse(line, out rows))
                        {
                            matrix = new int[rows, rows];

                            for (int i = 0; i < rows; i++) // majd addig olvasunk amig minden sort beolvasunk, ha hibat talalunk akkor jelezzuk egy Exceptionnal
                            {
                                line = reader.ReadLine();

                                if (line != null)
                                {
                                    char[] separator = new char[] { ' ', '\t'};
                                    string[] values = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                                    for (int j = 0; j < rows; j++)
                                    {
                                        if (int.TryParse(values[j], out int value))
                                        {
                                            matrix[i, j] = value;
                                        }
                                        else
                                        {
                                            throw new FormatException("Hibás érték a mátrixban.");
                                        }
                                    }
                                }
                                else
                                {
                                    throw new FormatException("Hiányzó sor az adatfájlban.");
                                }
                            }

                            return matrix;
                        }
                        else
                        {
                            throw new FormatException("Hibás sorok száma a fájlban.");
                        }
                    }
                    else
                    {
                        throw new FileNotFoundException("Nem sikerült beolvasni a sorok számát.");
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Debug.WriteLine("filenotfound");
                Console.WriteLine($"Hiba: {e.Message}");
                return new int[,] { { 0 } };
            }
            catch (FormatException e)
            {
               
                Console.WriteLine($"Hiba: {e.Message}");
                return new int[,] { { 0 } };
            }
            catch (Exception e)
            {
              
                Console.WriteLine($"Általános hiba: {e.Message}");
                return new int[,] { { 0 } };
            }
        }
    }
}
