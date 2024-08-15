namespace Suma4798198
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editResultadoId;


        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => listview.ItemsSource = await _dbService.GetResultado());
        }
        private async void sumarBtn_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Entryprimernumero.Text) || !string.IsNullOrEmpty(Entrysegundonumero.Text))
            {
                if (double.TryParse(Entryprimernumero.Text, out double num1) && double.TryParse(Entrysegundonumero.Text, out double num2))
                {
                    labelresultado.Text = (num1 + num2).ToString();
                    if (_editResultadoId == 0)
                    {
                        //Agrega resultados
                        await _dbService.Create(new Resultado
                        {
                            Numero1 = Entryprimernumero.Text,
                            Numero2 = Entrysegundonumero.Text,
                            Suma = labelresultado.Text
                        });
                    }
                    else
                    {
                        //Edita resultado
                        await _dbService.Update(new Resultado
                        {
                            Id = _editResultadoId,
                            Numero1 = Entryprimernumero.Text,
                            Numero2 = Entrysegundonumero.Text,
                            Suma = labelresultado.Text
                        });
                        _editResultadoId = 0;

                    }
                    Entryprimernumero.Text = string.Empty;
                    Entrysegundonumero.Text = string.Empty;
                    labelresultado.Text = string.Empty;
                    listview.ItemsSource = await _dbService.GetResultado();
                }
                else
                {
                    labelresultado.Text = "Ingrese solamente números válidos";
                }
            }
            else
            {
                labelresultado.Text  = "Llena todos los campos de lo que se le solicita";
            }

            

        }


        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var resultado = (Resultado)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editResultadoId = resultado.Id;
                    Entryprimernumero.Text = resultado.Numero1;
                    Entrysegundonumero.Text = resultado.Numero2;
                    labelresultado.Text = resultado.Suma;
                    break;

                case "Delete":
                    await _dbService.Delete(resultado);
                    listview.ItemsSource = await _dbService.GetResultado();
                    break;
            }

        }
    }

}
