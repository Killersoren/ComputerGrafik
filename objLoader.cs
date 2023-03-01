using OpenTK.Mathematics;
using Opgave_1___OpenTK.RendererStuff;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opgave_1___OpenTK
{
  
    class ObjLoader
    {
        public Model Load(string filePath)
        {
            
            List<Vector3> vertices = new List<Vector3>();
            List<int> indices = new List<int>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    string[] tokens = line.Split(' ');

                    //Console.WriteLine($"line: {line}");
                    //Console.WriteLine($"tokens: {string.Join(", ", tokens)}");

                    switch (tokens[0])
                    {
                        case "v":
                            // Vertex
                            float x = float.Parse(tokens[1], CultureInfo.InvariantCulture.NumberFormat);
                            float y = float.Parse(tokens[2], CultureInfo.InvariantCulture.NumberFormat);
                            float z = float.Parse(tokens[3], CultureInfo.InvariantCulture.NumberFormat);
                            vertices.Add(new Vector3(x, y, z));
                            break;
                        case "f":
                            // Face
                            int i0, i1, i2;
                            //int i0 = int.Parse(tokens[1]) - 1;
                            //int i1 = int.Parse(tokens[2]) - 1;
                            //int i2 = int.Parse(tokens[3]) - 1;
                            bool success = int.TryParse(tokens[0], out i0);
                            success &= int.TryParse(tokens[1], out i1);
                            success &= int.TryParse(tokens[2], out i2);
                            indices.Add(i0);
                            indices.Add(i1);
                            indices.Add(i2);

                            //int i1, i2, i3;

                            //// Attempt to parse each part of the face
                            //bool success = int.TryParse(tokens[0], out i1);
                            //success &= int.TryParse(tokens[1], out i2);
                            //success &= int.TryParse(tokens[2], out i3);

                            //indices.Add(i1);
                            //indices.Add(i2);
                            //indices.Add(i3);


                            //// If any of the parses failed, report the error
                            //if (!success)
                            //{
                            //    Console.WriteLine("Error parsing face: {0}", line);
                            //}
                            //else
                            //{
                            //    // Decrement to get zero-based vertex numbers
                            //    //face = new Tuple<int, int, int>(i1 - 1, i2 - 1, i3 - 1);
                            //    //faces.Add(face);
                            //    vertices = new Tuple<int, int, int>(i1 - 1, i2 - 1, i3 - 1);
                            //    indices.Add(vertices);
                            //}



                            break;
                    }
                }
            }

            return new Model(vertices.ToArray(), indices.ToArray());
        }
    }


}

