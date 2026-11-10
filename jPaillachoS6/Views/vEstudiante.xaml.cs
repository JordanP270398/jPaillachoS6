using jPaillachoS6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace jPaillachoS6.Views;

public partial class vEstudiante : ContentPage
{
    private const string Url = "http://10.2.15.22/vw_uisrael/post.php";

    private readonly HttpClient cliente = new HttpClient();

    private ObservableCollection<Estudiantes> _estudiante;


    public vEstudiante()
    {
        InitializeComponent();
        Mostrara();
    }

    public async void Mostrara()
    {
        var content = await cliente.GetStringAsync(Url);
        List<Estudiantes> mostrarEstudiante = JsonConvert.DeserializeObject<List<Estudiantes>>(content);
        _estudiante = new ObservableCollection<Estudiantes>(mostrarEstudiante);
        ListaEstudiantes.ItemsSource = _estudiante;
    }
}