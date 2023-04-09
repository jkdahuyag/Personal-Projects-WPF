using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class FileDataProvider<T>
    {
        public Graph<string> Graph { get; set; }

        public void CreateGraphFromFile(string vertexFileLocation, string edgeFileLocation)
        {
            var _vertexFile = File.Open(vertexFileLocation, FileMode.Open);
            var vertexReader = new StreamReader(_vertexFile);
            var vertexData = vertexReader.ReadToEnd();

            var vertices = new List<string>();
            
            var verticesFromFile = vertexData.Trim().Split(',',StringSplitOptions.TrimEntries);

            vertices.AddRange(verticesFromFile);
            
            _vertexFile.Close();
            vertexReader.Close();

            var _edgeFile = File.Open(edgeFileLocation, FileMode.Open);
            var edgeReader = new StreamReader(_edgeFile);
            var edgeData = edgeReader.ReadToEnd();

            var edges = new List<Edge>();

            var rowSeparator = "\n";
            var separatedRows = edgeData.Split(rowSeparator);
            foreach (var separatedRow in separatedRows)
            {
                if (separatedRow.Length <= 0) continue;
                var columns = separatedRow.Split(",",StringSplitOptions.TrimEntries);
                int from = int.Parse(columns[0]);
                int to = int.Parse(columns[1]);
                float weight = float.Parse(columns[2]);

                edges.Add(new Edge(from,to,weight));
            }

            _edgeFile.Close();
            edgeReader.Close();

            Graph = new Graph<string>(vertices, edges);
        }
    }
}
