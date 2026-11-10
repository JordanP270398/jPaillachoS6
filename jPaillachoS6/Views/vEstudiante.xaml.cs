using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace jPaillachoS6.Views;

public partial class vEstudiante : ContentPage
{
    private const string Url = "10.2.15.22";

    private readonly HttpClient cliente = new HttpClient();

    private ObservableCollection<Estudiante> _estudiante;


    public vEstudiante()
    {
        InitializeComponent();
    }

    public async void Mostrara()
    {
        var content await cliente.GetStringAsync(Url);
        List<Estudiante> mostrarEstudiante = JsonConvert.DeserializeObject<List<Estudiante>>(content);
        _estudiante = new ObservableCollection<Estudiante>(mostrarEstudiante);
        ListaEstudiantes.ItemSource = _estudiante;
    }
}