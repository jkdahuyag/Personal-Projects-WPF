using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Graph_Visualizer.Annotations;
using Graphs;
using Path = Graphs.Path;

namespace Graph_Visualizer
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml. INotifyPropertyChanged is used for data binding
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Graph<string> _graph;
        private GraphObject _graphObject;
        public FileDataProvider<string> DataProvider = new FileDataProvider<string>();

        public Graph<string> Graph
        {
            get => _graph;
            set
            {
                _graph = value;
                OnPropertyChanged();
            }
        }

        public GraphObject GraphObjects {
            get => _graphObject;
            set
            {
                _graphObject = value;
                OnPropertyChanged();
            }
        }
        private bool _isDraggingVertex;
        
        public MainWindow()
        {
            InitializeComponent();
            GraphObjects = new GraphObject();
            DataContext = this;
        }

        public MainWindow(Graph<string> graph) :this()
        {
            Graph = graph;
            UpdateGraphObjects();
        }

        private void UpdateGraphObjects()
        {
            for (int i = 0; i < Graph.VertexCount; i++)
            {
                var vertexShape = new VertexAsEllipse(Graph.Vertices[i]);
                GraphObjects.Vertices.Add(vertexShape);
                GraphObjects.Collection.Add(vertexShape);
            }

            foreach (var graphEdge in Graph.Edges)
            {
                var from = GraphObjects.Vertices[graphEdge.From];
                var to = GraphObjects.Vertices[graphEdge.To];
                var edgeShape = new EdgesAsLines(from, to, graphEdge.Weight);
                var existingEdge = GraphObjects.Edges.FirstOrDefault(c => (c.FromVertex == from &&
                                                                           c.ToVertex == to) ||
                                                                          (c.FromVertex == to &&
                                                                           c.ToVertex == from));
                if (existingEdge == null)
                {
                    GraphObjects.Edges.Add(edgeShape);
                    GraphObjects.Collection.Add(edgeShape);
                }
            }
        }

        private void LoadDefaultGraph()
        {
            DataProvider.CreateGraphFromFile("../../../../Vertex Data.txt", "../../../../Edge Data.txt");
            Graph = DataProvider.Graph;
            UpdateGraphObjects();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Graph == null)
            {
                LoadDefaultGraph();
            }
        }
        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse vertex = sender as Ellipse;
            _isDraggingVertex = true;
            vertex.CaptureMouse();
        }

        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Ellipse vertex = sender as Ellipse;
            _isDraggingVertex = false;
            vertex.ReleaseMouseCapture();
        }
        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            Ellipse vertex = sender as Ellipse;
            //Gets the data source from the collection of VerticesAsShapes
            VertexAsEllipse vertexAsEllipseFromSource = vertex.DataContext as VertexAsEllipse; 
            if (!_isDraggingVertex) return;

            // get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(CanvasMain);

            // center the vertex on the mouse
            if (mousePos.X < ViewBox.ActualWidth - vertex.ActualWidth/2 
                && mousePos.X > vertex.ActualWidth/2
                && mousePos.Y > vertex.ActualHeight/2
                && mousePos.Y < ViewBox.ActualHeight - vertex.ActualHeight / 2)
            {
                vertexAsEllipseFromSource.Left = mousePos.X - (vertex.ActualWidth / 2);
                vertexAsEllipseFromSource.Top = mousePos.Y - (vertex.ActualHeight / 2);
            }
            //Find edges that connect to the vertex and update their coordinates
            var connectedEdges = GraphObjects.Edges.Where(c =>
                c.FromVertex == vertexAsEllipseFromSource || c.ToVertex == vertexAsEllipseFromSource);
            foreach (var connectedEdge in connectedEdges)
            {
                connectedEdge.UpdateCoordinates();
            }
        }
        private void BtnAddVertex_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Graph.AddVertex(TxtBoxAddVertex.Text);
                var vertexShape = new VertexAsEllipse(TxtBoxAddVertex.Text);
                GraphObjects.Vertices.Add(vertexShape);
                GraphObjects.Collection.Add(vertexShape);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void BtnAddEdge_OnClick(object sender, RoutedEventArgs e)
        {
            var input = TxtBoxAddEdge.Text.Split(",");
            try
            {
                var newEdge = new Edge(int.Parse(input[0]), int.Parse(input[1]), float.Parse(input[2]));
                Graph.AddEdge(newEdge);
                var from = GraphObjects.Vertices[newEdge.From];
                var to = GraphObjects.Vertices[newEdge.To];
                var edgeShape = new EdgesAsLines(from, to, newEdge.Weight);
                var existingEdge = GraphObjects.Edges.FirstOrDefault(c => (c.FromVertex == from &&
                                                                           c.ToVertex == to) ||
                                                                          (c.FromVertex == to &&
                                                                           c.ToVertex == from));
                if (existingEdge == null)
                {
                    GraphObjects.Edges.Add(edgeShape);
                    GraphObjects.Collection.Add(edgeShape);
                }
                
            }
            catch (Exception exception)
            {
                if (exception is FormatException)
                {
                    MessageBox.Show("Edge format should be int,int,float");
                }else if (exception is ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Given input was out of range. Input should be index of the vertex");
                }
                else
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void BtnGetShortestPath_OnClick(object sender, RoutedEventArgs e)
        {
            var fromString = CmbBoxFrom.Text;
            var toString = CmbBoxTo.Text;
            
            var fromIndex = Graph.GetIndex(fromString);
            var toIndex = Graph.GetIndex(toString);
            try
            {
                var shortestPath = Graph.GetShortestPath(fromIndex);
                var prev = shortestPath.Prev[toIndex];
                var cost = shortestPath.Costs[toIndex];
                var searchOrder = new Stack<int>();
                searchOrder.Push(toIndex);
                while (prev != -1)
                {
                    searchOrder.Push(prev);
                    prev = shortestPath.Prev[prev];
                }

                var count = searchOrder.Count;
                var edgesInPath = new List<EdgesAsLines>();
                for (int i = 0; i < count - 1; i++)
                {
                    var from = searchOrder.Pop();
                    var to = searchOrder.Peek();
                    edgesInPath.AddRange(GraphObjects.Edges.Where(
                        c =>
                            (c.FromVertex == GraphObjects.Vertices[from] &&
                             c.ToVertex == GraphObjects.Vertices[to]) ||
                            (c.FromVertex == GraphObjects.Vertices[to] &&
                             c.ToVertex == GraphObjects.Vertices[from])));
                }

                foreach (var graphObjectsEdge in GraphObjects.Edges)
                {
                    graphObjectsEdge.Color = Brushes.HotPink;
                }
                foreach (var edge in edgesInPath)
                {
                    edge.Color = Brushes.LawnGreen;
                }

                MessageBox.Show(cost == float.MaxValue
                    ? $"There is no path from {CmbBoxFrom.Text} to {CmbBoxTo.Text}"
                    : $"The shortest Distance from {CmbBoxFrom.Text} to {CmbBoxTo.Text} is {cost}");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Please select vertices on \"From\" and \"To\".");
            }
        }

        private void BtnClearGraph_OnClick(object sender, RoutedEventArgs e)
        {
            Graph = new Graph<string>(new List<string>(), new List<Edge>());
            GraphObjects = new GraphObject();
        }

        private void BtnReloadDefault_OnClick(object sender, RoutedEventArgs e)
        {
            Graph = new Graph<string>(new List<string>(), new List<Edge>());
            GraphObjects = new GraphObject();
            LoadDefaultGraph();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// An Edge class that aids in displaying the edge as lines in the canvas
    /// </summary>
    public class EdgesAsLines : INotifyPropertyChanged
    {
        private double _zIndex = 5;
        private double _left = 0;
        private double _top = 0;
        private double _x1 = 100;
        private double _y1 = 100;
        private double _x2 = 100;
        private double _y2 = 100;
        private float _weight = 0;
        private Brush _color = Brushes.HotPink;
        private VertexAsEllipse _fromVertex;
        private VertexAsEllipse _toVertex;
        private Thickness _margin;
        public Thickness Margin
        {
            get => _margin;
            set
            {
                _margin = value;
                OnPropertyChanged();
            }
        }
        public VertexAsEllipse FromVertex
        {
            get => _fromVertex;
            set
            {
                _fromVertex = value;
                OnPropertyChanged();
            }
        }
        public VertexAsEllipse ToVertex
        {
            get => _toVertex;
            set
            {
                _toVertex = value;
                OnPropertyChanged();
            }
        }
        public double ZIndex
        {
            get => _zIndex;
            set
            {
                _zIndex = value;
                OnPropertyChanged();
            }
        }
        public double Left
        {
            get => _left;
            set
            {
                _left = value;
                OnPropertyChanged();
            }
        }
        public double Top
        {
            get => _top;
            set
            {
                _top = value;
                OnPropertyChanged();
            }
        }
        public double X1
        {
            get => _x1;
            set
            {
                _x1 = value;
                OnPropertyChanged();
            }
        }

        public double Y1
        {
            get => _y1;
            set
            {
                _y1 = value;
                OnPropertyChanged();
            }
        }
        public double X2
        {
            get => _x2;
            set
            {
                _x2 = value;
                OnPropertyChanged();
            }
        }
       
        public double Y2
        {
            get => _y2;
            set
            {
                _y2 = value;
                OnPropertyChanged();
            }
        }
        public float Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged();
            }
        }
        public Brush Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }
        public EdgesAsLines(VertexAsEllipse fromVertex,VertexAsEllipse toVertex, float weight)
        {
            _fromVertex = fromVertex;
            _toVertex = toVertex;
            _weight = weight;
            UpdateCoordinates();
        }

        public void UpdateCoordinates()
        {
            var x1 = FromVertex.Left + FromVertex.Radius;
            var x2 = ToVertex.Left + ToVertex.Radius;
            var y1 = FromVertex.Top + FromVertex.Radius;
            var y2 = ToVertex.Top + ToVertex.Radius;
            var lesserX = Math.Min(x1, x2);
            var lesserY = Math.Min(y1, y2);
            X1 = x1 - _left;
            Y1 = y1 - _top;
            X2 = x2 - _left;
            Y2 = y2 - _top;
            //correction for text
            Margin = new Thickness(lesserX-20, lesserY-20, 0, 0);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// A Vertex Class that aids in displaying the vertex to the canvas dynamically
    /// </summary>
    public class VertexAsEllipse : INotifyPropertyChanged
    {
        private double _zIndex = 10;
        private double _top = 100;
        private double _left = 100;
        public int Radius = 10;
        public double ZIndex
        {
            get => _zIndex;
            set
            {
                _zIndex = value;
                OnPropertyChanged();
            }
        }
        public double Top
        {
            get => _top;
            set
            {
                _top = value;
                OnPropertyChanged();
            }
        }

        public double Left
        {
            get => _left;
            set
            {
                _left = value;
                OnPropertyChanged();
            }
        }

        public string VertexName { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public VertexAsEllipse(string vertexVertexName)
        {
            Random r = new Random();
            int randLeft = r.Next(30, 1000);
            int randTop = r.Next(30, 800);
            VertexName = vertexVertexName;
            Top = randTop;
            Left = randLeft;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// Contains collection of both edges and vertices to be added to the canvas
    /// </summary>
    public class GraphObject : INotifyPropertyChanged
    {
        ObservableCollection<VertexAsEllipse> _vertices = new ObservableCollection<VertexAsEllipse>();
        ObservableCollection<EdgesAsLines> _edges = new ObservableCollection<EdgesAsLines>();
        CompositeCollection collection = new CompositeCollection();
        public event PropertyChangedEventHandler? PropertyChanged;
        public CompositeCollection Collection
        {
            get
            {
                return collection;
            }

            set
            {
                collection = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<EdgesAsLines> Edges
        {
            get
            {
                return _edges;
            }

            set
            {
                _edges = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<VertexAsEllipse> Vertices
        {
            get
            {
                return _vertices;
            }

            set
            {
                _vertices = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
