using System.Net;

namespace jPaillachoS6.Views;

public partial class vNuevoEstudiante : ContentPage
{
	public vNuevoEstudiante()
	{
		InitializeComponent();
	}

    private static readonly HttpClient cliente = new();

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        string nombre = txtNombre.Text?.Trim() ?? string.Empty;
        string apellido = txtApellido.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
        {
            await DisplayAlertAsync("Campos obligatorios",
                "Ingresa el nombre y el apellido.", "Aceptar");
            return;
        }

        if (!int.TryParse(txtEdad.Text, out int edad) || edad <= 0)
        {
            await DisplayAlertAsync("Edad inválida",
                "Ingresa una edad entera mayor que cero.", "Aceptar");
            return;
        }

        btnAgregar.IsEnabled = false;

        try
        {
            using var datos = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["nombre"] = nombre,
                ["apellido"] = apellido,
                ["edad"] = edad.ToString()
            });

            using var respuesta = await cliente.PostAsync(
                "http://192.168.1.54/vw_uisrael/post.php", datos);

            respuesta.EnsureSuccessStatusCode();
            await Navigation.PopAsync();
        }
        catch (Exception ex) when (ex is HttpRequestException or OperationCanceledException)
        {
            await DisplayAlertAsync("Error",
                $"No se pudo confirmar el registro. {ex.Message}", "Aceptar");
        }
        finally
        {
            btnAgregar.IsEnabled = true;
        }
    }
}