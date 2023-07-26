using Newtonsoft.Json;
using WSistema.Models;

namespace WSistema;

public partial class App : Application
{
	public static PrevisaoResponse Previsao;
    public static ListPrevisaoResponse ListPrevisao = new ListPrevisaoResponse();
	public App()
	{
		InitializeComponent();

		Previsao = new PrevisaoResponse();

        Task.Run(() => this.PopulateList()).Wait();


		MainPage = new AppShell();

	}

    private async void PopulateList()
    {
        for (int i = 1; i <= 7 ; i++)
        {
            PrevisaoResponse precisao = new PrevisaoResponse();
            precisao = await GetPrevisao(i);
            precisao.id = i;
            ListPrevisao.PrevisaoAtual.Add(precisao);
        }
        for (int i = 1; i <= 7; i++)
        {
            PrevisaoResponse precisao = new PrevisaoResponse();
            precisao = await GetPrevisao(i,false);
            precisao.id = i;
            ListPrevisao.PrevisaoAmanha.Add(precisao);
        }
    }

    private static async Task<PrevisaoResponse> GetPrevisao(int id, bool hoje = true)
    {
        string url = "https://pds-ufu.rj.r.appspot.com/";

        if (hoje)
            url = url + "previsaoatual/";
        else
            url = url + "previsaoproximodia/";


        using (var client = new HttpClient())
        {

            var response = await client.GetAsync(url + id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PrevisaoResponse>(jsonString);
            }
            else return null;
        }
    }
}
