using CommunityToolkit.Maui.Views;
using projeto_mobile_cliente_app.Dto;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace projeto_mobile_cliente_app.Views.App.Agendamento;

public partial class AdicionarLiquidoPopup : Popup, INotifyPropertyChanged
{
    private List<LiquidoDto> _liquidos;
    public List<LiquidoDto> Liquidos
    {
        get { return _liquidos; }
        set
        {
            _liquidos = value;
            OnPropertyChanged();
        }
    }
    public event Action<List<LiquidoDto>> OnSaveClicked = delegate { };

    public AdicionarLiquidoPopup(List<LiquidoDto> liquidos)
	{
		InitializeComponent();
		Liquidos = liquidos;
        BindingContext = this;
    }
    void OnItemTapped(object sender, EventArgs args)
    {
        var item = (LiquidoDto)((Grid)sender).BindingContext;
        item.IsSelected = !item.IsSelected;
    }
    void OnSaveButtonClicked(object sender, EventArgs args)
    {
        var selectedLiquidos = Liquidos.Where(l => l.IsSelected).ToList();
        OnSaveClicked(selectedLiquidos);
        this.Close();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}