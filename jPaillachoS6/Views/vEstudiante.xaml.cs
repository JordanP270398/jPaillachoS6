using jPaillachoS6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace jPaillachoS6.Views;

public partial class vEstudiante : ContentPage
{
    private const string Url = "http://192.168.1.54/vw_uisrael/post.php";

    private readonly HttpClient cliente = new HttpClient();

    private ObservableCollection<Estudiantes> _estudiante;


    public vEstudiante()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Mostrara();
    }

    public async void Mostrara()
    {
        var content = await cliente.GetStringAsync(Url);
        List<Estudiantes> mostrarEstudiante = JsonConvert.DeserializeObject<List<Estudiantes>>(content);
        _estudiante = new ObservableCollection<Estudiantes>(mostrarEstudiante);
        ListaEstudiantes.ItemsSource = _estudiante;
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new vNuevoEstudiante());
    }

    private async void listaEstudiantes_ItemSelected(
        object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is not Estudiantes estudiante)
            return;

        ListaEstudiantes.SelectedItem = null;
        await Navigation.PushAsync(new actualizarEliminar(estudiante));
    }

}