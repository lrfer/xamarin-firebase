using Newtonsoft.Json;
using System.Text;
using WSistema.Models;

namespace WSistema;

public partial class AppShell : Shell
{

	public AppShell()
	{
		InitializeComponent();

        BindingContext = App.Previsao;

    }


}
